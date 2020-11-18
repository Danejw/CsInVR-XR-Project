using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;
using UnityEngine.XR;

namespace CSInVR
{
    public class VelocityBasedHaptics : GrabbableHaptics
    {
        public bool testVibration;

        [SerializeField]
        private bool useLinearVelocity;
        [SerializeField]
        private bool useAngularVelocity;

        [SerializeField]
        private float vibrationFloor = 0;

        [HideInInspector]
        public Vector3 velocity;
        [HideInInspector]
        public Vector3 angularVelocity;


        private void Awake()
        {

        }

        private void FixedUpdate()
        {
            //this.transform.SetPositionAndRotation(hand.transform.position, hand.transform.rotation);

            // used to test if vibration works on the left controller
            if (testVibration)
            {
                testVibration = false;
                InputBridge.Instance.GetLeftController().SendHapticImpulse(0, VibrateAmplitude, VibrateDuration);
                InputBridge.Instance.GetRightController().SendHapticImpulse(0, VibrateAmplitude, VibrateDuration);
            }

            if (currentGrabber != null && useLinearVelocity)
            {
                InvokeRepeating("LinearVibrateHandController", VibrateDuration, VibrateFrequency);
            }
            if (currentGrabber == null)
            {
                CancelInvoke("LinearVibrateHandController");
            }

            if (currentGrabber != null && useAngularVelocity)
            {
                InvokeRepeating("AngularVibrateHandController", VibrateDuration, VibrateFrequency);
            }
            if (currentGrabber == null)
            {
                CancelInvoke("AngularVibrateHandController");
            }


        }


        // maps objects velocity to the vibrations amplitude and plays haptic to the corresponding hand
        private void LinearVibrateHandController()
        {
            if (currentGrabber == null)
                return;

            float amplitude = Remap(velocity.magnitude, 0, 3);
            Mathf.Lerp(0, 1, amplitude);
            Mathf.Clamp(amplitude, 0, 1);

            amplitude = Mathf.Sqrt(amplitude * amplitude);

            //Debug.Log("Amplitude: " + amplitude);

            if (currentGrabber.HandSide == ControllerHand.None)
                return;

            if (currentGrabber.HandSide == ControllerHand.Left)
            {
                InputBridge.Instance.GetLeftController().TryGetFeatureValue(CommonUsages.deviceVelocity, out velocity);
                InputBridge.Instance.GetLeftController().SendHapticImpulse(0, amplitude + vibrationFloor, VibrateDuration);
            }
            if (currentGrabber.HandSide == ControllerHand.Right)
            {
                InputBridge.Instance.GetRightController().TryGetFeatureValue(CommonUsages.deviceVelocity, out velocity);
                InputBridge.Instance.GetRightController().SendHapticImpulse(0, amplitude + vibrationFloor, VibrateDuration);
            }
        }

        private void AngularVibrateHandController()
        {
            if (currentGrabber == null)
                return;

            float amplitude = Remap(angularVelocity.magnitude, 0, 3);
            Mathf.Lerp(0, 1, amplitude);
            Mathf.Clamp(amplitude, 0, 1);

            amplitude = Mathf.Sqrt(amplitude * amplitude);

            //Debug.Log("Amplitude: " + amplitude);

            if (currentGrabber.HandSide == ControllerHand.None)
                return;

            if (currentGrabber.HandSide == ControllerHand.Left)
            {
                InputBridge.Instance.GetLeftController().TryGetFeatureValue(CommonUsages.deviceAngularVelocity, out angularVelocity);
                InputBridge.Instance.GetLeftController().SendHapticImpulse(0, amplitude + vibrationFloor, VibrateDuration);
            }
            if (currentGrabber.HandSide == ControllerHand.Right)
            {
                InputBridge.Instance.GetRightController().TryGetFeatureValue(CommonUsages.deviceAngularVelocity, out angularVelocity);
                InputBridge.Instance.GetRightController().SendHapticImpulse(0, amplitude + vibrationFloor, VibrateDuration);
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
