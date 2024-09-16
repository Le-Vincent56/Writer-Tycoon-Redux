using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using WriterTycoon.Patterns.EventBus;

namespace WriterTycoon.World.Interactables.UI
{
    public class InteractionUI : MonoBehaviour
    {
        [SerializeField] private float fadeDuration;
        [SerializeField] private CanvasGroup computerGroup;
        [SerializeField] private CanvasGroup fridgeGroup;
        private HashSet<CanvasGroup> canvasGroups;

        private Tween fadeTween;

        private EventBinding<HandleInteractMenu> showInteractMenuEvent;

        private void Awake()
        {
            canvasGroups = new() 
            { 
                computerGroup,
                fridgeGroup
            };
        }

        private void OnEnable()
        {
            showInteractMenuEvent = new EventBinding<HandleInteractMenu>(HandleInteractionMenus);
            EventBus<HandleInteractMenu>.Register(showInteractMenuEvent);
        }

        private void OnDisable()
        {
            EventBus<HandleInteractMenu>.Deregister(showInteractMenuEvent);
        }

        /// <summary>
        /// Callback function to handle the interaction menus
        /// </summary>
        /// <param name="eventData"></param>
        private void HandleInteractionMenus(HandleInteractMenu eventData)
        {
            Debug.Log("Handling menu");

            switch (eventData.InteractableType)
            {
                case InteractableType.Computer:
                    HandleMenu(eventData.Opening, computerGroup);
                    break;

                case InteractableType.Fridge:
                    HandleMenu(eventData.Opening, fridgeGroup);
                    break;
            }
        }

        /// <summary>
        /// Handle the opening and closing of the menu
        /// </summary>
        private void HandleMenu(bool opening, CanvasGroup canvasGroup)
        {
            // Close all other menus
            CloseOtherMenus(canvasGroup);

            // Check if opening the menu
            if (opening)
                // If so, show the menu
                ShowMenu(canvasGroup);
            else
                // Otherwise, hide the menu
                HideMenu(canvasGroup);
        }

        /// <summary>
        /// Close all other menus besides the current one
        /// </summary>
        /// <param name="currentGroup"></param>
        private void CloseOtherMenus(CanvasGroup currentGroup)
        {
            foreach(CanvasGroup canvasGroup in canvasGroups)
            {
                // Exit case - if the canvas group equals the current group
                if (canvasGroup == currentGroup) continue;

                // Hide the canvas group
                HideMenu(canvasGroup);
            }
        }

        /// <summary>
        /// Show the menu
        /// </summary>
        /// <param name="canvasGroup"></param>
        private void ShowMenu(CanvasGroup canvasGroup)
        {
            Fade(canvasGroup, 1f, fadeDuration, () =>
            {
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
            });
        }

        /// <summary>
        /// Hide the menu
        /// </summary>
        /// <param name="canvasGroup"></param>
        private void HideMenu(CanvasGroup canvasGroup)
        {
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
            // Kill the current fade tween if it exists
            fadeTween?.Kill(false);

            // Set the fade animation
            fadeTween = canvasGroup.DOFade(endFadeValue, duration)
                .SetEase(easeType);

            // Exit case - if there is no given Tween Callback
            if (onEnd == null) return;

            // Hook up callback events
            fadeTween.onComplete += onEnd;
        }
    }
}