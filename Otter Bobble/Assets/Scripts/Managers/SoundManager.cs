using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

// **************************************************************************
// Creator: Ryan Gainford
// Last Updated: June 21, 2016
//
// Handles the Music and Sound effects (SFX) in the game.
// **************************************************************************

public enum enSFX { Pop, Jump, Shoot, Death, Select, PowerUp, Beep, Pickup };     

public class SoundManager : MonoBehaviour, IManager
{
    
    [Header("Music")]
    public AudioSource theTheme;

    [Header("SFX")]
    public AudioSource SFX_Pop;
    public AudioSource SFX_Jump;
    public AudioSource SFX_Shoot;
    public AudioSource SFX_Death;
    public AudioSource SFX_Select;
    public AudioSource SFX_PowerUp;
    public AudioSource SFX_TimerBeep;
    public AudioSource SFX_Pickup; 

    [SerializeField] private AudioSource[] ArrayOfSFX;

    [Header("Managers")]
    [SerializeField] private GameManager theGameManager; 

    [Header("Save Data")]
    public SoundInfo localSoundInfo = new SoundInfo(); 


    void Start()
    {

        // Loads in data
        LoadSoundInfo();

        ChangeSFXVolume(localSoundInfo.SFXVolume); 
        ChangeMusicVolume(localSoundInfo.MusicVolume); 
        MuteSound(localSoundInfo.Mute); 

    }

    // Music Support Functions
    //
    public void PlayMusic()
    {

        // Plays the song
        theTheme.Play(); 

    }

    public void PauseMusic()
    {

        // Pauses the song
        theTheme.Pause();

    }


    // Sound Effects Support Functions
    //
    public void PlaySFX(enSFX aSoundEffect)
    {

        // Plays a sound effect based on the enum sent in
        switch (aSoundEffect)
        {

            case enSFX.Pop:
                SFX_Pop.Play();
                break;

            case enSFX.Jump: 
                SFX_Jump.Play();
                break;

            case enSFX.Shoot:
                SFX_Shoot.Play();
                break;

            case enSFX.Death: 
                SFX_Death.Play(); 
                break;

            case enSFX.Select:
                SFX_Select.Play(); 
                break;

            case enSFX.PowerUp:
                SFX_PowerUp.Play(); 
                break;

            case enSFX.Beep:
                SFX_TimerBeep.Play(); 
                break;

            case enSFX.Pickup:
                SFX_Pickup.Play();
                break;

            default:
                break;

        }

    }


    // Options Support Functions
    //
    public void MuteSound( bool IsMute )
    {

        // Mutes all sounds in the game
        theTheme.mute = IsMute;

        for (int i = 0; i < ArrayOfSFX.Length; i++)
        {
            ArrayOfSFX[i].mute = IsMute; 
        }

        // For saving data later
        localSoundInfo.Mute = IsMute; 

    }

    public void ChangeMusicVolume( float aVolume )
    {

        // Changes the volume of the music
        theTheme.volume = aVolume;

        // For saving data later
        localSoundInfo.MusicVolume = aVolume;

    }

    public void ChangeSFXVolume( float aVolume )
    {

        // Changes the volume of the SFX
        for (int i = 0; i < ArrayOfSFX.Length; i++)
        {
            ArrayOfSFX[i].volume = aVolume; 
        }

        // For saving data later
        localSoundInfo.SFXVolume = aVolume; 

    }


    // IManager Inheritence
    //
    public void Resume()
    {

        if (!theTheme.isPlaying)
        {

            // Plays music again
            PlayMusic();

        }

    }

    public void Pause()
    {

        if (theGameManager.m_IsGameStart)
        {

            // Pauses music
            PauseMusic();

        }

    }

    public void Reset()
    {

        // Resets the music
        theTheme.time = 0.0f;

    }


    // Saving and Loading
    //
    public void SaveSoundInfo()
    {

        // Saves data into PersistentData
        PersistentData.Instance.theSoundInfo = localSoundInfo; 

    }

    public void LoadSoundInfo()
    {

        // Loads data from PersistentData
        localSoundInfo = PersistentData.Instance.theSoundInfo; 

    }

}
