using UnityEngine;
using System.Collections;

// **************************************************************************
// Creator: Ryan Gainford
// Last Updated: June 10, 2016
//
// Handles the Main Camera in the game, specifically its movement in the 
// level. 
// **************************************************************************

public class CameraManager : MonoBehaviour, IManager
{

    // TODO: Start moving only after a certain amount of time has passed
    // TODO: Trigger level complete upon camera reaching end goal

    [Header("Camera Positions")]
    public Transform m_CameraStartPos;
    public Transform m_CameraEndPos;

    [HideInInspector] public bool m_IsCameraMoving; 

    [Header("The Camera")]
    [SerializeField] private Camera theCamera;

    void Start()
    {

        // Grabs the main camera and starts camera movement
        theCamera = Camera.main;
        m_IsCameraMoving = true; 

    }

    void Update()
    {

        // Calls the function to move the camera
        if(m_IsCameraMoving)
            theCamera.GetComponent<Scroll>().MoveObject(theCamera.transform.position, m_CameraEndPos.position);

    }


    // IManager Inheritence
    // 
    public void Resume()
    {

        // Continues camera movement
        m_IsCameraMoving = true; 

    }

    public void Pause()
    {

        // Stops camera movement
        m_IsCameraMoving = false; 

    }

    public void Reset()
    {

        // Sets the camera back to the beginning
        theCamera.transform.position = m_CameraStartPos.position; 
        
    }

}
