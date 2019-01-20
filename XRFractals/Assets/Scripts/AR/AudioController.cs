using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

    public AudioSource ambientSound;

    // Attach this to the camera 

    void Start()
    {
        ambientSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Hit!");
        if (other.gameObject.tag == "Door")
        {
            Debug.Log("Hitted the door!");
            if (!ambientSound.isPlaying) ambientSound.Play();
            else ambientSound.Stop();
        }
    }


    void OnTriggerExit(Collider other)
    {
        //ambientSound.Stop();
    }

}

