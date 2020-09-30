using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ambience_script : MonoBehaviour
{
    //UNUSED
    public AudioClip ambience;
    private AudioSource playerAudio;
    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        playerAudio.PlayOneShot(ambience);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
