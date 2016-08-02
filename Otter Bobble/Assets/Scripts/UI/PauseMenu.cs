using UnityEngine;
using System.Collections;

// **************************************************************************
// Creator: Ryan Gainford
// Last Updated: June 21, 2016
//
// A class for the Pause menu and its functionality. Can navigate to other 
// menus, and restart the game. 
// **************************************************************************

public class PauseMenu : MonoBehaviour 
{

    [Header("The HUD")]
    [SerializeField] private HUD theHUD; 

    [Header("Managers")]
    [SerializeField] private GameManager theGameManager; 
    [SerializeField] private UIManager theUIManager;
    [SerializeField] private SoundManager theSoundManager; 

    void Update()
    {

        // Allows the player to quickly escape the pause menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            theSoundManager.PlaySFX(enSFX.Select);

            theGameManager.Resume();
            theUIManager.ChangeMenu(enChangeMenu.Pause, false);

        }

    }

    public void Resume()
    {

        theSoundManager.PlaySFX(enSFX.Select);

        // Continues the game and closes the menu
        theGameManager.Resume();
        theUIManager.ChangeMenu(enChangeMenu.Pause, false);
    
    }

    public void Restart()
    {

        theSoundManager.PlaySFX(enSFX.Select);

        // Makes sure the HUD is reset
        theHUD.m_IsGameOver = true;

        // Resets everything then starts the game again
        theGameManager.Reset();
        theGameManager.Resume();

        if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().ToString() == "Level1")
            theUIManager.ChangeMenu(enChangeMenu.Pause, false); 
        
        else
            UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");

    }

    public void Options()
    {

        theSoundManager.PlaySFX(enSFX.Select);

        // Closes the pause menu and opens the options menu
        theUIManager.ChangeMenu(enChangeMenu.Pause, false);
        theUIManager.ChangeMenu(enChangeMenu.Options, true); 

    }

    public void MainMenu()
    {

        theSoundManager.PlaySFX(enSFX.Select);

        // Saves any changed options
        theSoundManager.SaveSoundInfo();

        // Closes the pause menu and switches to the main menu
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");

    }

}
