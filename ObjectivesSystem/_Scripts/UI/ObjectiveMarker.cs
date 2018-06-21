using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveMarker : MonoBehaviour {

    private Transform player;
    public float maxDistance = 50;
    public float minDistance = 5;
    private Image image;

    void Start () {
        image = transform.GetChild(0).GetComponent<Image>();
    }
	
	void Update () {
        //Makes canvas always point towards the main camera
        transform.LookAt(new Vector3(Camera.main.transform.position.x, transform.position.y, Camera.main.transform.position.z));

        //sets marker image alpha depending on min/max distance between main camera and self
        if (Vector3.Distance(transform.position, Camera.main.transform.position) < maxDistance)
        {
            float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
            float alpha = 1.0f - Mathf.Clamp01((distance - minDistance) / (maxDistance - minDistance));
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
        }
    }
}
