using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Impliment: Optional objectives which don't count towards completed total
/// </summary>

//Do this, this, and this
public class MultiObjective : Objective {

    //All objective options
    public SingleObjective[] objectives;

    //counter for checking how many objectives have been completed/removes need for another loop
    public int objectivesCompleted = 0;

    /// <summary>
    /// loops through all required objectives and checks for match between passed gameobject and objective requirement
    /// runs OnComplete method after determining all objectives have been completed
    /// </summary>
    /// <param name="obj"></param>
    public override void CheckForCompletion(GameObject obj)
    {
        for(int i = 0; i<objectives.Length; i++)
        {
            if(objectives[i].objectiveType == ObjectiveType.Interact)
            {
                if(obj == objectives[i].objectToInteract && objectives[i].isCompleted == false)
                {
                    objectives[i].isCompleted = true;
                    //Destroy(obj.GetComponentInChildren<ObjectiveMarker>().gameObject);   //destroys un needed marker //NEEDS FIXED
                    objectivesCompleted++;
                    Debug.Log(objectivesCompleted + "/" + objectives.Length + " objectives completed.");
                }
            }
            else if (objectives[i].objectiveType == ObjectiveType.Destination)
            {
                if (obj == objectives[i].destinationToReach && objectives[i].isCompleted == false)
                {
                    objectives[i].isCompleted = true;
                    //Destroy(obj.GetComponentInChildren<ObjectiveMarker>().gameObject);   //destroys un needed marker //NEEDS FIXED
                    objectivesCompleted++;
                    Debug.Log(objectivesCompleted + "/" + objectives.Length + " objectives completed.");
                }
            }
        }

        if(objectivesCompleted >= objectives.Length)
        {
            OnCompletion();
        }
    }

    public override Vector3[] GetPosition()
    {
        Vector3[] temp = new Vector3[objectives.Length];
        for (int i = 0; i < objectives.Length; i++)
        {
            if (objectives[i].objectiveType == ObjectiveType.Interact)
            {
                Vector3 tempPos = objectives[i].objectToInteract.transform.position;
                tempPos+= new Vector3(0, objectives[i].objectToInteract.GetComponent<MeshFilter>().mesh.bounds.size.y / 2 + objectives[i].objectToInteract.transform.localScale.y / 2, 0);

                temp[i] = tempPos;
            }
            else if (objectives[i].objectiveType == ObjectiveType.Destination)
            {
                temp[i] = objectives[i].destinationToReach.transform.position;
            }
            else
            {
                temp[i] = Vector3.zero;
            }
        }
        return temp;
    }

    ////////////////////////        GIZMO       //////////////////////// 
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, 0.5f);
        for (int i = 0; i < objectives.Length; i++)
        {
            if (objectives[i].objectiveType == ObjectiveType.Interact)
            {
                Gizmos.DrawLine(transform.position, objectives[i].objectToInteract.transform.position + Vector3.up);
                Gizmos.DrawCube(objectives[i].objectToInteract.transform.position + Vector3.up, new Vector3(0.25f, 0.25f, 0.25f));
            }
            else if (objectives[i].objectiveType == ObjectiveType.Destination)
            {
                Vector3 temp = objectives[i].destinationToReach.transform.position;
                temp += new Vector3(0, objectives[i].destinationToReach.GetComponent<MeshFilter>().sharedMesh.bounds.size.y / 2, 0);
                Gizmos.DrawLine(transform.position, temp);
                Gizmos.DrawCube(temp, new Vector3(0.25f, 0.25f, 0.25f));
            }
        }
        if (nextObjective != null)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawLine(transform.position, nextObjective.transform.position);
        }
    }
}

/// <summary>
/// Data container for each indivual objective
/// </summary>
[System.Serializable]
public struct SingleObjective
{
    public Objective.ObjectiveType objectiveType;
    public GameObject objectToInteract;
    public GameObject destinationToReach;
    public bool isCompleted;
}
