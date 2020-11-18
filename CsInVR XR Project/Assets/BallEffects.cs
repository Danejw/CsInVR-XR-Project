using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
[RequireComponent(typeof(AudioSource))]

public class BallEffects : MonoBehaviour
{
    public bool debug;
    public bool rigidbodyInParent;
    public bool speedDetection;
    public bool playSoundOnSpeedThreshold;
    //public bool playOnCollision;

    public float speedThreshold;

    public AudioClip audioClip;

    private Rigidbody rig;

    ParticleSystem particle;
    AudioSource audioSource;
    


    private void OnEnable()
    {
        particle = GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();

        TryGetComponent<Rigidbody>(out rig);

        if (rigidbodyInParent) rig = GetComponentInParent<Rigidbody>();
        else TryGetComponent<Rigidbody>(out rig);
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        if (playOnCollision && !other.CompareTag("Player"))
        {
            if (rig.velocity.sqrMagnitude > speedThreshold)
            {
                if (!particle.isPlaying)
                    particle.Play();
            }
        }
    }
    */

    private void Update()
    {
        // Play particle
        if (speedDetection && rig)
        {
            if (debug) Debug.Log("Ball Velocity: " + rig.velocity.sqrMagnitude);

            if (rig.velocity.sqrMagnitude > speedThreshold)
            {
                if (!particle.isPlaying)
                    particle.Play();
            }
            else
            {
                if (particle.isPlaying)
                    particle.Stop();
            }
        }

        // Play Sound
        if (playSoundOnSpeedThreshold && rig)
        {
            if (debug) Debug.Log("Playing ball speed sound");

            if (!audioClip) return;

            audioSource.clip = audioClip;

            if (rig.velocity.sqrMagnitude > speedThreshold)
            {
                if (!audioSource.isPlaying)
                    audioSource.Play();
            }
            else
            {
                if (audioSource.isPlaying)
                    audioSource.Stop();
            }
        }
    }
}
