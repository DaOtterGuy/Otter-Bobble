using UnityEngine;
using System.Collections;

// **************************************************************************
// Creator: Ryan Gainford
// Last Updated: June 8, 2016
//
// The Enemy Controller class is used to control enemies and set their 
// behaviours. 
// **************************************************************************

public enum enEnemyType { None, Patrol, Circle, Wander, Homing, Shooter };

public class EnemyController : MonoBehaviour 
{

    [Header("Enemy Basics")]
    public Vector2 m_StartPos;
    public float m_EnemySpd;
    public enEnemyType m_EnemyType;

    private Vector3 m_InitialScale; 

    [HideInInspector] public IEnemyType m_CurrentType;

    [Header("Projectile")]
    public GameObject pref_EnemyProjectile; 

    [Header("Managers")]
    [SerializeField] private EnemyManager theEnemyManager;

	void Start () 
    {

        m_StartPos = this.transform.position;
        m_InitialScale = this.transform.localScale; 

        // Starts the enemy's behaviour
        ActivateBehaviour();
	
	}
	
	public void UpdateEnemies () 
    {

        // Update enemy behaviour
        if (m_CurrentType != null)
            m_CurrentType.UpdateBehaviour();
	
	}


    // Enemy Behaviour Activation Functions
    //
    void ActivateBehaviour()
    {

        // Initilizes the enemy behaviour based on the chosen enemy type
        switch (m_EnemyType)
        {

            case enEnemyType.None:
                m_CurrentType = null; 
                break;

            case enEnemyType.Patrol:
                m_CurrentType = (IEnemyType)this.gameObject.AddComponent<PatrolType>(); 
                break;

            case enEnemyType.Circle:
                m_CurrentType = (IEnemyType)this.gameObject.AddComponent<CircleType>(); 
                break;

            case enEnemyType.Wander:
                m_CurrentType = (IEnemyType)this.gameObject.AddComponent<WanderType>();
                break;

            case enEnemyType.Homing:
                m_CurrentType = (IEnemyType)this.gameObject.AddComponent<HomingType>();
                break;

            case enEnemyType.Shooter:
                m_CurrentType = (IEnemyType)this.gameObject.AddComponent<ShooterType>(); 
                break; 

            default:
                Debug.Log("Enemy has no purpose in life, suffering from existential crisis");
                break;

        }

        // Activates the chosen behaviour
        if (m_CurrentType != null)
            m_CurrentType.ActivateEnemy(this.gameObject, m_EnemySpd);
        
    }


    // Deactivation Support Functions
    //
    public void DeactivateBehaviour()
    {
        m_CurrentType.DeactivateEnemy(); 
    }

    public void RemoveFromList()
    {

        // Removes the enemy from the active list and puts them in the inactive one
        theEnemyManager.ListOfActiveEnemies.Remove(this);
        theEnemyManager.ListOfInActiveEnemies.Add(this.gameObject); 

    }

    public void Reset()
    {

        // Restarts the enemy and its behaviour
        m_CurrentType.Reset( m_InitialScale, m_StartPos );
        m_CurrentType.ActivateEnemy(this.gameObject, m_EnemySpd); 

    }

}
