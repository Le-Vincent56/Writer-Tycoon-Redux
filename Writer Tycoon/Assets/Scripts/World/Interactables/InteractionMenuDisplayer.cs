using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using WriterTycoon.Patterns.EventBus;

namespace WriterTycoon.World.Interactables.UI
{
    public class InteractionMenuDisplayer : MonoBehaviour
    {
        [SerializeField] private float fadeDuration;
        [SerializeField] private CanvasGroup computerGroup;
        [SerializeField] private CanvasGroup fridgeGroup;
        private HashSet<CanvasGroup> canvasGroups;

        private EventBinding<ToggleInteractMenu> showInteractMenuEvent;
        private EventBinding<CloseInteractMenus> closeInteractMenusEvent;

        private void Awake()
        {
            computerGroup.transform.localScale = new Vector3(1f, 0f, 1f);
            fridgeGroup.transform.localScale = new Vector3(1f, 0f, 1f);

            canvasGroups = new() 
            { 
                computerGroup,
                fridgeGroup
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
                case InteractableType.Computer:
                    HandleMenu(eventData.Opening, computerGroup, eventData.CursorPosition);
                    break;

                case InteractableType.Fridge:
                    HandleMenu(eventData.Opening, fridgeGroup, eventData.CursorPosition);
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
        private void HandleMenu(bool opening, CanvasGroup canvasGroup, Vector2 cursorPosition)
        {
            // Close all menus
            CloseMenus();

            // Check if opening the menu
            if (opening)
                // If so, show the menu
                ShowMenu(canvasGroup, cursorPosition);
            else
                // Otherwise, hide the menu
                HideMenu(canvasGroup);
        }

        /// <summary>
        /// Close all interaction menus
        /// </summary>
        private void CloseMenus()
        {
            foreach(CanvasGroup canvasGroup in canvasGroups)
            {
                // Hide the canvas group
                HideMenu(canvasGroup);
            }
        }

        /// <summary>
        /// Show the menu
        /// </summary>
        private void ShowMenu(CanvasGroup canvasGroup, Vector2 cursorPosition)
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

            // Pause the Calendar
            EventBus<ChangeCalendarPauseState>.Raise(new ChangeCalendarPauseState()
            {
                Paused = true,
                AllowSpeedChanges = false
            });
        }

        /// <summary>
        /// Hide the menu
        /// </summary>
        private void HideMenu(CanvasGroup canvasGroup)
        {
            // Scale down to 0
            Scale(canvasGroup, 0f, fadeDuration);

            // Fade to 0
            Fade(canvasGroup, 0f, fadeDuration, () =>
            {
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
            });
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