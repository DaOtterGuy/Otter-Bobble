using UnityEngine;
using System.Collections;

// **************************************************************************
// Creator: Ryan Gainford
// Last Updated: June 21, 2016
//
// Handles the other managers. Mostly used to resume, pause, and reset any
// gameplay elements.
// **************************************************************************

public class GameManager : MonoBehaviour
{

    // Constants
    //
    private const float GAME_START_TIME = 3.0F;

    [Header("Timer")]
    public float tmr_GameTimer;
    public bool m_IsGameStart; 

    [Header("All Managers")]
    public GameObject[] ArrayOfManagers;    
    private IManager[] theManagers;

    [Header("Managers")]
    [SerializeField] private SoundManager theSoundManager;        

    void Start()
    {

        // creates and stores the managers as IManagers in an array
        theManagers = new IManager[ArrayOfManagers.Length];
        for (int i = 0; i < ArrayOfManagers.Length; i++)
        {
            theManagers[i] = ArrayOfManagers[i].GetComponent<IManager>();
        }

        // Plays Music
        theSoundManager.PlayMusic(); 

        tmr_GameTimer = GAME_START_TIME; 
        m_IsGameStart = false; 

    }

    void Update()
    {

        if(!m_IsGameStart && tmr_GameTimer == GAME_START_TIME)
            Pause();

        if (tmr_GameTimer > 0.0f)
            tmr_GameTimer -= Time.deltaTime;

        if (tmr_GameTimer <= 0.0f && !m_IsGameStart)
        {
            m_IsGameStart = true;
            Resume(); 
        }

    }


    // IManager Inheritence
    // 
    public void Resume()
    {

        // calls each Managers resume
        foreach (IManager aManager in theManagers)
        {
            aManager.Resume();
        }

    }

    public void Pause()
    {

        // calls each Managers Pause
        foreach (IManager aManager in theManagers)
        {
            aManager.Pause();
        }

    }

    public void Reset()
    {

        // calls each Managers reset
        foreach (IManager aManager in theManagers)
        {
            aManager.Reset();
        }

    }

    

}
