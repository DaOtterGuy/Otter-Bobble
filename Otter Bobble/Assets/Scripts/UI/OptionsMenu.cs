using UnityEngine;
using System.Collections;
using UnityEngine.UI; 

// **************************************************************************
// Creator: Ryan Gainford
// Last Updated: June 10, 2016
//
// A class for the options menu. Changes volume on sounds and mutes the game. 
// **************************************************************************

public class OptionsMenu : MonoBehaviour
{

    [Header("Menu")]
    public enChangeMenu theMenu;

    [Header("Options Buttons")]
    [SerializeField] private Toggle theMuteToggle;
    [SerializeField] private Slider theMusicVolumeSlider;
    [SerializeField] private Slider theSFXVolumeSlider; 

    [Header("Managers")]
    [SerializeField] private UIManager theUIManager;
    [SerializeField] private SoundManager theSoundManager;

    void Start()
    {

        // Load in saved data
        theMusicVolumeSlider.value = PersistentData.Instance.theSoundInfo.MusicVolume;
        theSFXVolumeSlider.value = PersistentData.Instance.theSoundInfo.SFXVolume; 
        theMuteToggle.isOn = PersistentData.Instance.theSoundInfo.Mute; 

    }

    public void Back()
    {

        theSoundManager.PlaySFX(enSFX.Select);

        // Closes Options menu and goes back to previous menu
        theUIManager.ChangeMenu(enChangeMenu.Options, false);
        theUIManager.ChangeMenu(theMenu, true); 

    }

    public void Mute()
    {

        // Sends the toggle state to the sound manager
        theSoundManager.MuteSound(theMuteToggle.isOn);

    }

    public void VolumeSFX()
    {

        // Changes the volume of the SFX based on the slider
        theSoundManager.ChangeSFXVolume(theSFXVolumeSlider.value); 

    }

    public void VolumeMusic()
    {

        // Changes the volume of the music based on the slider
        theSoundManager.ChangeMusicVolume(theMusicVolumeSlider.value);

    }

}
