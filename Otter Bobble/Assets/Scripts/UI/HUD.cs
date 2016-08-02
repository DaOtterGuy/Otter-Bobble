using UnityEngine;
using System.Collections;

// **************************************************************************
// Creator: Ryan Gainford
// Last Updated: June 19, 2016
//
// A class for the HUD. Sends information to the UIManager to update the 
// values in the HUD display. 
// **************************************************************************

public class HUD : MonoBehaviour
{
    // Constants
    //
    private const int INITIAL_LIVES = 3;
    private const int MAX_LIVES = 20;
    private const int MAX_PLAYER_SCORE = 99999999;
    private const float DOUBLE_SCORE_TIME = 18.0F; 

    [Header("HUD Info")]
    public HUDInfo localHUDInfo = new HUDInfo(); 

    [HideInInspector] public bool m_IsGameOver;

    [Header("Double Score Variables")]
    [SerializeField] private int m_ScoreFactor = 1;
    [SerializeField] private float tmr_DoubleScore; 
    [SerializeField] private bool m_IsDoubleScoreOn; 

    [Header("Managers")]
    [SerializeField] private UIManager theUIManager;
    [SerializeField] private GameManager theGameManager; 

    void Start()
    {

        // Loads in data
        LoadHUD();

        theUIManager.UpdateHUD(enChangeHUD.Score, localHUDInfo.Score);
        theUIManager.UpdateHUD(enChangeHUD.Lives, localHUDInfo.Lives);

        m_IsDoubleScoreOn = false; 

    }

    void Update()
    {

        // Double Score Power Up
        {

            if (tmr_DoubleScore > 0.0f && m_IsDoubleScoreOn)
                tmr_DoubleScore -= Time.deltaTime;

            // Turns off double score and resets the score factor
            else if (tmr_DoubleScore <= 0.0f && m_IsDoubleScoreOn)
            {

                m_ScoreFactor = 1;
                m_IsDoubleScoreOn = false; 

            }

        }

    }


    // Player Life Support Functions
    //
    public void AddLife()
    {

        // Chcecks first if the player has reached their maximum number of lives
        if (localHUDInfo.Lives != MAX_LIVES)
        {

            // Increments the number of lives and updates the HUD accordingly
            localHUDInfo.Lives++; 
            theUIManager.UpdateHUD(enChangeHUD.Lives, localHUDInfo.Lives);

        }

        else
        {

            // Gives the player an extra 1000 points if they are at the max number of lives
            localHUDInfo.Score += 1000 * m_ScoreFactor;
            theUIManager.UpdateHUD(enChangeHUD.Score, localHUDInfo.Score);

        }

    }

    public void LoseLife()
    {

        // Decrements a life and updates the HUD through the UIManager
        localHUDInfo.Lives--;
        theUIManager.UpdateHUD(enChangeHUD.Lives, localHUDInfo.Lives);

        // Initiates game over state when the player reaches 0 lives
        if (localHUDInfo.Lives == 0)
        {

            // Flag to let the game know to reset the HUD
            m_IsGameOver = true;

            // Transitions to the game over screen
            theGameManager.Pause();
            theUIManager.ChangeMenu(enChangeMenu.GameOver, true);

        }

        // Resets the level
        else
            theGameManager.Reset(); 

    }


    // Player Score Support Functions
    //
    public void ScorePoints( int aScore )
    {

        if (localHUDInfo.Score != MAX_PLAYER_SCORE)
        {

            // Updates player score in HUD
            localHUDInfo.Score += (aScore * m_ScoreFactor);
            theUIManager.UpdateHUD(enChangeHUD.Score, localHUDInfo.Score);

        }

    }


    // Double Score Support Function
    public void ApplyDoubleScore()
    {

        // Doubles the score factor
        m_ScoreFactor *= 2;

        tmr_DoubleScore = DOUBLE_SCORE_TIME;
        m_IsDoubleScoreOn = true; 

    }


    // Reset 
    // 
    public void Reset()
    {

        // Only reset if the game over state has been achieved
        if (m_IsGameOver)
        {

            // Resets everything to base values
            localHUDInfo.Score = 0;
            localHUDInfo.Lives = INITIAL_LIVES;

            theUIManager.UpdateHUD(enChangeHUD.Score, localHUDInfo.Score);
            theUIManager.UpdateHUD(enChangeHUD.Lives, localHUDInfo.Lives);

            m_IsGameOver = false;

        }

        else
        {

            // Changes the current score to the score the player had at the
            // end of the last level
            localHUDInfo.Score = localHUDInfo.PrevScore;
            theUIManager.UpdateHUD(enChangeHUD.Score, localHUDInfo.Score); 

        }

        // Turns off double score if its on
        if(m_IsDoubleScoreOn)
        {

            tmr_DoubleScore = 0.0f; 
            m_ScoreFactor = 1;
            m_IsDoubleScoreOn = false; 

        }

    }


    // Saving and Loading
    //
    public void SaveHUD()
    {

        // Saving data to persistent data
        PersistentData.Instance.theHUDInfo = localHUDInfo;
        PersistentData.Instance.theHUDInfo.PrevScore = localHUDInfo.Score; 

    }

    public void LoadHUD()
    {

        // Loading in data
        localHUDInfo = PersistentData.Instance.theHUDInfo;

    }

}
