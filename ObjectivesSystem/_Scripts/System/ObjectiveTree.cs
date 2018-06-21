using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Impliment: loading current objective from file
/// Impliment: options after tree runs out of objectives
/// </summary>
public class ObjectiveTree : MonoBehaviour
{

    public string treeName;
    public IObjective currentObjective;

    /// <summary>
    /// sets the first child under the tree as the current objective 
    /// throws an error if a valid objective is not present
    /// </summary>
	void Start()
    {
        try
        {
            currentObjective = transform.GetChild(0).gameObject.GetComponent<IObjective>();
            currentObjective.SetStatus(Objective.Status.Pending);
            Debug.Log("Current Objective: " + currentObjective);
        }
        catch (System.Exception)
        {
            Debug.LogWarning("Objective Tree \"" + treeName + "\" is empty or there is an invalid object assigned as a child. "
                + "\n" + "Assign only children with an objective component under a tree object.");
        }
    }

    /// <summary>
    /// Marks objective as completed
    /// checks if another objective exists and sets it as the current objective
    /// </summary>
    public void SetNextObjective()
    {
        currentObjective.SetStatus(Objective.Status.Completed);
        if (currentObjective.GetNextObjective() != null)
        {
            currentObjective = currentObjective.GetNextObjective();
            currentObjective.SetStatus(Objective.Status.Pending);
            Debug.Log("Current Objective: " + currentObjective);
            currentObjective.StartObjective();
        }
        else
        {
            currentObjective = null;
            Debug.Log("All objective Completed");
        }

    }

    ////////////////////////        GIZMO       ////////////////////////        *OBSOLETE*
    /*void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        GizmoDrawBranch(transform.GetChild(0).gameObject);
    }

    //recursive method to draw each branch segment
    void GizmoDrawBranch(GameObject obj)
    {
        if (obj.GetComponent<IObjective>().GetNextObjectiveObject() != null)
        {
            Gizmos.DrawLine(obj.transform.position, obj.GetComponent<IObjective>().GetNextObjectiveObject().transform.position);
            GizmoDrawBranch(obj.GetComponent<IObjective>().GetNextObjectiveObject());
        }
        
        if(obj.GetComponent<BranchingObjective>())
        {
            for (int i = 0; i<obj.transform.childCount;i++)
            {
                Gizmos.DrawLine(obj.transform.position, obj.transform.GetChild(i).transform.position);
                GizmoDrawBranch(obj.transform.GetChild(i).gameObject);
            }
        }
    }*/
}

