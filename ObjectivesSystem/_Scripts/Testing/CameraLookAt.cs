using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraLookAt : MonoBehaviour {

    public Image dot;       ///dot used to represent the mouse cursor locked to the center of the screen
    public LayerMask mask;  ///Prevents raycast from hitting player collider

    /// <summary>
    /// Basic raycast to detect what object in the center of the screen
    /// </summary>
	void Update () {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, mask))
        {
            if (hit.transform.tag == "Interactable")
            {
                dot.color = Color.yellow;
                if (Input.GetKeyUp(KeyCode.E))
                {
                    InteractObjective(hit);
                }
            }
            else
            {
                dot.color = Color.black;
            }
        }
    }

    /// <summary>
    /// Handles what to do when interacting with an objective object
    /// </summary>
    /// <param name="hit"></param>
    private void InteractObjective(RaycastHit hit)
    {
        InteractObjectiveComponent component = hit.transform.gameObject.GetComponent<InteractObjectiveComponent>();
        if (component != null)
        {
            foreach(ObjectiveTree tree in ObjectiveTreeController.trees)
            {
                if (tree.currentObjective != null)
                {
                    tree.currentObjective.CheckForCompletion(hit.transform.gameObject);
                }
            }
        }
    }
}
