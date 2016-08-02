using UnityEngine;
using System.Collections;

// **************************************************************************
// Creator: Ryan Gainford
// Last Updated: June 14, 2016
//
// Stores classes that can be used for saving and loading information. 
// **************************************************************************

[System.Serializable]
public class SoundInfo
{

    public float SFXVolume;
    public float MusicVolume; 
    public bool Mute;

}

[System.Serializable]
public class HUDInfo
{

    public int Lives;
    public int Score;
    public int PrevScore; 

}
