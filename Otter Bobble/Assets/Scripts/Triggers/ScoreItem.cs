using UnityEngine;
using System.Collections;

// **************************************************************************
// Creator: Ryan Gainford
// Last Updated: June 19, 2016
//
// USed on items, that when grabbed will score points.
// **************************************************************************

public class ScoreItem : MonoBehaviour
{

    // Constants
    //
    private const float FALL_SPD = 2.0f; 
    private const float BOB_HEIGHT = 0.25F;

    [SerializeField]private Vector2 m_TopBob;
    [SerializeField]private Vector2 m_BottomBob;

    [SerializeField]private bool m_IsGoingUp;
    private bool m_IsDropItem; 

    private float m_BobSpd; 

    [Header("Score Amount")]
    public int m_PointWorth;

    [Header("Managers")]
    [SerializeField] private HUD theHUD;
    [SerializeField] private SoundManager theSoundManager;
    [SerializeField] private ItemManager theItemManager;   


    void Start()
    {

        theHUD = GameObject.Find("HUD").GetComponent<HUD>();
        theSoundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        theItemManager = GameObject.Find("ItemManager").GetComponent<ItemManager>();

        // Grabs the top and bottom bounds of the bouncing fruit
        m_TopBob = new Vector2(this.transform.position.x, this.transform.position.y + BOB_HEIGHT);
        m_BottomBob = new Vector2 (this.transform.position.x, this.transform.position.y - BOB_HEIGHT);

        m_BobSpd = 0.5f;

        m_IsGoingUp = false;

        m_IsDropItem = false;  

    }

    public void UpdateItem( bool IsDrop )
    {

        // For items that are not dropped by enemies
        if (!IsDrop)
        {

            // Movement
            {

                // Moves the item in its current direction
                if (m_IsGoingUp)
                    Bob(this.transform.position, m_TopBob);
                else
                    Bob(this.transform.position, m_BottomBob);

            }


            // Direction
            {

                // Switches movement direction whent he item has reached its destination
                if (this.transform.position.y == m_TopBob.y)
                    m_IsGoingUp = false;

                else if (this.transform.position.y == m_BottomBob.y)
                    m_IsGoingUp = true;

            }

        }

        else
        {

            if (m_IsDropItem == false)
                m_IsDropItem = true; 

            // Moves the item downwards
            Vector3 newPos = this.transform.position;
            newPos.y -= FALL_SPD * Time.deltaTime;
            this.transform.position = newPos;

        }

    }


    void Bob(Vector2 aStartPos, Vector2 aDestination)
    {

        // Bobs the item up or down by a fraction of its distance
        float moveValue = m_BobSpd * Time.deltaTime;
        this.transform.position = Vector3.MoveTowards(aStartPos, aDestination, moveValue);

    }


    void OnTriggerEnter2D( Collider2D other )
    {

        if( other.CompareTag("Player") )
        {

            theSoundManager.PlaySFX(enSFX.Pickup);

            // Scores points and updates the HUD accordingly
            theHUD.ScorePoints(m_PointWorth);

            // Sets the object to inactive, destroying will happen in the reset
            this.gameObject.SetActive(false); 

        }

    }

}
