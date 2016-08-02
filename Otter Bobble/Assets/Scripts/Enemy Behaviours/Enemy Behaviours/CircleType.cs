using UnityEngine;
using System.Collections;

// **************************************************************************
// Creator: Ryan Gainford
// Last Updated: June 8, 2016
//
// Enemy will move in a circle continuously. 
// **************************************************************************

public class CircleType : MonoBehaviour, IEnemyType
{

    // Constants
    //
    private const float RADIUS = 2.0F;

    private GameObject theEnemy;

    private float m_CenterX;
    private float m_CenterY;

    private float m_Timer; 

    private float m_EnemySpd;

    private bool m_IsCaptured;  


    // Activation and Deactivation
    //
    public void ActivateEnemy(GameObject aEnemy, float aSpd)
    {

        theEnemy = aEnemy;

        // Set the center to above the enemy by the radius of the path
        m_CenterX = aEnemy.transform.position.x;
        m_CenterY = aEnemy.transform.position.y + RADIUS;

        // Adds variation to patrol type enemy speed
        float randNum = Random.Range(0.0f, 1.0f);
        m_EnemySpd = aSpd + randNum;

        m_IsCaptured = false; 

    }

    public void DeactivateEnemy()
    {

        // Stops any enemy behaviour
        m_IsCaptured = true;

    }


    // Behaviour Functions
    //
    public void UpdateBehaviour()
    {

        // Check If Captured
        {

            if (m_IsCaptured)
                return;

        }

        // Movement
        {

            // Determines the motion of the circular path using sin and cos
            m_Timer += Time.deltaTime;
            float angle = m_Timer * m_EnemySpd;

            theEnemy.transform.position = new Vector2(m_CenterX + Mathf.Sin(angle) * RADIUS,
                                                       m_CenterY + Mathf.Cos(angle) * RADIUS);

        }

    }

    public void FlipSprite() { }

    public void Reset(Vector3 aScale, Vector3 aPos)
    {

        // Resets all initial values
        theEnemy.transform.localScale = aScale;
        theEnemy.transform.position = aPos;

        theEnemy.GetComponent<BoxCollider2D>().enabled = true;

    }

}
