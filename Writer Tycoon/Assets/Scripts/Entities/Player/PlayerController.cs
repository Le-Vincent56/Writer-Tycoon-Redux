using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WriterTycoon.Entities.Player.States;
using WriterTycoon.Entities.Tracker;
using WriterTycoon.Patterns.EventBus;
using WriterTycoon.Patterns.ServiceLocator;
using WriterTycoon.Patterns.StateMachine;
using WriterTycoon.WorkCreation.Development.Tracker;
using WriterTycoon.World.Graph;

namespace WriterTycoon.Entities.Player
{
    public enum CommandType
    {
        Computer,
        Fridge
    }

    public class PlayerController : MonoBehaviour, IWorker
    {
        [Header("General")]
        [SerializeField] private string playerName;

        [Header("Locomotion")]
        [SerializeField] private bool canMove;
        [SerializeField] private float baseMoveSpeed;
        [SerializeField] private float currentMoveSpeed;
        [SerializeField] private bool moving;

        private MapGraph graph;
        private Node currentNode;
        [SerializeField] private List<Node> currentPath;
        [SerializeField] private int currentPathIndex;

        [Header("Rest/Recreation")]
        [SerializeField] private bool firstTimeWorking;
        [SerializeField] private bool eating;

        [Header("Work")]
        [SerializeField] private bool working;
        [SerializeField] private int assignedHash;

        private Animator animator;
        private SpriteRenderer spriteRenderer;
        private WorkerRecord workerRecord;

        private StateMachine stateMachine;

        private EventBinding<CommandPlayerPosition> commandPlayerPositionEvent;
        private EventBinding<CalendarPauseStateChanged> pauseCalendarEvent;
        private EventBinding<NotifySuccessfulCreation> notifySuccessfulCreationEvent;
        private EventBinding<EndEditing> endEditingEvent;
        private EventBinding<ChangeCalendarSpeed> changeCalendarSpeedEvent;

        public string Name { get => playerName; set => playerName = value; }
        public bool Working { get => working; set => working = value; }
        public int AssignedHash { get => assignedHash; set => assignedHash = value; }

        private void Awake()
        {
            // Get components
            animator = GetComponentInChildren<Animator>();
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();

            // Set variables
            currentMoveSpeed = baseMoveSpeed;
            canMove = true;
            firstTimeWorking = true;
            AssignedHash = -1;
            playerName = "Vincent Le";

            // Initialize the state machine
            stateMachine = new StateMachine();

            // Create states
            IdleState idleState = new(this, spriteRenderer);
            LocomotionState locomotionState = new(this, spriteRenderer);
            WorkState workState = new(this, spriteRenderer);
            EatState eatState = new(this, spriteRenderer);

            // Define state transitions
            stateMachine.At(idleState, locomotionState, new FuncPredicate(() => moving));
            stateMachine.At(idleState, workState, new FuncPredicate(() => working));
            stateMachine.At(idleState, eatState, new FuncPredicate(() => eating));

            stateMachine.At(locomotionState, idleState, new FuncPredicate(() => !moving));
            stateMachine.At(locomotionState, workState, new FuncPredicate(() => working && !moving));
            stateMachine.At(locomotionState, eatState, new FuncPredicate(() => eating && !moving));

            stateMachine.At(workState, idleState, new FuncPredicate(() => !working && !moving));
            stateMachine.At(workState, locomotionState, new FuncPredicate(() => !working && moving));

            stateMachine.At(eatState, idleState, new FuncPredicate(() => !eating && !moving));
            stateMachine.At(eatState, locomotionState, new FuncPredicate(() => !eating && moving));

            // Set an initial state
            stateMachine.SetState(idleState);
        }

        private void OnEnable()
        {
            commandPlayerPositionEvent = new EventBinding<CommandPlayerPosition>(MovePlayer);
            EventBus<CommandPlayerPosition>.Register(commandPlayerPositionEvent);

            pauseCalendarEvent = new EventBinding<CalendarPauseStateChanged>(HandleCalendarPause);
            EventBus<CalendarPauseStateChanged>.Register(pauseCalendarEvent);

            notifySuccessfulCreationEvent = new EventBinding<NotifySuccessfulCreation>(ResetDevelopmentVars);
            EventBus<NotifySuccessfulCreation>.Register(notifySuccessfulCreationEvent);

            endEditingEvent = new EventBinding<EndEditing>(EndEditing);
            EventBus<EndEditing>.Register(endEditingEvent);

            changeCalendarSpeedEvent = new EventBinding<ChangeCalendarSpeed>(ChangePlayerSpeed);
            EventBus<ChangeCalendarSpeed>.Register(changeCalendarSpeedEvent);
        }

        private void OnDisable()
        {
            EventBus<CommandPlayerPosition>.Deregister(commandPlayerPositionEvent);
            EventBus<CalendarPauseStateChanged>.Deregister(pauseCalendarEvent);
            EventBus<NotifySuccessfulCreation>.Deregister(notifySuccessfulCreationEvent);
            EventBus<EndEditing>.Deregister(endEditingEvent);
            EventBus<ChangeCalendarSpeed>.Deregister(changeCalendarSpeedEvent);
        }

        private void Start()
        {
            // Get the graph to use as a service
            graph = ServiceLocator.ForSceneOf(this).Get<MapGraph>();

            currentNode = graph.GetNodeFromWorldPosition(transform.position);
            transform.position = graph.GetWorldPosFromNode(currentNode);

            // Get the worker record to use as a service
            workerRecord = ServiceLocator.ForSceneOf(this).Get<WorkerRecord>();
            workerRecord.RecordWorker(this);
        }

        private void Update()
        {
            // Update the state machine
            stateMachine.Update();
        }
        private void FixedUpdate()
        {
            // Fixed update the state machine
            stateMachine.FixedUpdate();
        }

        /// <summary>
        /// Callback handler for when the Calendar pause state has changed
        /// </summary>
        private void HandleCalendarPause(CalendarPauseStateChanged eventData)
        {
            // Don't allow the player to move if the Calendar is paused
            canMove = !eventData.Paused;
        }

        /// <summary>
        /// Callback handler for when a work's editing phase ends
        /// </summary>
        private void EndEditing(EndEditing eventData)
        {
            // Set the player to no longer working
            working = false;
        }

        /// <summary>
        /// Callback handler for resetting development variables on new work creation
        /// </summary>
        private void ResetDevelopmentVars(NotifySuccessfulCreation eventData)
        {
            // Set first time working
            firstTimeWorking = true;
        }

        /// <summary>
        /// Callback handler for changing the Calendar speed
        /// </summary>
        private void ChangePlayerSpeed(ChangeCalendarSpeed eventData)
        {
            // Multiply the base move speed by the Calendar's time scale
            currentMoveSpeed = baseMoveSpeed * eventData.TimeScale;
        }

        /// <summary>
        /// Callback function to move the player when commanded
        /// </summary>
        private void MovePlayer(CommandPlayerPosition eventData)
        {
            // Set the path at the node position of the event data position
            SetPath(
                graph.GetNodeAtCellPosition(
                    eventData.TargetPosition.x, 
                    eventData.TargetPosition.y
                )
            );

            switch (eventData.Type)
            {
                case CommandType.Computer:
                    // Start traversal to the computer
                    StartTraversal(() =>
                    {
                        working = true;

                        // Check if the first time working
                        if (firstTimeWorking)
                        {
                            // Set first time working to false
                            firstTimeWorking = false;

                            // Open the Focus Slider window
                            EventBus<OpenSliderWindow>.Raise(new OpenSliderWindow()
                            {
                                Hash = assignedHash
                            });

                            // Set the current development phase state
                            EventBus<SetDevelopmentPhase>.Raise(new SetDevelopmentPhase()
                            {
                                Hash = assignedHash,
                                Phase = DevelopmentPhase.PhaseOne
                            });

                            // Prepare the sliders
                            EventBus<PrepareSliders>.Raise(new PrepareSliders()
                            {
                                Hash = assignedHash
                            });
                        }
                    });
                    break;
                case CommandType.Fridge:
                    // Start traversal to the fridge
                    StartTraversal(() =>
                    {
                        eating = true;
                    });
                    break;
            }
        }

        /// <summary>
        /// Set the path for traversal
        /// </summary>
        public void SetPath(Node endNode) => currentPath = graph.PathfindAStar(currentNode, endNode);

        /// <summary>
        /// Begin player traversal
        /// </summary>
        public void StartTraversal(Action onComplete)
        {
            currentPathIndex = 0;
            moving = true;
            working = false;
            eating = false;
            StartCoroutine(TraversePath(onComplete));
        }

        /// <summary>
        /// Handle traversal
        /// </summary>
        private IEnumerator TraversePath(Action onComplete)
        {
            // Exit case - if no path was found
            if(currentPath == null)
            {
                Debug.Log("No path found");
                yield break;
            }

            while(currentPathIndex < currentPath.Count)
            {
                // If can't move, then constantly loop until able to
                if (!canMove)
                    yield return null;

                // Get the target node
                Node targetNode = currentPath[currentPathIndex];

                // Move the player towards the target node
                Vector3 targetPosition = graph.GetWorldPosFromNode(targetNode);

                // Move until the player has reached the position
                while(Vector3.Distance(transform.position, targetPosition) > 0.1f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, targetPosition, currentMoveSpeed * Time.deltaTime);
                    yield return null;
                }

                // Snap to the exact position
                transform.position = targetPosition;

                // Set the current node to the target node
                currentNode = targetNode;

                // Move to the next node in the path
                currentPathIndex++;

                yield return null;
            }

            // Finish traversal
            moving = false;

            onComplete.Invoke();
        }
    }
}