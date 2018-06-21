using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ObjectiveOption))]
public class ChoiceStructEditor : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        var typeRect = new Rect(position.x, position.y, 100, position.height);
        var objectRect = new Rect(position.x + 105, position.y, 285, position.height);

        EditorGUI.PropertyField(typeRect, property.FindPropertyRelative("objectiveType"), GUIContent.none);

        if(property.FindPropertyRelative("objectiveType").enumValueIndex == 0)
        {
            EditorGUI.PropertyField(objectRect, property.FindPropertyRelative("objectToInteract"), new GUIContent("Object To Interact"));
        }
        else if(property.FindPropertyRelative("objectiveType").enumValueIndex == 1)
        {
            EditorGUI.PropertyField(objectRect, property.FindPropertyRelative("destinationToReach"), new GUIContent("Destination To Reach"));
        }

        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}
