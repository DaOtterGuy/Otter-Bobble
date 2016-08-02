using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

// **************************************************************************
// Creator: Ryan Gainford
// Last Updated: June 8, 2016
//
// Handles all of the enemies in the game. 
// **************************************************************************

public class EnemyManager : MonoBehaviour, IManager
{

    [Header("Enemy Support Variables")]
    private bool m_IsEnemyActive; 
    public EnemyController[] theEnemies;
    public ActivateChildren theParent;

    [HideInInspector] public List<EnemyController> ListOfActiveEnemies;
    [HideInInspector] public List<GameObject> ListOfInActiveEnemies;

    void Start()
    {

        m_IsEnemyActive = true;

        // Initializes lists of enemies
        ListOfActiveEnemies = new List<EnemyController>();
        ListOfInActiveEnemies = new List<GameObject>();

        // Used to initially fill list of enemies
        FillList(); 

    }

    void Update()
    {

        // checks that the game is not paused
        if (m_IsEnemyActive)
        {

            // Safely iterates through list of enemies to update them
            for (int i = ListOfActiveEnemies.Count - 1; i >= 0; i--)
            {
                ListOfActiveEnemies[i].UpdateEnemies();
            }

        }

    }


    // List of Enemies Support Functions
    //
    void FillList()
    {

        // Clears the list to make sure its empty
        if (ListOfActiveEnemies.Count != 0)
            ListOfActiveEnemies.Clear();

        // Fills it with the enemies and activates them
        for (int i = 0; i < theEnemies.Length; i++)
        {

            ListOfActiveEnemies.Add(theEnemies[i]);

            if(!ListOfActiveEnemies[i].gameObject.activeSelf)
                ListOfActiveEnemies[i].gameObject.SetActive(true); 

        }

    }

    void FillWithInActiveEnemies()
    {

        // Fills it with the enemies
        for (int i = ListOfInActiveEnemies.Count - 1; i >= 0; i--)
        {
            ListOfActiveEnemies.Add(ListOfInActiveEnemies[i].GetComponent<EnemyController>());
        }

        // Clears the list after filling the active list
        if (ListOfInActiveEnemies.Count != 0)
            ListOfInActiveEnemies.Clear();

    }


    // IManager Inheritence 
    //
    public void Resume()
    {

        // Reactivates enemies
        m_IsEnemyActive = true;
         
    }

    public void Pause()
    {

        // Deactivates enemies
        m_IsEnemyActive = false;
         
    }

    public void Reset()
    {

        // Reactivates all enemies
        FillWithInActiveEnemies();

        // Calls the reset for each enemy
        for (int i = ListOfActiveEnemies.Count - 1; i >= 0; i--)
        {
            ListOfActiveEnemies[i].Reset(); 
        }

        // Reactivates all enemies
        theParent.ActivateAll(); 

    }

}
