using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//universal objective methods
public interface IObjective {

    void StartObjective();
    void OnCompletion();
    void SetStatus(Objective.Status stat);
    Objective.Status GetStatus();
    ObjectiveTree GetTree();
    IObjective GetNextObjective();
    GameObject GetNextObjectiveObject(); //Used only for gizmo
    void CheckForCompletion(GameObject obj);
    string GetName();
    string GetDescription();
    Vector3[] GetPosition();//Used only for objective marker
}
