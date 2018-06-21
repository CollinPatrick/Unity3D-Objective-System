using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ChoiceObjective))]
[CanEditMultipleObjects]
public class ChoiceObjectiveEditor : Editor {

    SerializedObject serializedObj;
    ChoiceObjective objective;
    SerializedProperty processes;
    SerializedProperty options;
    void OnEnable()
    {
        serializedObj = new SerializedObject(target);
        objective = (ChoiceObjective)target;
        processes = serializedObject.FindProperty("processesOnCompletion"); //retrieves array for processes on completion
        options = serializedObject.FindProperty("objectiveOptions");        //retrieves struct array for objective options
    }

    public override void OnInspectorGUI()
    {
        EditorStyles.label.wordWrap = true;
        EditorGUILayout.LabelField("This objective type allows for multiple complete conditions that all lead into the same next objective");

        EditorGUILayout.Space();

        serializedObj.Update();
        #region default variabls
        objective.objectiveName = EditorGUILayout.TextField("Objective Name", objective.objectiveName);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Description");
        objective.description = EditorGUILayout.TextField(objective.description, GUILayout.Height(45));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        objective.status = (Objective.Status)EditorGUILayout.EnumPopup("Status: ", objective.status);

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(processes, new GUIContent("Actions On Completion"), true);

        EditorGUILayout.Space();
        
        EditorGUILayout.PropertyField(options, new GUIContent("Objective Options"), true);

        EditorGUILayout.Space();

        objective.nextObjective = (GameObject)EditorGUILayout.ObjectField("Next Objective", objective.nextObjective, typeof(GameObject), true);

        #endregion default variabls

        serializedObject.ApplyModifiedProperties();
    }
}
