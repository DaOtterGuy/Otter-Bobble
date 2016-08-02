using UnityEngine;
using System.Collections;

// **************************************************************************
// Creator: Ryan Gainford
// Last Updated: June 14, 2016
//
// A class for the  player projectiles. Handles everything regarding the 
// bubble including movement and destruction. 
// **************************************************************************

public class Bubble : MonoBehaviour 
{

    // Constants
    //
    private const float INITIAL_START_TIME = 0.55f;
    private const float INITIAL_CAPTURE_TIME = 3.0f;
    private const float HORIZONTAL_BUBBLE_SPEED = 10.0f;
    private const float VERTICAL_BUBBLE_SPEED = 1.0f;
    private const float SIN_WAVE_FREQUENCY = 5.0f;
    private const float LINE_CHECK_DIST = 0.4F;

    private float tmr_Exists;
    private float m_BubbleSpeed; 

    private bool m_HitSomething;
    private int m_CollisionCheckMask;

    public bool m_CaughtEnemy; 

    private Rigidbody2D theRigidBody; 
    private PlayerController thePlayer;
    private ParticleSystemManager theParticleSystemManager; 

    void Awake()
    {

        // Sets the timer for how long the bubble exists in-game
        tmr_Exists = INITIAL_START_TIME;

        m_CaughtEnemy = false; 

        thePlayer = GameObject.Find("Player").GetComponent<PlayerController>();
        theRigidBody = GetComponent<Rigidbody2D>();
        theParticleSystemManager = GameObject.Find("ParticleSystemManager").GetComponent<ParticleSystemManager>();

        // Applies force depending on which direction the player is facing
        if (thePlayer.m_IsRight)
            m_BubbleSpeed = HORIZONTAL_BUBBLE_SPEED;

        else
            m_BubbleSpeed = -HORIZONTAL_BUBBLE_SPEED; 

        // Sets up the layer mask to detect walls
        m_CollisionCheckMask = LayerMask.GetMask("Wall");

    }

    public void UpdateBubble()
    {

        // Obstacle Checking
        {

            if (!m_CaughtEnemy)
            {

                // First it checks the facing direction of the enemy then throws a linecast
                // in front of the enemy
                if (m_BubbleSpeed < 0)
                    m_HitSomething = Physics2D.Linecast(transform.position,
                                                         new Vector2(this.transform.position.x - LINE_CHECK_DIST, transform.position.y),
                                                         m_CollisionCheckMask);

                else
                    m_HitSomething = Physics2D.Linecast(transform.position,
                                                         new Vector2(this.transform.position.x + LINE_CHECK_DIST, transform.position.y),
                                                         m_CollisionCheckMask);

                // If the enemy detects something it will move in the opposite direction
                if (m_HitSomething)
                    DestroyBubble();

            }

        }

        // Bubble exists
        {

            //  Once the timer runs out the bubble will be destroyed
            tmr_Exists -= Time.deltaTime;

            if (tmr_Exists <= 0.0f)
                DestroyBubble(); 

        }

        // Moves the Bubble
        {

            if (!m_CaughtEnemy)
            {

                Vector3 newPos = this.transform.position;
                newPos.x += m_BubbleSpeed * Time.deltaTime;
                this.transform.position = newPos;

            }

            else
            {

                Vector3 newPos = this.transform.position;
                newPos.y += m_BubbleSpeed * Time.deltaTime;
                this.transform.position = newPos;

            }

        }


        // Captured an enemy
        {

            if (m_CaughtEnemy)
            {

                // Applies a sin wave to the bubbled enemies movement
                Vector3 newPos = this.transform.position;

                newPos.x += Mathf.Sin(Time.time * SIN_WAVE_FREQUENCY) * Time.deltaTime;

                this.transform.position = newPos;

            }

        }

    }


    // Bubble Destruction Support Functions
    //
    public void DestroyBubble()
    {

        // Plays the bubble popping particles
        theParticleSystemManager.PlayParticles(enParticles.BubblePop, this.transform.position);
        thePlayer.GetComponent<PlayerController>().theSoundManager.PlaySFX(enSFX.Pop);

        // Removes the bubble from the list of bubbles in-game
        thePlayer.ListOfBubbles.Remove(this.gameObject);

        // Deactivate any enemies who may be inside the bubble
        if (this.transform.childCount != 0)
        {

            // Removes the captured enemy from the bubble parent, removes it rom
            // the list of active enemies and deactivates it
            for (int i = 0; i < this.transform.childCount; i++)
            {

                GameObject oldParent = GameObject.FindGameObjectWithTag("EnemyList");

                GameObject child = this.transform.GetChild(i).gameObject;
                child.transform.parent = oldParent.transform;

                child.GetComponent<EnemyController>().RemoveFromList();
                child.SetActive(false); 
                
            }

        }

        Destroy(this.gameObject);

    }


    // Enemy Capture Support Functions
    //
    public void CaptureEnemy()
    {

        m_CaughtEnemy = true;

        // Reset timer
        tmr_Exists = INITIAL_CAPTURE_TIME;

        // Make the bubble move upwards now
        m_BubbleSpeed = VERTICAL_BUBBLE_SPEED; 

    }

}
