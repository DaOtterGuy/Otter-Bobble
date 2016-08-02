using UnityEngine;
using System.Collections;

// **************************************************************************
// Creator: Ryan Gainford
// Last Updated: June 14, 2016
//
// Will run around the level continuously. 
// **************************************************************************

public class WanderType : MonoBehaviour, IEnemyType
{

    // Constants
    // 
    private const float LINE_CHECK_DIST = 0.6F; 

    private GameObject theEnemy;

    private float m_EnemySpd;

    private bool m_IsCaptured; 

    private bool m_HitSomething;
    private int m_CollisionCheckMask;


    // Activation and Deactivation
    //
    public void ActivateEnemy(GameObject aEnemy, float aSpd)
    {

        theEnemy = aEnemy;

        // Gets a random number to detrmine which way the enemy will move first
        float randNum = Random.Range(-5, 5);

        if (randNum >= 0)
            m_EnemySpd = aSpd;

        else
        {
            FlipSprite(); 
            m_EnemySpd = -aSpd;
        }

        // Sets up the layer mask to check for walls, platforms, enemies and the player
        m_CollisionCheckMask = LayerMask.GetMask("Wall", "Platform","Enemy");

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

        // Obstacle Checking
        { 

            // First it checks the facing direction of the enemy then throws a linecast
            // in front of the enemy
            if (m_EnemySpd < 0)
                m_HitSomething = Physics2D.Linecast( transform.position, 
                                                     new Vector2(this.transform.position.x - LINE_CHECK_DIST, transform.position.y), 
                                                     m_CollisionCheckMask );

            else
                m_HitSomething = Physics2D.Linecast( transform.position, 
                                                     new Vector2(this.transform.position.x + LINE_CHECK_DIST, transform.position.y), 
                                                     m_CollisionCheckMask );

            // If the enemy detects something it will move in the opposite direction
            if (m_HitSomething)
            {
                m_EnemySpd *= -1.0f;
                FlipSprite();
            }

        }

        // Movement
        {

            // Moves the enemy
            Vector3 newPos = theEnemy.transform.position;
            newPos.x += m_EnemySpd * Time.deltaTime;
            theEnemy.transform.position = newPos;

        }

    }

    public void FlipSprite()
    {

        // Flips the sprite in the x axis
        Vector2 flipScale = theEnemy.transform.localScale;
        flipScale.x *= -1.0f;
        theEnemy.transform.localScale = flipScale;

    }

    public void Reset(Vector3 aScale, Vector3 aPos)
    {

        // Resets all initial values
        theEnemy.transform.localScale = aScale;
        theEnemy.transform.position = aPos;

        theEnemy.GetComponent<Rigidbody2D>().isKinematic = false;
        theEnemy.GetComponent<BoxCollider2D>().enabled = true; 

    }
}
