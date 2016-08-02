using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// **************************************************************************
// Creator: Ryan Gainford
// Last Updated: June 8, 2016
//
// Handles transitioninig ot all of the menus in the game. Also handles 
// updating the HUD. 
// **************************************************************************

public enum enChangeHUD { Score, Lives };
public enum enChangeMenu { GameOver, HUD, Pause, Main, Options, Instructions, Credits };

public class UIManager : MonoBehaviour, IManager 
{

    [Header("HUD Numbers")]
    public int m_LengthOfScore;
    public int m_LengthOfLives; 

    [Header("HUD Text")]
    public Text t_Score;
    public Text t_Lives;

    [Header("The HUD")]
    [SerializeField] private HUD theHUD;

    [Header("Menus")]
    [SerializeField] private GameObject thePauseMenu;
    [SerializeField] private GameObject theGameOverMenu;
    [SerializeField] private GameObject theMainMenu;
    [SerializeField] private GameObject theInstructionsMenu;
    [SerializeField] private GameObject theOptionsMenu;
    [SerializeField] private GameObject theCreditsMenu; 


    // Menu Changing Support Functions
    //
    public void ChangeMenu(enChangeMenu aMenu, bool IsOn)
    {

        // Activates or deactivates the menu depending on the enum and bool passed in
        switch (aMenu)
        {

            case enChangeMenu.GameOver:
                ActivateMenu(theGameOverMenu, IsOn);
                break;

            case enChangeMenu.HUD:
                ActivateMenu(theHUD.gameObject, IsOn);
                break;

            case enChangeMenu.Pause:
                ActivateMenu(thePauseMenu, IsOn);
                break;

            case enChangeMenu.Main:
                ActivateMenu(theMainMenu, IsOn);
                break;

            case enChangeMenu.Instructions:
                ActivateMenu(theInstructionsMenu, IsOn);
                break;

            case enChangeMenu.Options:
                ActivateMenu(theOptionsMenu, IsOn);
                break;

            case enChangeMenu.Credits:
                ActivateMenu(theCreditsMenu, IsOn); 
                break;

            default:
                break;

        }

    }

    void ActivateMenu(GameObject aMenu, bool IsOn)
    {

        // Activates or deactivates a menu based on passed in variables
        aMenu.SetActive(IsOn);

    }


    // HUD Support Functions
    //
    public void UpdateHUD(enChangeHUD enUpdate, int aValue)
    {

        // Updates the part of the HUD that needs updating
        switch (enUpdate)
        {

            case enChangeHUD.Score:
                t_Score.text = FormatText(aValue, m_LengthOfScore);
                break;

            case enChangeHUD.Lives:
                t_Lives.text = FormatText(aValue, m_LengthOfLives);
                break;

            default:
                break;

        }

    }

    string FormatText(int aValue, int aLength)
    {

        // Formats a string based on the value of an int and the desired length of the text
        string formattedText = string.Empty;

        // Gets the character difference between the desired length and the current length
        int CharDiff = aLength - aValue.ToString().Length; 

        // If char diff is 0 then it is already the desired length and will return
        if (CharDiff == 0)
        {
            return aValue.ToString();
        }

        // Adds a 0 to the beginning for each missing char in the desired length
        for (int i = 0; i < CharDiff; i++)
        {
            formattedText += "0";
        }

        // returns the formatted string with the value appended on it
        return formattedText += aValue.ToString(); 

    }


    // IManager Inheritence
    //
    public void Resume() { }

    public void Pause() { }

    public void Reset()
    {
        theHUD.Reset(); 
    }

    

}
