using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CinemticTrigger : MonoBehaviour
{
    public bool CanPlayCinematic = true;

    private PlayableDirector cinematicDirector;
    private ThirdPersonController playerObject;
    
    private void Awake()
    {
        cinematicDirector = GetComponent<PlayableDirector>();
        cinematicDirector.stopped += CinematicDirectorStopped; 
    }

    void CinematicDirectorStopped(PlayableDirector CanPlayCinematic)
    {
        if (cinematicDirector == CanPlayCinematic)
        {
            playerObject.canControl = true; 
         }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && CanPlayCinematic)
        {
            playerObject = other.GetComponent<ThirdPersonController>();

            cinematicDirector.Play(); 

            playerObject.canControl = false;

            CanPlayCinematic = false; 
        }
    }
}
