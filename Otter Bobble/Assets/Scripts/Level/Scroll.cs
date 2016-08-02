using UnityEngine;
using System.Collections;

// **************************************************************************
// Creator: Ryan Gainford
// Last Updated: June 5, 2016
//
// Scrolls an object continuously at a set speed.
// **************************************************************************

public class Scroll : MonoBehaviour
{

    public float m_ScrollSpd;

    public void MoveObject( Vector3 aStartPos, Vector3 aEndPos )
    {
        
        // Moves the object a fraction of the distance towards its destination
        float moveValue = m_ScrollSpd * Time.deltaTime;
        this.transform.position = Vector3.MoveTowards( aStartPos, aEndPos, moveValue );

    }

}
