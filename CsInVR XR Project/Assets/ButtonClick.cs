using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace CSInVR.UI
{
    [RequireComponent(typeof(AudioSource))]

    public class ButtonClick : MonoBehaviour
    {

        [SerializeField] private AudioClip downClip;
        [SerializeField] private AudioClip upClip;

        private AudioSource source;

        private void OnEnable()
        {
            source = GetComponent<AudioSource>();
        }

        public void PlayDownClickAudio()
        {
            if (downClip)
            {
                source.clip = downClip;
                source.Play();
            }
        }

        public void PlayUpClickAudio()
        {
            if (upClip)
            {
                source.clip = upClip;
                source.Play();
            }
        }
    }
}
