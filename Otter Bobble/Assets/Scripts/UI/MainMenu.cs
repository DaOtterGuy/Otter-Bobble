using UnityEngine;
using System.Collections;

// **************************************************************************
// Creator: Ryan Gainford
// Last Updated: June 14, 2016
//
// A class for the Pause menu and its functionality. Can navigate to other 
// menus, and restart the game. 
// **************************************************************************

public class MainMenu : MonoBehaviour
{

    // Constants
    //
    private const int INITIAL_LIVES = 3; 

    [Header("Next Level")]
    [SerializeField] private enSceneToLoad theNextScene; 

    [Header("Managers")]
    [SerializeField] private UIManager theUIManager; 
    [SerializeField] private SoundManager theSoundManager; 

    public void Play()
    {

        theSoundManager.PlaySFX(enSFX.Select);

        // Saves any changed options
        theSoundManager.SaveSoundInfo();

        PersistentData.Instance.theHUDInfo.Lives = INITIAL_LIVES;
        PersistentData.Instance.theHUDInfo.Score = 0;
        PersistentData.Instance.theHUDInfo.PrevScore = 0;

        // Closes the main menu and goes to the game
        UnityEngine.SceneManagement.SceneManager.LoadScene(theNextScene.ToString());

    }

    public void Instructions()
    {

        theSoundManager.PlaySFX(enSFX.Select);

        // Closes the main menu and goes to the credits menu
        theUIManager.ChangeMenu(enChangeMenu.Main, false);
        theUIManager.ChangeMenu(enChangeMenu.Instructions, true);

    }

    public void Options()
    {

        theSoundManager.PlaySFX(enSFX.Select);

        // Closes the main menu and goes to the options menu
        theUIManager.ChangeMenu(enChangeMenu.Main, false);
        theUIManager.ChangeMenu(enChangeMenu.Options, true); 

    }

    public void Credits()
    {

        theSoundManager.PlaySFX(enSFX.Select);

        // Closes the main menu and goes to the credits menu
        theUIManager.ChangeMenu(enChangeMenu.Main, false);
        theUIManager.ChangeMenu(enChangeMenu.Credits, true); 

    }

    public void Exit()
    {

        theSoundManager.PlaySFX(enSFX.Select);

        // Closes the game
        Application.Quit();

    }

}
