using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BranchingObjective))]
public class BranchingObjectiveEditor : Editor {

    BranchingObjective objective;
    void OnEnable()
    {
        objective = (BranchingObjective)target;
    }

    public override void OnInspectorGUI()
    {
        EditorStyles.label.wordWrap = true;
        EditorGUILayout.LabelField("This objective type allows for branching paths depending on what objective is completed");

        EditorGUILayout.Space();

        #region default variabls
        objective.objectiveName = EditorGUILayout.TextField("Objective Name", objective.objectiveName);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Description");
        objective.description = EditorGUILayout.TextField(objective.description, GUILayout.Height(45));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        objective.status = (Objective.Status)EditorGUILayout.EnumPopup("Status: ", objective.status);

        EditorGUILayout.Space();
        
        EditorGUILayout.LabelField("Branching Objectives: ");     
        for (int i = 0; i < objective.transform.childCount; i++)
        {
            EditorGUI.indentLevel = 1;
            EditorGUILayout.LabelField(objective.transform.GetChild(i).GetComponent<Objective>().objectiveName);
            EditorGUI.indentLevel = 2;
            EditorGUILayout.LabelField(objective.transform.GetChild(i).GetComponent<Objective>().description);
        }
        EditorGUI.indentLevel = 0;
        #endregion default variabls
    }
}
