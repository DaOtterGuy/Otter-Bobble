using UnityEngine;
using System.Collections;

// **************************************************************************
// Creator: Ryan Gainford
// Last Updated: June 19, 2016
//
// Class used for all power up functionality. 
// **************************************************************************

public enum enPowerUp { SuperSpeed, DoubleScore, Invincibility, ExtraLife };

public class PowerUp : MonoBehaviour
{

    [Header("Power Up Info")]
    public enPowerUp thePowerUp; 

    [Header("Managers")]
    [SerializeField] private SoundManager theSoundManager;
    [SerializeField] private PlayerManager thePlayerManager;
    [SerializeField] private HUD theHUD; 

    void Start ()
    {

        // Grabbing all of the necessary managers
        theSoundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        thePlayerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        theHUD = GameObject.Find("HUD").GetComponent<HUD>();

    }


    // Activate Power Up Support Functions
    //
    void OnTriggerEnter2D(Collider2D other)
    {

        // Checks that the other is the player
        if (other.CompareTag("Player"))
        {

            theSoundManager.PlaySFX(enSFX.PowerUp);

            // Activates the power up 
            ActivatePowerUp();

        }

    }

    void ActivatePowerUp()
    {

        // Goes to the function needed to perform the power up
        switch (thePowerUp)
        {

            case enPowerUp.SuperSpeed:
                SuperSpeed(); 
                break;

            case enPowerUp.DoubleScore:
                DoubleScore(); 
                break;

            case enPowerUp.Invincibility:
                Invincibility(); 
                break;

            case enPowerUp.ExtraLife:
                ExtraLife(); 
                break;

            default:
                Debug.Log("I am powerless to stop the madness!");
                break;

        }

    }


    // Power Up Functions
    //
    void ExtraLife()
    {

        // Adds a life
        theHUD.AddLife(); 

    }

    void Invincibility()
    {

        // Apply invincibility
        thePlayerManager.ApplyInvincibilityToPlayer(); 

    }

    void SuperSpeed()
    {

        // Apply super speed
        thePlayerManager.ApplySuperSpeed(); 

    }

    void DoubleScore()
    {

        // Apply Double Score
        theHUD.ApplyDoubleScore(); 

    }

    

}
