using UnityEngine;
using System.Collections;

// **************************************************************************
// Creator: Ryan Gainford
// Last Updated: June 8, 2016
//
// A class for handling any collisions with the enemy. 
// **************************************************************************

public class EnemyCollide : MonoBehaviour 
{

    [SerializeField] private float m_ShrinkScale;

    void OnTriggerEnter2D(Collider2D other)
    {

        string tag = other.tag; 

        // Player Projectile Collision
        //
        if (tag == "P_Projectile" )
        {

            // Exits the function if the projectile has already captured an enemy
            if (other.GetComponent<Bubble>().m_CaughtEnemy)
                return;

            other.transform.position = transform.position; 

            // Makes the enemy smaller to fit in the bubble
            transform.localScale /= m_ShrinkScale;

            // Parents the object to move it with the bubble
            transform.parent = other.gameObject.transform;

            // Disable the collider and rigid body so the enemy inside the bubble doesnt collide with anything
            this.GetComponent<Collider2D>().enabled = false;
            this.GetComponent<Rigidbody2D>().isKinematic = true; 

            other.GetComponent<Bubble>().CaptureEnemy();

            // Stop any behaviours the enemy is in the middle of doing
            this.GetComponent<EnemyController>().DeactivateBehaviour(); 

        }

    }

}
