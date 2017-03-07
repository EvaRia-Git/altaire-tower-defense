using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class WASDCameraScroll : MonoBehaviour {
    public float scrollSpeed;
    private Transform cameraTransform;

	// Use this for initialization
	void Start () {
        cameraTransform = gameObject.GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update () {
        if (cameraTransform != null)
        {
            if (Input.GetButton("W")) cameraTransform.position += Vector3.up * scrollSpeed;
            if (Input.GetButton("A")) cameraTransform.position += Vector3.left * scrollSpeed;
            if (Input.GetButton("S")) cameraTransform.position += Vector3.down * scrollSpeed;
            if (Input.GetButton("D")) cameraTransform.position += Vector3.right * scrollSpeed;
        }
    }
}
