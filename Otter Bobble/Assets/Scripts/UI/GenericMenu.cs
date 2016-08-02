using UnityEngine;
using System.Collections;

// **************************************************************************
// Creator: Ryan Gainford
// Last Updated: June 8, 2016
//
// Shows the Credits to anyone who contributed to the game.
// **************************************************************************

public class GenericMenu : MonoBehaviour
{

    [Header("Menu")]
    [SerializeField] private enChangeMenu theMenu; 

    [Header("Managers")]
    [SerializeField] private UIManager theUIManager;
    [SerializeField] private SoundManager theSoundManager; 

    public void Back()
    {

        theSoundManager.PlaySFX(enSFX.Select); 

        // Closes the credits menu and goes back to the Main Menu
        theUIManager.ChangeMenu(theMenu, false);
        theUIManager.ChangeMenu(enChangeMenu.Main, true); 

    }

}
