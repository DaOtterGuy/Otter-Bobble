using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

// **************************************************************************
// Creator: Ryan Gainford
// Last Updated: June 7, 2016
//
// Handles the various particle systems in the game. 
// **************************************************************************

public enum enParticles { BubblePop };

public class ParticleSystemManager : MonoBehaviour, IManager
{

    [Header("Prefabs")]
    public ParticleSystem pref_BubblePop;
    private List<ParticleSystem> ListOfParticles; 

    void Start()
    {
        ListOfParticles = new List<ParticleSystem>();
    }

    void Update()
    {

        if(ListOfParticles.Count != 0)
        {

            // Iterates through the list safely to find any particles that need
            // to be destroyed
            for (int i = ListOfParticles.Count - 1; i >= 0; i--)
            {

                ParticleSystem tempPS = ListOfParticles[i]; 

                // Checks if the particle system is still going
                if( !tempPS.IsAlive() )
                {
                    // Removes and destroys the particle system
                    ListOfParticles.Remove(tempPS);
                    Destroy(tempPS.gameObject); 
                }

            }

        }

    }
 

    // Particle Handling Support Functions
    // 
    public void PlayParticles( enParticles aParticle, Vector3 aPos )
    {

        // Creates the particles needed using a function that takes in a prefab
        switch (aParticle)
        {

            case enParticles.BubblePop:
                CreateParticles(pref_BubblePop, aPos);
                break;

            default:
                Debug.Log("No Sparklies.");
                break;

        }

    }

    void CreateParticles(ParticleSystem aPrefab, Vector3 aPos)
    {

        // Creates and positions the particles
        ParticleSystem aParticleSystem = Instantiate(aPrefab);
        aParticleSystem.transform.position = aPos;
        aParticleSystem.Play();

        // Adds to the list of active particles
        ListOfParticles.Add(aParticleSystem);

    }


    // IManager Inheritence
    //
    public void Resume()
    {

        // Iterates safely through to play all active particles
        for (int i = ListOfParticles.Count - 1; i >= 0; i--)
        {
            ListOfParticles[i].Play();
        }

    }

    public void Pause()
    {

        // Iterates safely through to pause all active particles
        for (int i = ListOfParticles.Count - 1; i >= 0; i--)
        {
            ListOfParticles[i].Pause();
        }

    }

    public void Reset()
    {

        // Iterates through the list safely to destroy all particles in the list
        for (int i = ListOfParticles.Count - 1; i >= 0; i--)
        {

            ParticleSystem tempPS = ListOfParticles[i];
            
            // Removes and destroys the particle system
            ListOfParticles.Remove(tempPS);
            Destroy(tempPS.gameObject);

        }

    }

}
