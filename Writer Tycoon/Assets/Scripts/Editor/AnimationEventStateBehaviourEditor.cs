using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace GhostWriter.Animations.Editors
{
    [CustomEditor(typeof(AnimationEventStateBehaviour))]
    public class AnimationEventStateBehaviourEditor : Editor
    {
        private AnimationClip previewClip;
        private float previewTime;

        public override void OnInspectorGUI()
        {
            // Draw the default inspector
            DrawDefaultInspector();

            // Get the target as the correct component type
            AnimationEventStateBehaviour stateBehaviour = (AnimationEventStateBehaviour)target;

            if(Validate(stateBehaviour, out string errorMessage))
            {
                GUILayout.Space(10f);

                PreviewAnimationClip(stateBehaviour);

                GUILayout.Label($"Preview at {previewTime:F2}s", EditorStyles.helpBox);
            } else
            {
                EditorGUILayout.HelpBox(errorMessage, MessageType.Info);
            }
        }

        private void PreviewAnimationClip(AnimationEventStateBehaviour stateBehaviour)
        {
            // Exit case - the preview clip is null
            if (previewClip == null) return;

            previewTime = stateBehaviour.triggerTime * previewClip.length;

            AnimationMode.StartAnimationMode();
            AnimationMode.SampleAnimationClip(Selection.activeGameObject, previewClip, previewTime);
            AnimationMode.StopAnimationMode();
        }

        private bool Validate(AnimationEventStateBehaviour stateBehaviour, out string errorMessage)
        {
            // Get a valid AnimatorController if possible
            AnimatorController animatorController = GetValidAnimatorController(out errorMessage);

            // If there's no AnimatorController, return with the already processed error message
            if (animatorController == null) return false;

            // Find the target state behaviour
            ChildAnimatorState matchingState = animatorController.layers
                .SelectMany(layer => layer.stateMachine.states)
                .FirstOrDefault(state => state.state.behaviours.Contains(stateBehaviour));

            // Set the preview clip
            previewClip = matchingState.state?.motion as AnimationClip;

            // Check if the preview clip is null
            if (previewClip == null)
            {
                // If so, notify and return
                errorMessage = "No valid AnimationClip found for the current state";
                return false;
            }

            return true;
        }

        private AnimatorController GetValidAnimatorController(out string errorMessage)
        {
            // Start with an empty string
            errorMessage = string.Empty;

            // Check what object is selected in the hierarchy
            GameObject targetGameObject = Selection.activeGameObject;

            // Check if the target GameObject is null
            if(targetGameObject == null)
            {
                // Notify and return
                errorMessage = "Please select a GameObject with an Animator to preview";
                return null;
            }

            // Check if the GameObject has an Animator
            Animator animator = targetGameObject.GetComponent<Animator>();
            if(animator == null)
            {
                // If not, then notify and return
                errorMessage = "The selected GameObject deoes not have an Animator component";
                return null;
            }

            // Check if the Animator actually has an AnimatorController
            AnimatorController animatorController = animator.runtimeAnimatorController as AnimatorController;
            if(animatorController == null)
            {
                // If not, then notify and return
                errorMessage = "The selected Animator does not have a valid AnimatorController";
                return null;
            }

            // Return the animator contrller
            return animatorController;
        }
    }
}
