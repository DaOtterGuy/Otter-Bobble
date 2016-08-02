using UnityEngine;
using System.Collections;

// **************************************************************************
// Creator: Ryan Gainford
// Last Updated: June 5, 2016
//
// An interface inherited by all managers to be able to reset, resume, and 
// pause all at once in the Game Manager class. 
// **************************************************************************

public interface IManager 
{

    void Resume(); 
    void Pause(); 
    void Reset();   

}
