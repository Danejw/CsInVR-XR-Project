using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CSInVR
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(AudioSource))]

    // acccess' the animator and sets its trigger to loop in/out
    public class UILoop : MonoBehaviour
    {
        public bool debug;

        Animator animator;

        public bool playAudio;
        private AudioSource source;

        [SerializeField] private AudioClip audioIn;
        [SerializeField] private AudioClip audioOut;

        private void OnEnable()
        {
            animator = GetComponent<Animator>();
            source = GetComponent<AudioSource>();
        }

        // animate loop in
        public void LoopIn()
        {
            if (animator) animator.SetTrigger("Loop In");
            if (playAudio) PlayAudioIn();
        }

        // animate loop out
        public void LoopOut()
        {
            if (animator) animator.SetTrigger("Loop Out");
            if (playAudio) PlayAudioOut();
        }

        public void PlayAudioIn()
        {
            if (audioIn)
            {
                source.clip = audioIn;
                source.Play();
            }
        }

        public void PlayAudioOut()
        {
            if (audioOut)
            {
                source.clip = audioOut;
                source.Play();
            }
        }

        private void Update()
        {
            if (debug)
            {
                // toggle outer loop
                if (Input.GetKeyDown(KeyCode.K))
                    LoopIn();
                if (Input.GetKeyDown(KeyCode.L))
                    LoopOut();
            }
        }
    }
}
