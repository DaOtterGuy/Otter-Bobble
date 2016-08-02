using UnityEngine;
using System.Collections;

// **************************************************************************
// Creator: Ryan Gainford
// Last Updated: June 17, 2016
//
// Class used for Projectiles fired by the enemy. 
// **************************************************************************

public class EnemyProjectile : MonoBehaviour
{

    // Constants
    //
    private const float PROJECTILE_SPD_INCREASE = 2.0F;
    private const float CHECK_RADIUS = 0.5F; 

    private float m_ProjectileSpd;

    private bool m_HitSomething; 
    private int m_CollisionCheckMask; 

    private GameObject theEnemy;
    private IEnemyType theEnemyType; 

    void Awake()
    {

        m_ProjectileSpd = 0.0f;

        // Sets up the layer mask to detect walls
        m_CollisionCheckMask = LayerMask.GetMask("Wall");

    }

    public void UpdateProjectile()
    {

        // Collision Checking
        {

            // Does an overlapcircle to check if the projectile hashit a wall
            m_HitSomething = Physics2D.OverlapCircle(this.transform.position,
                                                        CHECK_RADIUS,
                                                        m_CollisionCheckMask);

            if (m_HitSomething)
                DestroyProjectile();

        }

        // Moves the bullet forward 
        Vector3 newPos = this.transform.position;
        newPos.x += m_ProjectileSpd * Time.deltaTime;
        this.transform.position = newPos;  

    }

    public void DestroyProjectile()
    {

        // Removes projectile from the enemies list so it doesnt update anymore
        theEnemy.GetComponent<EnemyController>().GetComponent<ShooterType>().ListOfProjectiles.Remove(this.gameObject);

        Destroy(this.gameObject); 

    }


    // Setters 
    //
    public void SetProjectileSpeed(float aSpd)
    {

        // Checks if the bullet is going left or right
        if (Mathf.Sign(aSpd) == -1)
            m_ProjectileSpd = aSpd - PROJECTILE_SPD_INCREASE;

        else
            m_ProjectileSpd = aSpd + PROJECTILE_SPD_INCREASE;

    }

    public void SetEnemy(GameObject aEnemy)
    {

        // Grabbing the enmy this way as the class is instantiated and there are many enemies
        theEnemy = aEnemy;

    }
	
}
