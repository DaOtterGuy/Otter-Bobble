using UnityEngine;
using System.Collections;

// **************************************************************************
// Creator: Ryan Gainford
// Last Updated: June 19, 2016
//
// A class for handling any collisions with the player. 
// **************************************************************************

public class PlayerCollide : MonoBehaviour 
{
    
    [SerializeField] private PlayerController thePlayer;
    [SerializeField] private HUD theHUD; 

    void Start()
    {
        thePlayer = GetComponent<PlayerController>(); 
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        // Player Projectile Collision
        //
        if( other.CompareTag("P_Projectile") )
        {

            // Ignore bubble if it hasn't caught an enemy
            if (!other.GetComponent<Bubble>().m_CaughtEnemy)
                return;

            // Possibly drops an item
            other.transform.GetChild(0).GetComponent<EnemyDrops>().DropItem(); 

            // Destroy the bubble
            other.GetComponent<Bubble>().DestroyBubble();

            // Score Points
            theHUD.ScorePoints(500); 

        }

        // Enemy Projectile Collision
        //
        if ( other.CompareTag("E_Projectile") )
        {

            theHUD.LoseLife();

            other.gameObject.GetComponent<EnemyProjectile>().DestroyProjectile();

            thePlayer.transform.position = thePlayer.m_StartPoint.position;

        }

        // Death Pit Collision
        if ( other.CompareTag("Death_Pit") )
        {

            // Takes away a life
            theHUD.LoseLife();

            // Resets the player's position
            thePlayer.transform.position = thePlayer.m_StartPoint.position;

        }

    }

    void OnCollisionEnter2D( Collision2D other )
    {

        // Enemy Collision
        //
        if ( other.collider.CompareTag("Enemy") )
        {

            // Takes away a life
            theHUD.LoseLife();
            
            // Resets the player's position
            thePlayer.transform.position = thePlayer.m_StartPoint.position;

        }

    }

}
