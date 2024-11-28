using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using GhostWriter.Patterns.EventBus;

namespace GhostWriter.World.Interactables.UI
{
    public class InteractionMenuDisplayer : SerializedMonoBehaviour
    {
        [SerializeField] private float fadeDuration;
        [SerializeField] private Dictionary<InteractableType, CanvasGroup> menus;
        [SerializeField] private CanvasGroup bookshelfGroup;
        [SerializeField] private CanvasGroup computerGroup;
        [SerializeField] private CanvasGroup fridgeGroup;

        private EventBinding<ToggleInteractMenu> showInteractMenuEvent;
        private EventBinding<CloseInteractMenus> closeInteractMenusEvent;

        private void Awake()
        {
            bookshelfGroup.transform.localScale = new Vector3(1f, 0f, 1f);
            computerGroup.transform.localScale = new Vector3(1f, 0f, 1f);
            fridgeGroup.transform.localScale = new Vector3(1f, 0f, 1f);

            menus = new()
            {
                { InteractableType.Bookshelf, bookshelfGroup },
                { InteractableType.Computer, computerGroup },
                { InteractableType.Fridge, fridgeGroup },
            };
        }

        private void OnEnable()
        {
            showInteractMenuEvent = new EventBinding<ToggleInteractMenu>(HandleInteractionMenus);
            EventBus<ToggleInteractMenu>.Register(showInteractMenuEvent);

            closeInteractMenusEvent = new EventBinding<CloseInteractMenus>(CloseInteractionMenus);
            EventBus<CloseInteractMenus>.Register(closeInteractMenusEvent);
        }

        private void OnDisable()
        {
            EventBus<ToggleInteractMenu>.Deregister(showInteractMenuEvent);
        }

        /// <summary>
        /// Callback function to handle the interaction menus
        /// </summary>
        private void HandleInteractionMenus(ToggleInteractMenu eventData)
        {
            switch (eventData.InteractableType)
            {
                case InteractableType.Bookshelf:
                    HandleMenu(eventData.Interactable, eventData.Opening, bookshelfGroup, eventData.CursorPosition);
                    break;

                case InteractableType.Computer:
                    HandleMenu(eventData.Interactable, eventData.Opening, computerGroup, eventData.CursorPosition);
                    break;

                case InteractableType.Fridge:
                    HandleMenu(eventData.Interactable, eventData.Opening, fridgeGroup, eventData.CursorPosition);
                    break;
            }
        }

        /// <summary>
        /// Callback function to hide all interaction menus
        /// </summary>
        private void CloseInteractionMenus(CloseInteractMenus eventData)
        {
            // Close all the menus
            CloseMenus();
        }

        /// <summary>
        /// Handle the opening and closing of the menu
        /// </summary>
        private void HandleMenu(Interactable interactable, bool opening, CanvasGroup canvasGroup, Vector2 cursorPosition)
        {
            // Close all menus
            CloseMenus();

            // Check if opening the menu
            if (opening)
                // If so, show the menu
                ShowMenu(canvasGroup, cursorPosition, interactable);
            else
                // Otherwise, hide the menu
                HideMenu(canvasGroup, interactable);
        }

        /// <summary>
        /// Close all interaction menus
        /// </summary>
        private void CloseMenus()
        {
            foreach(KeyValuePair<InteractableType, CanvasGroup> kvp in menus)
            {
                // Hide the canvas group
                HideMenu(kvp.Value);
            }
        }

        /// <summary>
        /// Show the menu
        /// </summary>
        private void ShowMenu(CanvasGroup canvasGroup, Vector2 cursorPosition, Interactable interactable = null)
        {
            canvasGroup.transform.position = cursorPosition;

            // Fade in to 1
            Fade(canvasGroup, 1f, fadeDuration, () =>
            {
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
            });

            // Scale up to 1
            Scale(canvasGroup, 1f, fadeDuration);

            // Set interacting
            EventBus<SetInteracting>.Raise(new SetInteracting()
            {
                Interacting = true
            });

            // Pause the Calendar
            EventBus<ChangeCalendarPauseState>.Raise(new ChangeCalendarPauseState()
            {
                Paused = true,
                AllowSpeedChanges = false
            });

            // Exit case - if the interactable doesn't exist
            if (interactable == null) return;

            // Set the interactable to close the menu
            interactable.SetOpenMenu(false);
        }

        /// <summary>
        /// Hide the menu
        /// </summary>
        private void HideMenu(CanvasGroup canvasGroup, Interactable interactable = null)
        {
            // Scale down to 0
            Scale(canvasGroup, 0f, fadeDuration);

            // Fade to 0
            Fade(canvasGroup, 0f, fadeDuration, () =>
            {
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
            });

            // Set not interacting
            EventBus<SetInteracting>.Raise(new SetInteracting()
            {
                Interacting = false
            });

            // Unpause the Calendar
            EventBus<ChangeCalendarPauseState>.Raise(new ChangeCalendarPauseState()
            {
                Paused = false,
                AllowSpeedChanges = true
            });

            // Exit case - if the interactable doesn't exist
            if (interactable == null) return;

            // Set the interactable to open the menu
            interactable.SetOpenMenu(true);
        }

        /// <summary>
        /// Handle fading for the menus
        /// </summary>
        private void Fade(CanvasGroup canvasGroup, float endFadeValue, float duration, TweenCallback onEnd = null, Ease easeType = Ease.OutQuint)
        {
            // Set the fade animation
            Tween fadeTween = canvasGroup.DOFade(endFadeValue, duration)
                .SetEase(easeType);

            // Exit case - if there is no given Tween Callback
            if (onEnd == null) return;

            // Hook up callback events
            fadeTween.onComplete += onEnd;
        }

        /// <summary>
        /// Handle scaling for the menus
        /// </summary>
        private void Scale(CanvasGroup canvasGroup, float endFadeValue, float duration, Ease easeType = Ease.OutQuint)
        {
            // Set the scale animation
            canvasGroup.transform.DOScaleY(endFadeValue, duration)
                .SetEase(easeType);
        }
    }
}