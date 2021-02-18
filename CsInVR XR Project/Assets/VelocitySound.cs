using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CSInVR.Football
{
    public class VelocitySound : MonoBehaviour
    {
        Rigidbody parentRig;
        AudioSource audioSource;
        HikeBall ball;

        public float multiplier = .2f;


        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            parentRig = GetComponentInParent<Rigidbody>();
            ball = GetComponentInParent<HikeBall>();
        }

        private void LateUpdate()
        {
            if (parentRig && audioSource && ball.GetBallIsActive())
            {
                if (!audioSource.isPlaying) audioSource.Play();
                audioSource.volume = parentRig.velocity.normalized.magnitude * multiplier;
            }

            if (parentRig && audioSource && !ball.GetBallIsActive())
                if (audioSource.isPlaying) audioSource.Stop();
        }
    }
}
