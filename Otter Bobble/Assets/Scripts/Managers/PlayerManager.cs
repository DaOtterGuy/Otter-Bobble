using UnityEngine;
using System.Collections;

// **************************************************************************
// Creator: Ryan Gainford
// Last Updated: June 19, 2016
//
// Handles the player, specifically the player controller. Also flips the 
// player sprite. 
// **************************************************************************

public class PlayerManager : MonoBehaviour, IManager
{

    // Constants
    //
    private const float SUPER_SPEED_FACTOR = 2.0F;
    private const float SUPER_SPEED_TIME = 15.0f;
    private const float INVINICIBILITY_TIME = 5.0F;

    [Header("The Player")]
    public GameObject thePlayer;
    private PlayerController thePlayerController;

    [Header("Super Speed Variables")]
    [SerializeField] private bool m_IsSuperSpeedOn; 
    [SerializeField] private float tmr_SuperSpd; 

    private float m_PrevSpd;

    [Header("Invincibility Variables")]
    [SerializeField] public bool m_IsInvincibilityOn;
    [SerializeField] private float tmr_Invinicibility;


    [HideInInspector] public bool m_IsRight;
    [HideInInspector] public bool m_IsPlayerMoving; 

    void Start()
    {

        thePlayerController = thePlayer.GetComponent<PlayerController>();

        m_IsSuperSpeedOn = false;
        m_IsInvincibilityOn = false; 
        m_IsPlayerMoving = true; 

    }

    void Update()
    {

        // Updates the Player
        if(m_IsPlayerMoving)
            thePlayerController.UpdatePlayer();


        // Super Speed Power Up
        if(m_IsPlayerMoving)
        {

            if (tmr_SuperSpd > 0.0f && m_IsSuperSpeedOn)
                tmr_SuperSpd -= Time.deltaTime; 

            // Turns off super speed and resets the player move speed
            else if (tmr_SuperSpd <= 0.0f && m_IsSuperSpeedOn)
            {

                m_IsSuperSpeedOn = false;
                thePlayerController.m_PlayerMoveSpeed = m_PrevSpd; 

            }

        }


        // Invincibility Power Up
        if (m_IsPlayerMoving)
        {

            if (tmr_Invinicibility > 0.0f && m_IsInvincibilityOn)
                tmr_Invinicibility -= Time.deltaTime;

            // Turns off invincibility
            else if (tmr_Invinicibility <= 0.0f && m_IsInvincibilityOn)
            {

                m_IsInvincibilityOn = false;
                thePlayerController.DisableInvinicibility();

            }

        }

    }


    // Sprite Direction Support Functions
    //
    public void Flip( bool IsRight, Vector3 aBubbleOffset )
    {
        
        // Makes sure the player isnt already going in the sent in dirtection
        if (m_IsRight != IsRight)
        {

            m_IsRight = IsRight;

            // Flips the player sprite
            Vector2 flip = thePlayer.transform.localScale;
            flip.x *= -1.0f;
            thePlayer.transform.localScale = flip;

            // Makes sure the bubble shoots in the right direction
            aBubbleOffset.x *= -1.0f; 
            thePlayerController.m_BubbleOffset = aBubbleOffset; 

        }

    }


    // Power Up Support Functions
    // 
    public void ApplySuperSpeed()
    {

        // Grabs the player move speed
        float newSpeed = thePlayerController.m_PlayerMoveSpeed;
        m_PrevSpd = newSpeed; 

        // Applies the super speed factor
        newSpeed *= SUPER_SPEED_FACTOR;

        // Applies the new speed
        thePlayerController.m_PlayerMoveSpeed = newSpeed;

        // Sets the timer
        tmr_SuperSpd = SUPER_SPEED_TIME;
        m_IsSuperSpeedOn = true; 

    }


    public void ApplyInvincibilityToPlayer()
    {

        m_IsInvincibilityOn = true;
        tmr_Invinicibility = INVINICIBILITY_TIME;  

        thePlayerController.ApplyInvinicibility(); 

    }


    // IManager Inheritence
    //
    public void Resume()
    {
        m_IsPlayerMoving = true; 
    }

    public void Pause()
    {
        m_IsPlayerMoving = false; 
    }

    public void Reset()
    {

        // Places the player back at start position
        thePlayer.transform.position = thePlayerController.m_StartPoint.position;

        if (thePlayerController.ListOfBubbles.Count != 0)
        {

            // Deletes all bubble currently existing
            for (int i = thePlayerController.ListOfBubbles.Count - 1; i >= 0; i--)
            {
                thePlayerController.ListOfBubbles[i].GetComponent<Bubble>().DestroyBubble();
            }

        }

        // Turn off super speed if its on
        if(m_IsSuperSpeedOn)
        {

            tmr_SuperSpd = 0.0f;
            m_IsSuperSpeedOn = false; 
            thePlayerController.m_PlayerMoveSpeed = m_PrevSpd; 

        }

        // Turn off Invincibility
        if(m_IsInvincibilityOn)
        {

            tmr_Invinicibility = 0.0f;
            m_IsInvincibilityOn = false;
            thePlayerController.DisableInvinicibility(); 

        }

    }
    
}
