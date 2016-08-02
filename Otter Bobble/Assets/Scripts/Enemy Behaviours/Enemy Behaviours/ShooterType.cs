using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// **************************************************************************
// Creator: Ryan Gainford
// Last Updated: June 17, 2016
//
// Will run around the level continuously. Enemy will shoot projectiles
// periodically. 
// **************************************************************************

public class ShooterType : MonoBehaviour, IEnemyType
{

    // Constants
    //
    private const float LINE_CHECK_DIST = 0.4F;
    private const float TIME_UNTIL_NEXT_BULLET = 1.5f;

    private GameObject theEnemy;

    private float m_EnemySpd;

    private bool m_HitSomething;
    private int m_CollisionCheckMask;

    public List<GameObject> ListOfProjectiles;
    public GameObject pref_Projectile;
    public Vector3 m_ProjectileOffset; 

    private float tmr_BulletTimer;

    private bool m_IsCaptured;


    // Activation and Deactivation
    //
    public void ActivateEnemy(GameObject aEnemy, float aSpd)
    {

        theEnemy = aEnemy;

        // Gets a random number to detrmine which way the enemy will move first
        // Also sets which direction the bullet will shoot
        float randNum = UnityEngine.Random.Range(-5, 5);

        m_ProjectileOffset = new Vector3(0.75f, 0.1f, 0.0f);

        if (randNum >= 0)
            m_EnemySpd = aSpd;

        else
        {
            m_EnemySpd = -aSpd;
            FlipSprite(); 
        }

        // Sets up the layer mask to check for walls, platforms, enemies and the player
        m_CollisionCheckMask = LayerMask.GetMask("Wall", "Platform", "Enemy");

        // Grabs the enemy projectile prefab from the enemy controller
        pref_Projectile = theEnemy.GetComponent<EnemyController>().pref_EnemyProjectile;

        // Starts timer until the first bullet is fired
        tmr_BulletTimer = TIME_UNTIL_NEXT_BULLET;

        // Initializes list of projectiles
        ListOfProjectiles = new List<GameObject>();

        m_IsCaptured = false; 

    }

    public void DeactivateEnemy()
    {

        // Stops any enemy behaviour
        m_IsCaptured = true;

        for (int i = ListOfProjectiles.Count - 1; i >= 0 ; i--)
        {
            ListOfProjectiles[i].GetComponent<EnemyProjectile>().DestroyProjectile(); 
        }

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
                m_HitSomething = Physics2D.Linecast(transform.position,
                                                     new Vector2(this.transform.position.x - LINE_CHECK_DIST, transform.position.y),
                                                     m_CollisionCheckMask);

            else
                m_HitSomething = Physics2D.Linecast(transform.position,
                                                     new Vector2(this.transform.position.x + LINE_CHECK_DIST, transform.position.y),
                                                     m_CollisionCheckMask);

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


        // Projectiles
        {

            // If the timer runs out then shoot a projectile
            if (tmr_BulletTimer <= 0.0f)
                ShootProjectile();

            else
                tmr_BulletTimer -= Time.deltaTime;

            // If there are projectiles then update them
            if (ListOfProjectiles.Count != 0)
                HandleProjectiles();

        }

    }

    public void FlipSprite()
    {

        // Flips the sprite in the x axis
        Vector2 flipScale = theEnemy.transform.localScale;
        flipScale.x *= -1.0f;
        theEnemy.transform.localScale = flipScale;

        // Switches the facing direction of the bullet
        m_ProjectileOffset.x *= -1.0f;

    }


    // Projectile Support functions
    // 
    void ShootProjectile()
    {

        // Create a new projectile
        GameObject aProjectile = Instantiate(pref_Projectile, theEnemy.transform.position + m_ProjectileOffset, Quaternion.identity) as GameObject;

        // Sets the enemy that shot the projectile and its directional speed
        aProjectile.GetComponent<EnemyProjectile>().SetProjectileSpeed(m_EnemySpd);
        aProjectile.GetComponent<EnemyProjectile>().SetEnemy(this.gameObject);

        // Adds the projectile to the list of existing projectiles
        ListOfProjectiles.Add(aProjectile);

        // Reset the timer until next bullet is shot
        tmr_BulletTimer = TIME_UNTIL_NEXT_BULLET;

    }

    void HandleProjectiles()
    {

        // Interates in the list backwards to forgo errors with removing projectiles
        // Updates all active projectiles
        for (int i = ListOfProjectiles.Count - 1; i >= 0; i--)
        {
            ListOfProjectiles[i].GetComponent<EnemyProjectile>().UpdateProjectile();
        }

    }

    public void Reset(Vector3 aScale, Vector3 aPos)
    {

        // Resets all initial values
        theEnemy.transform.localScale = aScale;
        theEnemy.transform.position = aPos;

        theEnemy.GetComponent<Rigidbody2D>().isKinematic = false;
        theEnemy.GetComponent<BoxCollider2D>().enabled = true;

        // Removes any currently existing rojectiles
        for (int i = ListOfProjectiles.Count - 1; i >= 0; i--)
        {
            ListOfProjectiles[i].GetComponent<EnemyProjectile>().DestroyProjectile(); 
        }

    }
}
