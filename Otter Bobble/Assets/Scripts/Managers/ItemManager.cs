using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

// **************************************************************************
// Creator: Ryan Gainford
// Last Updated: June 19, 2016
//
// Manager used to reactivate all of the items and deal with item drops and
// power ups. 
// **************************************************************************

public class ItemManager : MonoBehaviour, IManager
{

    private bool m_AreItemsActive; 
    [SerializeField] private ActivateChildren theParent; 

    [HideInInspector] public List<GameObject> ListOfItemDrops; 

    void Start()
    {

        // Initialize list
        ListOfItemDrops = new List<GameObject>();

        m_AreItemsActive = true;  

    }

    void Update()
    {

        // Stops any updating if the game is paused
        if( m_AreItemsActive )
        {

            foreach(Transform child in theParent.transform)
            {
                child.GetComponent<ScoreItem>().UpdateItem(false); 
            }

            // Checks if there are any dropped items
            if(ListOfItemDrops.Count != 0)
            {

                // Safely updates all dropped items
                for (int i = ListOfItemDrops.Count - 1; i >= 0; i--)
                {
                    ListOfItemDrops[i].GetComponent<ScoreItem>().UpdateItem(true);
                }

            }

        }

    }

    public void Resume()
    {

        // Continues updating items
        m_AreItemsActive = true;

    }

    public void Pause()
    {

        // Stops updating items
        m_AreItemsActive = false; 

    }

    public void Reset()
    {

        // Destorys any active item drops before resetting
        if (ListOfItemDrops.Count != 0)
        {

            for (int i = ListOfItemDrops.Count - 1; i >= 0; i--)
            {

                // Creates a temp to destroy the object
                GameObject toBeDestroyed = ListOfItemDrops[i]; 

                // Removes the game object from the list then destroys it
                ListOfItemDrops.Remove(toBeDestroyed); 
                Destroy(toBeDestroyed); 

            }

        }

        // Reactivates all items in game
        theParent.ActivateAll(); 

    }

}
