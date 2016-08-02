using UnityEngine;
using System.Collections;

// **************************************************************************
// Creator: Ryan Gainford
// Last Updated: June 14, 2016
//
// Transitions the player to the next level. 
// **************************************************************************

public enum enSceneToLoad { MainMenu, Level1, Level2, Level3, Level4, Level5};

public class EndGoal : MonoBehaviour
{

    // TODO: Load next level
    // TODO: Speed up camera to final position
    // TODO: Transition

    [Header("Level Transition")]
    public enSceneToLoad theNextLevel;

    [Header("Managers")]
    [SerializeField] private HUD theHUD; 
    [SerializeField] private SoundManager theSoundManager; 

	void OnTriggerEnter2D( Collider2D other )
    {

        theSoundManager.SaveSoundInfo();
        theHUD.SaveHUD(); 

        // Transitions to next level
        UnityEngine.SceneManagement.SceneManager.LoadScene(theNextLevel.ToString());

    }

}
