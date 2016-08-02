using UnityEngine;
using System.Collections;

// **************************************************************************
// Creator: Ryan Gainford
// Last Updated: June 14, 2016
//
// A class for the Game Over menu. Shows up when the player runs out of 
// lives. Used to either restart the game or go to the main menu.
// **************************************************************************

public class GameOverMenu : MonoBehaviour
{

    // Constants
    //
    private const int INITIAL_LIVES = 3;

    [Header("Managers")]
    [SerializeField] private GameManager theGameManager;
    [SerializeField] private UIManager theUIManager;
    [SerializeField] private SoundManager theSoundManager;

    public void Restart()
    {

        theSoundManager.PlaySFX(enSFX.Select);

        // Closes the menu
        theUIManager.ChangeMenu(enChangeMenu.GameOver, false);

        // Loads level1 unless the player is on level1 in which case the level will simply be reset
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().ToString() != "Level1")
        {

            // Resets values here as the scene is changing
            PersistentData.Instance.theHUDInfo.Lives = INITIAL_LIVES;
            PersistentData.Instance.theHUDInfo.Score = 0;
            PersistentData.Instance.theHUDInfo.PrevScore = 0; 

            UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");

        }

        else
        {

            theGameManager.Reset();
            theGameManager.Resume();

        }

    }

    public void MainMenu()
    {

        theSoundManager.PlaySFX(enSFX.Select);

        // Closes the game over screen and switches to the main menu scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");

    }

}
