using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveTreeController : MonoBehaviour {

    public static ObjectiveTree[] trees;
    public static int focusedTree;

	void Start () {
        trees = FindObjectsOfType<ObjectiveTree>();
        focusedTree = 0;
	}

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Z))
        {
            if (focusedTree > 0)
            {
                focusedTree--;
            }
        }
        if (Input.GetKeyUp(KeyCode.X))
        {
            if (focusedTree < trees.Length-1)
            {
                focusedTree++;
            }
        }
    }
}


