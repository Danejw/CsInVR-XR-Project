using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSInVR.Football
{
    [RequireComponent(typeof(AudioSource))]

    public class CrowdSoundController : MonoBehaviour
    {
        [SerializeField] AudioClip[] audioClips;

        private AudioSource audioSource;



        private void Start()
        {
            audioSource = GetComponent<AudioSource>();

            PlayNormalCrowd();
        }

        public void PlayNormalCrowd()
        {
            if (audioSource && audioClips != null)
            {
                audioSource = GetComponent<AudioSource>();

                audioSource.clip = audioClips[0];

                audioSource.Play();
            }
        }

        public void PlayCheeringCrowd()
        {
            if (audioSource && audioClips != null)
            {
                audioSource = GetComponent<AudioSource>();

                audioSource.clip = audioClips[1];

                audioSource.Play();
            }
        }

        public void PlayHypedCrowd()
        {
            if (audioSource && audioClips != null)
            {
                audioSource = GetComponent<AudioSource>();

                audioSource.clip = audioClips[2];

                audioSource.Play();
            }
        }

        public void PlayTouchdownCrowd()
        {
            if (audioSource && audioClips != null)
            {
                audioSource = GetComponent<AudioSource>();

                audioSource.clip = audioClips[3];

                audioSource.Play();
            }
        }

        public void PlayMissedOpportunity()
        {
            if (audioSource && audioClips != null)
            {
                audioSource = GetComponent<AudioSource>();

                audioSource.clip = audioClips[4];

                audioSource.Play();
            }
        }

    }
}
