using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFXManager : MonoBehaviour
{
    public AudioClip[] soundClips;
    public AudioSource AS;
    // Start is called before the first frame update
    void Start()
    {
        AS= GetComponent<AudioSource>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(int index)
    {
        AS.PlayOneShot(soundClips[index]);
    }
}
