using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;
using UnityEngine.XR;


namespace CSInVR
{
    [RequireComponent(typeof(AudioSource))]
    public class VelocityBasedSound : GrabbableEvents
    {
        [SerializeField]
        private bool velocityBasedVolume;

        private AudioSource audioSource;

        [SerializeField]
        private AudioClip velocityClip;
        [SerializeField]
        private AudioClip triggerClip;

        [HideInInspector]
        public Vector3 velocity;

        [SerializeField]
        private float volumeFloor = 0f;


        void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        void FixedUpdate()
        {
            if (thisGrabber != null)
            {
                GetHandControllerVelocity();
                PlayVelocityAudio();
            }
            else audioSource.Stop();

        }


        // trigger based volume
        public override void OnTrigger(float triggerValue)
        {
            base.OnTrigger(triggerValue);

            if (triggerValue > .4f && triggerValue <= 1)
            {
                if (audioSource.isPlaying)
                    audioSource.Pause();

                velocityBasedVolume = false;

                if (triggerClip != null)
                    audioSource.clip = triggerClip;

                audioSource.volume = triggerValue;
                //print(triggerValue);

                if (!audioSource.isPlaying)
                    audioSource.Play();
                else
                    audioSource.UnPause();
            }
            else
            {
                velocityBasedVolume = true;
                PlayVelocityAudio();
            }
        }

        public override void OnRelease()
        {
            base.OnRelease();
            audioSource.Stop();
            thisGrabber = null;
        }


        // Gets this grabber velocity value and stores it as a variable
        private void GetHandControllerVelocity()
        {
            if (thisGrabber.HandSide == ControllerHand.Left)
            {
                InputBridge.Instance.GetLeftController().TryGetFeatureValue(CommonUsages.deviceVelocity, out velocity);

            }
            if (thisGrabber.HandSide == ControllerHand.Right)
            {
                InputBridge.Instance.GetRightController().TryGetFeatureValue(CommonUsages.deviceVelocity, out velocity);
            }
        }

        private void PlayVelocityAudio()
        {
            if (velocityBasedVolume)
            {
                if (audioSource.isPlaying)
                    audioSource.Pause();

                if (velocityClip != null)
                    audioSource.clip = velocityClip;

                float amplitude = Remap(velocity.magnitude, 0, 3);
                Mathf.Lerp(0, 1, amplitude);

                amplitude = Mathf.Sqrt(amplitude * amplitude);

                //Debug.Log("Amplitude: " + amplitude);

                audioSource.volume = amplitude + volumeFloor;

                if (!audioSource.isPlaying)
                    audioSource.Play();
                else
                    audioSource.UnPause();
            }
        }

        private float Remap(float value, float min, float max)
        {
            float scaledValue = (value - min) / (max - min);
            scaledValue = value / max;
            return scaledValue;
        }
    }
}
