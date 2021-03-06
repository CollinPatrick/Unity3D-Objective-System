﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(SingleObjective))]
public class SingleObjectiveStructEditor : PropertyDrawer {

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        var typeRect = new Rect(position.x, position.y, 75, position.height);
        var objectRect = new Rect(position.x + 80, position.y, 260, position.height);
        var flagRect = new Rect(position.x + 355, position.y, 25, position.height);

        EditorGUI.PropertyField(typeRect, property.FindPropertyRelative("objectiveType"), GUIContent.none);

        if (property.FindPropertyRelative("objectiveType").enumValueIndex == 0)
        {
            EditorGUI.PropertyField(objectRect, property.FindPropertyRelative("objectToInteract"), new GUIContent("Object To Interact"));
        }
        else if (property.FindPropertyRelative("objectiveType").enumValueIndex == 1)
        {
            EditorGUI.PropertyField(objectRect, property.FindPropertyRelative("destinationToReach"), new GUIContent("Destination To Reach"));
        }

        EditorGUI.PropertyField(flagRect, property.FindPropertyRelative("isCompleted"), GUIContent.none);

        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}
