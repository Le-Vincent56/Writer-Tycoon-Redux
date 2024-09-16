using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WriterTycoon.Entities.Player.States;
using WriterTycoon.Input;
using WriterTycoon.Patterns.EventBus;
using WriterTycoon.Patterns.ServiceLocator;
using WriterTycoon.Patterns.StateMachine;
using WriterTycoon.World.Graph;

namespace WriterTycoon.Entities.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private GameInputReader inputReader;
        [SerializeField] private bool canMove;
        [SerializeField] private float moveSpeed;
        [SerializeField] private bool moving;
        [SerializeField] private bool working;

        private MapGraph graph;
        private Node currentNode;
        [SerializeField] private List<Node> currentPath;
        [SerializeField] private int currentPathIndex;

        private Animator animator;
        private SpriteRenderer spriteRenderer;

        private StateMachine stateMachine;

        private EventBinding<CommandPlayerPosition> commandPlayerPositionEvent;
        private EventBinding<CalendarPauseStateChanged> pauseCalendarEvent;
        private EventBinding<ChangePlayerWorkState> changePlayerWorkStateEvent;

        private void Awake()
        {
            // Get components
            animator = GetComponentInChildren<Animator>();
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();

            // Set variables
            canMove = true;

            // Initialize the state machine
            stateMachine = new StateMachine();

            // Create states
            IdleState idleState = new(this, spriteRenderer);
            LocomotionState locomotionState = new(this, spriteRenderer);
            WorkState workState = new(this, spriteRenderer);

            // Define state transitions
            stateMachine.At(idleState, locomotionState, new FuncPredicate(() => moving));
            stateMachine.At(idleState, workState, new FuncPredicate(() => working));

            stateMachine.At(locomotionState, idleState, new FuncPredicate(() => !moving));
            stateMachine.At(locomotionState, workState, new FuncPredicate(() => working));

            stateMachine.At(workState, idleState, new FuncPredicate(() => !working && !moving));
            stateMachine.At(workState, locomotionState, new FuncPredicate(() => !working && moving));

            // Set an initial state
            stateMachine.SetState(idleState);
        }

        private void OnEnable()
        {
            inputReader.Move += OnMove;

            commandPlayerPositionEvent = new EventBinding<CommandPlayerPosition>(MovePlayer);
            EventBus<CommandPlayerPosition>.Register(commandPlayerPositionEvent);

            pauseCalendarEvent = new EventBinding<CalendarPauseStateChanged>(HandleCalendarPause);
            EventBus<CalendarPauseStateChanged>.Register(pauseCalendarEvent);

            changePlayerWorkStateEvent = new EventBinding<ChangePlayerWorkState>(HandlePlayerWorkStateChange);
            EventBus<ChangePlayerWorkState>.Register(changePlayerWorkStateEvent);
        }

        private void Start()
        {
            // Get the graph to use as a service
            graph = ServiceLocator.ForSceneOf(this).Get<MapGraph>();

            currentNode = graph.GetNodeFromWorldPosition(transform.position);
            transform.position = graph.GetWorldPosFromNode(currentNode);
        }

        private void OnDisable()
        {
            inputReader.Move -= OnMove;

            EventBus<CommandPlayerPosition>.Deregister(commandPlayerPositionEvent);
            EventBus<CalendarPauseStateChanged>.Deregister(pauseCalendarEvent);
            EventBus<ChangePlayerWorkState>.Deregister(changePlayerWorkStateEvent);
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
        /// Callback handler for when the Player work state is to change
        /// </summary>
        private void HandlePlayerWorkStateChange(ChangePlayerWorkState eventData)
        {
            // Set the player to working
            working = eventData.Working;
        }

        /// <summary>
        /// Handle Player movement input
        /// </summary>
        private void OnMove(Vector2 movementVector, bool started)
        {
            // Exit case - if the player can't move
            if (!canMove) return;

            // Check if the control has just been pressed and the Player is working
            if (started && working)
            {
                // Update the Player Work State to stop working
                EventBus<ChangePlayerWorkState>.Raise(new ChangePlayerWorkState()
                {
                    Working = false
                });
            }
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

            // Start traversal
            StartTraversal();
        }

        /// <summary>
        /// Set the path for traversal
        /// </summary>
        private void SetPath(Node endNode) => currentPath = graph.PathfindAStar(currentNode, endNode);

        /// <summary>
        /// Begin player traversal
        /// </summary>
        private void StartTraversal()
        {
            currentPathIndex = 0;
            moving = true;
            StartCoroutine(TraversePath());
        }

        /// <summary>
        /// Handle traversal
        /// </summary>
        private IEnumerator TraversePath()
        {
            while(currentPathIndex < currentPath.Count)
            {
                Node targetNode = currentPath[currentPathIndex];

                // Move the player towards the target node
                Vector3 targetPosition = graph.GetWorldPosFromNode(targetNode);

                // Move until the player has reached the position
                while(Vector3.Distance(transform.position, targetPosition) > 0.1f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                    yield return null;
                }

                // Snap to the exact position
                transform.position = targetPosition;

                // Move to the next node in the path
                currentPathIndex++;

                yield return null;
            }

            // Finish traversal
            moving = false;
        }
    }
}