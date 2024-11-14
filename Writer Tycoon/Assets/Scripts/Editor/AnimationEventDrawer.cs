using UnityEngine;
using UnityEditor;

namespace GhostWriter.Animations.Editors
{
    [CustomPropertyDrawer(typeof(AnimationEvent))]
    public class AnimationEventDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Begin the property block
            EditorGUI.BeginProperty(position, label, property);

            // Get the serialized properties
            SerializedProperty stateNameProperty = property.FindPropertyRelative("eventName");
            SerializedProperty stateEventProperty = property.FindPropertyRelative("OnAnimationEvent");

            // Create rects for the properties
            Rect stateNameRect = new(
                position.x, 
                position.y, 
                position.width, 
                EditorGUIUtility.singleLineHeight
            );
            Rect stateEventRect = new(
                position.x, 
                position.y + EditorGUIUtility.singleLineHeight + 2, 
                position.width, 
                EditorGUI.GetPropertyHeight(stateEventProperty)
            );

            // Draw the property fields
            EditorGUI.PropertyField(stateNameRect, stateNameProperty);
            EditorGUI.PropertyField(stateEventRect, stateEventProperty, true);

            // End the property block
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            SerializedProperty stateEventProperty = property.FindPropertyRelative("OnAnimationEvent");
            return EditorGUIUtility.singleLineHeight + EditorGUI.GetPropertyHeight(stateEventProperty) + 4f;
        }
    }
}