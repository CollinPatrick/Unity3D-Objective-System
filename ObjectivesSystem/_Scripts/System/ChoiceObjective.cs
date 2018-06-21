using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Impliment: Optional objectives which don't count towards completion
/// </summary>

//Do this, this, or this
public class ChoiceObjective : Objective, IObjective {

    //all objective options
    public ObjectiveOption[] objectiveOptions;

    /// <summary>
    /// Checks each objective option if completion requirement is met
    /// </summary>
    /// <param name="obj"></param>
    public override void CheckForCompletion(GameObject obj)
    {
        foreach(ObjectiveOption option in objectiveOptions)
        {
            if(option.objectiveType == ObjectiveType.Interact)
            {
                if(option.objectToInteract == obj)
                {
                    OnCompletion();
                    break;
                }
            }
            else if(option.objectiveType == ObjectiveType.Destination)
            {
                if (option.destinationToReach == obj)
                {
                    OnCompletion();
                    break;
                }
            }
        }
    }

    public override Vector3[] GetPosition()
    {
        Vector3[] temp = new Vector3[objectiveOptions.Length];
        for(int i = 0; i<objectiveOptions.Length; i++)
        {
            if (objectiveOptions[i].objectiveType == ObjectiveType.Interact)
            {
                Vector3 tempPos = objectiveOptions[i].objectToInteract.transform.position;
                tempPos += new Vector3(0, objectiveOptions[i].objectToInteract.GetComponent<MeshFilter>().mesh.bounds.size.y / 2 + objectiveOptions[i].objectToInteract.transform.localScale.y / 2, 0);

                temp[i] = tempPos;
            }
            else if (objectiveOptions[i].objectiveType == ObjectiveType.Destination)
            {
                temp[i] = objectiveOptions[i].destinationToReach.transform.position;
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
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.5f);
        for(int i = 0; i<objectiveOptions.Length; i++)
        {
            if (objectiveOptions[i].objectiveType == ObjectiveType.Interact)
            {
                Gizmos.DrawLine(transform.position, objectiveOptions[i].objectToInteract.transform.position + Vector3.up);
                Gizmos.DrawCube(objectiveOptions[i].objectToInteract.transform.position + Vector3.up, new Vector3(0.25f, 0.25f, 0.25f));
            }
            else if(objectiveOptions[i].objectiveType == ObjectiveType.Destination)
            {
                Vector3 temp = objectiveOptions[i].destinationToReach.transform.position;
                temp += new Vector3(0, objectiveOptions[i].destinationToReach.GetComponent<MeshFilter>().sharedMesh.bounds.size.y / 2, 0);
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
/// Data container for each objective option
/// </summary>
[System.Serializable]
public struct ObjectiveOption
{
    public Objective.ObjectiveType objectiveType;
    public GameObject objectToInteract;
    public GameObject destinationToReach;
}
