using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class IsometricObject : MonoBehaviour {

    [Serializable]
    public class IsoScale
    {
        public float xYFactor;      // The size of one isometric square in Unity Space
        public float heightFactor;  // The size of one unit of height in Unity Space
    }

    [Serializable]
    public class IsoPosition
    {
        public float isoX;      // Object's X position in Isometric Space
        public float isoY;      // Object's Y position in Isometric Space
        public float isoHeight; // Object's Height in Isometric Space
        public float drawDepth; // Object's Z position in Unity Space for drawing layers 
    }

    // Declare Fields
    public IsoScale     isoScale;
    public IsoPosition  isoPosition;
    private Vector3     realPosition; // Object's transform position in Unity Space

    // Use this for initialization
    void Start()
    {
        realPosition = new Vector3();
        UpdateRealPosition();
    }

    void OnValidate ()
    {
        UpdateRealPosition();
    }

    // Set object's real position based on its position in Isometric Space
    public void UpdateRealPosition()
    {
        realPosition.Set(
            //Set X
            isoScale.xYFactor * isoPosition.isoX -
            isoScale.xYFactor * isoPosition.isoY,
            //Set Y
            0.5f * isoScale.xYFactor * isoPosition.isoX +
            0.5f * isoScale.xYFactor * isoPosition.isoY +
            isoScale.heightFactor * isoPosition.isoHeight,
            //Set Z
            isoPosition.drawDepth
            );

        gameObject.transform.position = realPosition;
    }
}
