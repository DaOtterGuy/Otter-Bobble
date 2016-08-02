using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// **************************************************************************
// Creator: Ryan Gainford
// Last Updated: June 21, 2016
//
// The Player controller handles everything to do with the player including 
// movement, jumping, and bubble shooting. 
// **************************************************************************

public class PlayerController : MonoBehaviour 
{

    // Constants
    //
    private const int NO_COLLIDE_PLATFORM_LAYER = 12;
    private const int PLAYER_DEFAULT_LAYER = 9;
    private const int NO_COLLIDE_ENEMY_LAYER = 13; 

    [Header("Movement")]
    public float m_PlayerMoveSpeed;
    public bool m_IsRight;


    [Header("Jumping")]
    public float m_JumpTime; 
    [SerializeField] private float m_JumpSpd;
    [SerializeField] private float m_JumpTmr;
    [SerializeField] private float m_CheckForGroundTimer; 

    [SerializeField] private Transform m_GroundCheck;
    [SerializeField] private bool m_IsGrounded;
    [SerializeField] private bool m_IsInAir; 
    private int m_GroundCheckMask;

    private Rigidbody2D theRigidBody;
    [SerializeField] private float m_GravityScale;


    [Header("Bubble Projectile")]
    [HideInInspector] public List<GameObject> ListOfBubbles;
    public GameObject pref_BubbleProjectile;
    public Vector3 m_BubbleOffset; 


    [Header("Death")]
    public Transform m_StartPoint; 


    [Header("Animation")]
    [SerializeField] private Animator thePlayerAnimator;


    [Header("Managers")] 
    [SerializeField] private GameManager theGameManager; 
    [SerializeField] private UIManager theUIManager; 
    [SerializeField] public SoundManager theSoundManager;
    [SerializeField] private PlayerManager thePlayerManager; 


    void Start()
    {

        theRigidBody = this.GetComponent<Rigidbody2D>(); 
        thePlayerAnimator = this.GetComponent<Animator>();

        // Initializes gravity scale
        theRigidBody.gravityScale = m_GravityScale; 

        // Sets up collision checking
        m_GroundCheckMask = LayerMask.GetMask("Wall", "Platform");

    }

    void Update()
    {

        // Pause Menu
        {

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                theGameManager.Pause();
                theUIManager.ChangeMenu(enChangeMenu.Pause, true);
            }

        }

    }

    public void UpdatePlayer()
    {

        // Player Movement
        {

            // Change to Idle
            if (Input.GetAxisRaw("Horizontal") == 0)
            {
                thePlayerAnimator.SetBool("IsWalking", false);
            }

            // Move Right
            if (Input.GetAxisRaw("Horizontal") > 0)
            {

                // For flipping the image later
                if (!m_IsRight)
                    m_IsRight = !m_IsRight;

                PlayerMove(m_PlayerMoveSpeed);

                thePlayerAnimator.SetBool("IsWalking", true);

            }

            // Move Left
            if (Input.GetAxisRaw("Horizontal") < 0)
            {

                // For flipping the image later
                if (m_IsRight)
                    m_IsRight = !m_IsRight;

                PlayerMove(-m_PlayerMoveSpeed);

                thePlayerAnimator.SetBool("IsWalking", true);

            }

        }


        // Jumping
        {

            // Resets gravity scale
            theRigidBody.gravityScale = m_GravityScale;

            // Player can jump again
            if (m_IsGrounded)
                m_IsInAir = false; 

            // Takes in jump input
            if (Input.GetKey(KeyCode.UpArrow) && !m_IsInAir)
                PlayerJump();

            // Disables jumping again when the up key is released
            if (Input.GetKeyUp(KeyCode.UpArrow) && !m_IsGrounded)
                m_IsInAir = true;

            // Decreases jump time
            if (m_JumpTmr > 0)
                m_JumpTmr -= Time.deltaTime;

            // Safety check to make sure that the player can jump again
            if (m_CheckForGroundTimer > 0 && m_IsInAir)
                m_CheckForGroundTimer -= Time.deltaTime;

            else if (m_CheckForGroundTimer <= 0.0f && m_IsInAir)
                m_IsGrounded = true; 

        }


        // Go Down Through Platforms 
        {

            // Changes the current layer to one in which the player can go through platforms
            if (Input.GetKey(KeyCode.DownArrow) && m_IsGrounded)
                this.gameObject.layer = NO_COLLIDE_PLATFORM_LAYER;

            else
            {

                if (thePlayerManager.m_IsInvincibilityOn)
                    this.gameObject.layer = NO_COLLIDE_ENEMY_LAYER; 

                else
                    this.gameObject.layer = PLAYER_DEFAULT_LAYER;

            }

        }


        // Bubbles
        {

            // Creates new bubble
            if (Input.GetKeyDown(KeyCode.Space))
            {
                theSoundManager.PlaySFX(enSFX.Shoot); 
                ShootBubble();
            }

            // Handles updating bubbles
            if (ListOfBubbles.Count != 0)
                HandleBubbles();

        }

    }


    // Player Movement Support Functions
    //
    void PlayerMove(float aMoveSpeed)
    {

        // Make the player faces the right direction
        thePlayerManager.Flip(m_IsRight, m_BubbleOffset);

        // Moves the player based on a speed
        Vector2 newPos = transform.position;
        newPos.x += aMoveSpeed * Time.deltaTime;
        transform.position = newPos;   

    }


    // Player Jump Support Functions
    //
    void PlayerJump()
    {

        // Makes sure player is on ground
        if(m_IsGrounded)
        {
            
            m_JumpTmr = m_JumpTime;
            m_IsGrounded = false;

            // Safety check to make sure the player can jump again
            m_CheckForGroundTimer = m_JumpTime * 3; 

        }

        // Disables gravity and applies upwards motion
        if (m_JumpTmr > 0.0f)
        {
            
            theRigidBody.gravityScale = 0;
            transform.position += Vector3.up * m_JumpSpd * Time.deltaTime;

        } 

    }

    void OnCollisionEnter2D(Collision2D col)
    {

        // Checks that the player is on the ground
        m_IsGrounded = Physics2D.Linecast(this.transform.position,
                                            m_GroundCheck.transform.position,
                                            m_GroundCheckMask);

    }


    // Bubble Support Functions
    //
    void ShootBubble()
    {

        // Adds a new bubble to the list
        ListOfBubbles.Add( Instantiate(pref_BubbleProjectile, transform.position + m_BubbleOffset, Quaternion.identity) as GameObject);

    }

    void HandleBubbles()
    {

        // Interates in the list backwards to forgo errors with removing projectiles
        // Updates all active bubbles
        for (int i = ListOfBubbles.Count - 1; i >= 0; i--)
        {
            ListOfBubbles[i].GetComponent<Bubble>().UpdateBubble(); 
        }

    }


    // Invincibility Power Up Functions
    //
    public void ApplyInvinicibility()
    {

        // Disables player colliding with enemy
        this.gameObject.layer = NO_COLLIDE_ENEMY_LAYER; 

    }

    public void DisableInvinicibility()
    {

        // enables player to colllide with enemy
        this.gameObject.layer = PLAYER_DEFAULT_LAYER; 

    }

}
