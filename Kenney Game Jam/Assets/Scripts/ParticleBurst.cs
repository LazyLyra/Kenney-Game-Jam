using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBurst : MonoBehaviour
{
    public ParticleSystem PS;

    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        PS = GetComponent<ParticleSystem>();

        timer = 0f;

        TriggerBurst();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 0.6f)
        {
            GameObject.Destroy(gameObject);
        }
    }
    
    void TriggerBurst()
    {
        if (!PS.isPlaying)
        {
            PS.Play();
        }

        ParticleSystem.Burst burst = new ParticleSystem.Burst(0f, 100f);
        PS.emission.SetBurst(0, burst);
    }
}
