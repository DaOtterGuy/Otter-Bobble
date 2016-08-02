using UnityEngine;
using System.Collections;

// **************************************************************************
// Creator: Ryan Gainford
// Last Updated: June 6, 2016
//
// Stops an object from rotating. Used mostly to keep the hover enemy sprite
// from changing its rotation. 
// **************************************************************************

public class FixRotation : MonoBehaviour
{

    Quaternion rotation;

    void Start()
    {

        // Grabs the object's current rotation
        rotation = this.transform.rotation; 

    }

    void LateUpdate()
    {

        // Reverts it to its initial rotation
        this.transform.rotation = rotation; 

    }

}
