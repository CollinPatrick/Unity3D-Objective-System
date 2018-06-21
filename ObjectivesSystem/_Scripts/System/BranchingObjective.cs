using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//Children of this objective are the different branches
public class BranchingObjective : Objective {
    /// <summary>
    /// loops through all children or "branches"
    ///     sets next objective to branche's nexr objective(PATCH)
    ///     runs branche's check for completion
    ///     if all objective requirements for branch are completed, select branch as new objective path
    /// </summary>
    /// <param name="obj"></param>
    public override void CheckForCompletion(GameObject obj)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            nextObjective = transform.GetChild(i).GetComponent<Objective>().nextObjective;      //Patch to set current objective to the branch being checked 
            transform.GetChild(i).GetComponent<IObjective>().CheckForCompletion(obj);
            if(transform.GetChild(i).GetComponent<IObjective>().GetStatus() == Status.Completed)
            {
                for (int k = 0; k < transform.childCount; k++)
                {
                    if(transform.GetChild(k).gameObject != transform.GetChild(i).gameObject)
                    {
                        transform.GetChild(k).GetComponent<IObjective>().SetStatus(Status.Inactive);
                    }
                }
                break;
            }
        }
    }

    /// <summary>
    /// sets akk branches status to pending
    /// </summary>
    public override void StartObjective()
    {
        base.StartObjective();
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<IObjective>().SetStatus(Status.Pending);
        }
    }

    public override Vector3[] GetPosition()
    {
        List<Vector3> temp = new List<Vector3>();
        for (int i = 0; i < transform.childCount; i++)
        {
            Vector3[] positions = transform.GetChild(i).GetComponent<IObjective>().GetPosition();
            foreach(Vector3 pos in positions)
            {
                temp.Add(pos);
            }
        }
        return temp.ToArray();
    }

    ////////////////////////        GIZMO       //////////////////////// 
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 0.5f);

        for(int i = 0; i<transform.childCount; i++)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawLine(transform.position, transform.GetChild(i).position);
        }
    }
}
