using UnityEngine;
using System.Collections;

// **************************************************************************
// Creator: Ryan Gainford
// Last Updated: June 8, 2016
//
// The enemy will patrol an area until it detects the player. It will then
// give chase until it either catches the player or loses the player. If the
// enemy loses the player it will return to its initial position and start
// patrolling again. 
// **************************************************************************

public class HomingType : MonoBehaviour, IEnemyType
{

    // Constants
    //
    private const float PATROL_DIST = 1.0F;
    private const float CHECK_RADIUS = 8.0f; 

    private GameObject theEnemy;
    private GameObject thePlayer; 

    private Vector2 m_LeftBounds;
    private Vector2 m_RightBounds;

    private float m_ReturnTimer; 

    private float m_EnemySpd;

    private bool m_IsMovingLeft; 
    private bool m_IsCaptured;

    private bool m_ChasingPlayer;


    // Activation and Deactivation
    //
    public void ActivateEnemy(GameObject aEnemy, float aSpd)
    {

        theEnemy = aEnemy;
        thePlayer = GameObject.FindGameObjectWithTag("Player");

        // Sets the patrol bounds by the position of the enemy and its patrol distance
        m_LeftBounds = new Vector2(theEnemy.transform.position.x - PATROL_DIST, theEnemy.transform.position.y);
        m_RightBounds = new Vector2(theEnemy.transform.position.x + PATROL_DIST, theEnemy.transform.position.y);

        m_EnemySpd = aSpd;

        m_ReturnTimer = 0.0f; 

        m_ChasingPlayer = false;
        m_IsMovingLeft = true; 
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
            if (m_IsMovingLeft && !m_ChasingPlayer && m_ReturnTimer <= 0.0f)
                Patrol(transform.position, m_LeftBounds);

            else if (!m_IsMovingLeft && !m_ChasingPlayer)
                Patrol(transform.position, m_RightBounds);

        }

        // Checking Destination Reached
        {

            // Switches the direction the enemy is heading to if the enemy has reached their 
            // destination
            if (transform.position.x == m_LeftBounds.x)
                m_IsMovingLeft = false;

            if (transform.position.x == m_RightBounds.x)
                m_IsMovingLeft = true;

        }

        // Player Detection
        {

            // Checks for the player
            Collider2D[] hitInfo = Physics2D.OverlapCircleAll(transform.position, CHECK_RADIUS);

            if (hitInfo != null && !m_ChasingPlayer)
            {

                // Checks if the enemy has detected the player
                foreach (Collider2D col in hitInfo)
                {

                    if (col.tag == "Player")
                    {
                        // Stops patrolling and readies to chase the player
                        m_ChasingPlayer = true;
                    }

                }

            }

        }

        // Stop Chasing Player
        {

            // If the distance between the enemy and the player is greater then its radius, 
            // the enemy will stop chasing the player. 
            if (Vector3.Distance(theEnemy.transform.position, thePlayer.transform.position) >= CHECK_RADIUS)
            { 
                m_ChasingPlayer = false;
            }

        }

        // Enemy Chasing
        {

            if (m_ChasingPlayer)
            {

                // The enmy looks at the player and corrects its rotation
                theEnemy.transform.LookAt(thePlayer.transform.position);
                theEnemy.transform.Rotate(new Vector3(0, -90, 0), Space.Self);

                // It then moves towards the player 
                theEnemy.transform.Translate(new Vector3(m_EnemySpd * Time.deltaTime, 0, 0));

            }

        }

        // Reduce Timer
        {

            if (m_ReturnTimer > 0.0f)
            {
                m_ReturnTimer -= Time.deltaTime;
                Patrol(this.transform.position, theEnemy.GetComponent<EnemyController>().m_StartPos);
            }

        }

    }

    void Patrol(Vector2 aStartPos, Vector2 aDestination)
    {

        float moveValue = m_EnemySpd * Time.deltaTime;
        this.transform.position = Vector3.MoveTowards(aStartPos, aDestination, moveValue);

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
