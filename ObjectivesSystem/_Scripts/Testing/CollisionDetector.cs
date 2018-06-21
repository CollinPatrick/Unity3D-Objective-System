using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour {

	void OnTriggerEnter(Collider other)
    {
        foreach(ObjectiveTree tree in ObjectiveTreeController.trees)
        {
            tree.currentObjective.CheckForCompletion(other.gameObject); //breaks when a tree is completed
        }
    }
}
