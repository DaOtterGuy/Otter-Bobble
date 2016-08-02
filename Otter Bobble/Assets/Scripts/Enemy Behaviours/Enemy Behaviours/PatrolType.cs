using UnityEngine;
using System.Collections;

// **************************************************************************
// Creator: Ryan Gainford
// Last Updated: June 8, 2016
//
// The enemy will patrol an area continuously.  
// **************************************************************************

public class PatrolType : MonoBehaviour, IEnemyType
{

    // Constants
    //
    private const float PATROL_DIST = 1.0f; 

    private GameObject theEnemy; 

    private Vector2 m_LeftBounds;
    private Vector2 m_RightBounds;

    private bool m_IsMovingLeft;
    private bool m_IsCaptured; 

    private float m_EnemySpd;


    // Activation and Deactivation
    //
    public void ActivateEnemy( GameObject aEnemy, float aSpd )
    {

        theEnemy = aEnemy;

        // Sets the bounds of the patrolling enemy by the initial position
        m_LeftBounds = new Vector2(theEnemy.transform.position.x - PATROL_DIST, theEnemy.transform.position.y);
        m_RightBounds = new Vector2(theEnemy.transform.position.x + PATROL_DIST, theEnemy.transform.position.y);

        // Adds variation to patrol type enemy speed
        float randNum = Random.Range(0.0f, 1.5f);
        m_EnemySpd = aSpd + randNum;

        // Randomize starting direction of enemy
        randNum = Random.Range(-5, 5);

        if (randNum >= 0)
            m_IsMovingLeft = true;

        else
        {
            FlipSprite();
            m_IsMovingLeft = false; 
        }

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

        // Patrolling
        {

            // Passes in the enemy's position and the current bounds the enemy is heading to
            if (m_IsMovingLeft)
                Patrol(transform.position, m_LeftBounds);

            else
                Patrol(transform.position, m_RightBounds);

        }

        // Checking Destination Reached
        {

            // Switches the direction the enemy is heading to if the enemy has reached their 
            // destination
            if ( transform.position.x == m_LeftBounds.x )
            {
                m_IsMovingLeft = false;
                FlipSprite(); 
            }

            if( transform.position.x == m_RightBounds.x )
            {
                m_IsMovingLeft = true;
                FlipSprite(); 
            }

        }

    }

    void Patrol( Vector2 aStartPos, Vector2 aDestination)
    {
        
        float moveValue = m_EnemySpd * Time.deltaTime;
        this.transform.position = Vector3.MoveTowards(aStartPos, aDestination, moveValue);

    }

    public void FlipSprite()
    {

        // Flips the sprite in the x axis
        Vector2 flipScale = theEnemy.transform.localScale;
        flipScale.x *= -1.0f;
        theEnemy.transform.localScale = flipScale;

    }

    public void Reset( Vector3 aScale, Vector3 aPos )
    {

        // Resets all intial values
        theEnemy.transform.position = aPos;
        theEnemy.transform.localScale = aScale;

        theEnemy.GetComponent<Rigidbody2D>().isKinematic = false;
        theEnemy.GetComponent<BoxCollider2D>().enabled = true; 

    }
}
