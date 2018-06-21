using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MultiObjective))]
[CanEditMultipleObjects]
public class MultiObjectiveEditor : Editor {

    SerializedObject serializedObj;
    MultiObjective objective;
    SerializedProperty processes;
    SerializedProperty objectives;// = objective.objectiveOptions;
    void OnEnable()
    {
        serializedObj = new SerializedObject(target);
        objective = (MultiObjective)target;
        processes = serializedObject.FindProperty("processesOnCompletion");
        objectives = serializedObject.FindProperty("objectives");
    }

    public override void OnInspectorGUI()
    {
        EditorStyles.label.wordWrap = true;
        EditorGUILayout.LabelField("This objective type allows for multiple conditions to be met before completion");
        serializedObj.Update();
        #region default variabls
        objective.objectiveName = EditorGUILayout.TextField("Objective Name", objective.objectiveName);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Description");
        objective.description = EditorGUILayout.TextField(objective.description, GUILayout.Height(30));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        objective.status = (Objective.Status)EditorGUILayout.EnumPopup("Status: ", objective.status);

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(processes, new GUIContent("Actions On Completion"), true);

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(objectives, new GUIContent("Objectives"), true);

        EditorGUILayout.Space();

        objective.nextObjective = (GameObject)EditorGUILayout.ObjectField("Next Objective", objective.nextObjective, typeof(GameObject), true);

        #endregion default variabls

        serializedObject.ApplyModifiedProperties();
    }
}
