using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveMarkerManager : MonoBehaviour {

    public GameObject objectiveMarker;
    private List<GameObject> markers;
    private IObjective markedObjective;

    void Start () {
        markers = new List<GameObject>();
	}
	
	void Update () {
        if (markedObjective != ObjectiveTreeController.trees[ObjectiveTreeController.focusedTree].currentObjective)     //Check if focused objective changed
        {
            markedObjective = ObjectiveTreeController.trees[ObjectiveTreeController.focusedTree].currentObjective;      //set new objective
            foreach (GameObject marker in markers)                                                                      //destroy old markers
            {
                Destroy(marker);
            }
            markers.Clear();

            if (ObjectiveTreeController.trees[ObjectiveTreeController.focusedTree].currentObjective != null)
            {
                foreach (Vector3 pos in markedObjective.GetPosition())                                                //create new markers
                {
                    //Vector3 temp = new Vector3(0, pos.GetComponent<MeshFilter>().mesh.bounds.size.y / 2 + pos.localScale.y/2,0);
                    //temp += pos.position;
                    markers.Add(Instantiate(objectiveMarker, pos, Quaternion.identity, transform));
                }
            }
        }
	}
}
