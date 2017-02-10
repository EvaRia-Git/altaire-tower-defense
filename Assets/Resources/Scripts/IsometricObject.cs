using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class IsometricObject : MonoBehaviour {

        public float    isoScale;
        public Vector3  isoPosition;
        private Vector3 realPosition;

    // Use this for initialization
    void Start ()
    {
        realPosition = new Vector3();
    }

    // Update is called once per frame
    void Update ()
    {
        realPosition.Set(
            isoScale * isoPosition.x - isoScale * isoPosition.y,                //X
            0.5f * isoScale * isoPosition.x + 0.5f * isoScale * isoPosition.y,  //Y
            isoPosition.z                                                       //Z
            );

        gameObject.transform.position = realPosition;
    }
}
