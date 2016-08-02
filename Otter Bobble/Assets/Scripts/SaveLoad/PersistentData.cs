using UnityEngine;
using System.Collections;

// **************************************************************************
// Creator: Ryan Gainford
// Last Updated: June 14, 2016
//
// Stores the sound data of the options menu to keep throughout the game. 
// **************************************************************************

public class PersistentData : MonoBehaviour
{

    [Header("Singleton")]
    public static PersistentData Instance;

    [Header("Data To Save")]
    public SoundInfo theSoundInfo = new SoundInfo();
    public HUDInfo theHUDInfo = new HUDInfo(); 

    void Awake()
    {

        // Makes singleton 
        if (Instance == null)
        {

            // This makes it so there is only every one and it isn't destroyed between 
            // scene transitions. 
            DontDestroyOnLoad(this.gameObject);
            Instance = this;

        }

        else if (Instance != this)
            Destroy(this);

    }

}
