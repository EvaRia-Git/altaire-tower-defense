using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WASDcameraScroll : MonoBehaviour {
    public float scrollSpeed;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if (Camera.current != null)
        {
            Vector3 position = Camera.current.transform.position;
            if (Input.GetButton("W")) Camera.current.transform.position = new Vector3(position.x, position.y + scrollSpeed, position.z);
            if (Input.GetButton("A")) Camera.current.transform.position = new Vector3(position.x - scrollSpeed, position.y, position.z);
            if (Input.GetButton("S")) Camera.current.transform.position = new Vector3(position.x, position.y - scrollSpeed, position.z);
            if (Input.GetButton("D")) Camera.current.transform.position = new Vector3(position.x + scrollSpeed, position.y, position.z);
        }

    }
}
