using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearObjective : Objective {
    //Derrives all functionality from base class/used as a wrapper

    ////////////////////////        GIZMO       //////////////////////// 
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.5f);
        if(objectiveType == ObjectiveType.Interact)
        {
            Vector3 temp = objectToInteract.transform.position;
            temp += new Vector3(0, objectToInteract.GetComponent<MeshFilter>().sharedMesh.bounds.size.y / 2 + objectToInteract.transform.localScale.y / 2, 0);
            Gizmos.DrawLine(transform.position, temp);
            Gizmos.DrawCube(temp, new Vector3(0.25f,0.25f,0.25f));
        }
        if (objectiveType == ObjectiveType.Destination)
        {
            Vector3 temp = destinationToReach.transform.position;
            temp += new Vector3(0, objectToInteract.GetComponent<MeshFilter>().sharedMesh.bounds.size.y / 2, 0);
            Gizmos.DrawLine(transform.position, temp);
            Gizmos.DrawCube(temp, new Vector3(0.25f, 0.25f, 0.25f));
        }
        if (nextObjective != null)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawLine(transform.position, nextObjective.transform.position);
        }
    }
}
