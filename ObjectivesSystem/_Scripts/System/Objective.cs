using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Impliment: more processes on completion and functionality
/// </summary>

//Base class and functionality of all objective types

public class Objective : MonoBehaviour, IObjective {

    public string objectiveName;
    [Multiline(3)]
    public string description;

    private ObjectiveTree myTree;

    public enum Status
    {
        Inactive,       //locked behind other objective or alternate objective path
        Pending,        //Currently active objective
        Completed       //Already completed
    }
    public Status status = Status.Inactive;

    public enum ProcessesOnCompletion
    {
        PlayCutscene,
        PlayCinematic,
        MarkAsComplete
    }
    public ProcessesOnCompletion[] processesOnCompletion;

    public enum ObjectiveType
    {
        Interact,
        Destination,
        Dialogue_Placeholder        //used when integrating dialoge - detects when a dialogue event is reached
    }
    public ObjectiveType objectiveType;

    //visability controlled through editor script depending on objective type
    public GameObject objectToInteract;
    public GameObject destinationToReach;

    //The objective marked to run after this one
    public GameObject nextObjective;

    ///returns the next objective's interface
    public virtual IObjective GetNextObjective()
    {
        if (nextObjective != null)
            return nextObjective.GetComponent<IObjective>();
        else
            return null;
    }

    ///shortcut ONLY USED FOR GIZMO to directly obtain the objective object
    public virtual GameObject GetNextObjectiveObject()
    {
        return nextObjective;
    }

    /// <summary>
    /// Sets up objective for use
    /// finds and Sets this objectives parent tree
    /// throws error if tree cannot be found
    /// </summary>
    void Start()
    {
        Transform temp = gameObject.transform;
        while (myTree == null)
        {            
            try
            {
                temp = temp.transform.parent;   
                if (temp.gameObject.GetComponent<ObjectiveTree>())
                {
                    myTree = temp.GetComponent<ObjectiveTree>();
                }
            }
            catch (System.Exception)
            {
                Debug.LogError("Objective: \"" + objectiveName + "\" is not parented to an objective tree.");
                break;
            }
        }
    }

    ///Generic method for retrieving parent tree
    public virtual ObjectiveTree GetTree()
    {
        return myTree;
    }

    ///Generic method for setting up objective and running start processes when set to current objective
	public virtual void StartObjective()
    {
        Debug.Log("Starting Objective");
    }

    /// Used by tree to set status of objective
    public virtual void SetStatus(Status stat)
    {
        status = stat;
    }

    ///Generic method to get current status of objective 
    public Status GetStatus()
    {
        return status;
    }

    public virtual string GetName()
    {
        return objectiveName;
    }

    public virtual string GetDescription()
    {
        return description;
    }

    public virtual Vector3[] GetPosition()
    {
        if(objectiveType == ObjectiveType.Interact)
        {
            Vector3 temp = objectToInteract.transform.position;
            temp += new Vector3(0, objectToInteract.GetComponent<MeshFilter>().mesh.bounds.size.y / 2 + objectToInteract.transform.localScale.y/2, 0);
            return new Vector3[] { temp };
        }
        else if (objectiveType == ObjectiveType.Destination)
        {
            Vector3 temp = destinationToReach.transform.position;
            temp += new Vector3(0, objectToInteract.GetComponent<MeshFilter>().mesh.bounds.size.y/2, 0);
            return new Vector3[] { temp };
        }
        else
        {
            return null;
        }
    }

    ///Generic method to check if objective completion requirements were met 
    public virtual void CheckForCompletion(GameObject obj)
    {
        if (objectiveType == ObjectiveType.Interact)
        {
            if (obj == objectToInteract)
            {
                OnCompletion();
            }
        }
        else if (objectiveType == ObjectiveType.Destination)
        {
            if(obj == destinationToReach)
            {
                OnCompletion();
            }
        }
    }

    ///Generic method to handle what happens when an objective is completed
    public virtual void OnCompletion()
    {
        if (processesOnCompletion.Contains(ProcessesOnCompletion.PlayCutscene))
        {
            Debug.Log("Playing Cutscene");
        }
        if (processesOnCompletion.Contains(ProcessesOnCompletion.PlayCinematic))
        {
            Debug.Log("Playing Cinematic");
        }
        if (processesOnCompletion.Contains(ProcessesOnCompletion.MarkAsComplete))
        {
            Debug.Log("Marked as complete");
            myTree.GetComponent<ObjectiveTree>().SetNextObjective();
            //gameObject.SetActive(false);
        }
        GetTree().transform.parent.GetComponent<ObjectivesUIManager>().Message("Completed: " + GetName());
    }
}
