using UnityEngine;
using System.Collections;

// **************************************************************************
// Creator: Ryan Gainford
// Last Updated: June 8, 2016
//
// Used solely to reactivate all of the enemies.
// **************************************************************************

public class ActivateChildren : MonoBehaviour
{

    public void ActivateAll()
    {

        // Goes through each child and activates them
        foreach (Transform child in this.transform)
        {
            child.gameObject.SetActive(true);
        }

    }

}
