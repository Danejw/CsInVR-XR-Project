using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR;
using BNG;
using UnityEngine;


namespace CSInVR
{
    public class DistanceBasedHaptics : GrabbableHaptics
    {
        public bool debug;

         private float amplitude;
         private bool isClosestRemoteGrabbable = false;
         
         private Grabber grabber;


        private void FixedUpdate()
        {
            if (isClosestRemoteGrabbable)
                InvokeRepeating("VibrateController", VibrateDuration, VibrateFrequency);
            if (!isClosestRemoteGrabbable)
                CancelInvoke("VibrateController");
        }

        public override void OnBecomesClosestRemoteGrabbable(Grabber theGrabber)
        {
            base.OnBecomesClosestRemoteGrabbable(theGrabber);

            grabber = theGrabber;
            isClosestRemoteGrabbable = true;
        }

        public override void OnNoLongerClosestRemoteGrabbable(Grabber theGrabber)
        {
            base.OnNoLongerClosestRemoteGrabbable(theGrabber);

            grabber = null;
            isClosestRemoteGrabbable = false;
        }

        private void VibrateController()
        {
            // if (grabber.HandSide == ControllerHand.None)
            //     return;
            /*
            switch (grabber.HandSide)
            {
                // vibrate left controller
                case ControllerHand.Left:
                    InputBridge.Instance.GetLeftController().SendHapticImpulse(0, CalculateAmplitudeFromDistance(grabber), VibrateDuration);
                    return;

                // vibrate right controller
                case ControllerHand.Right:
                    InputBridge.Instance.GetRightController().SendHapticImpulse(0, CalculateAmplitudeFromDistance(grabber), VibrateDuration);
                    return;
            }
            */

            if (grabber.HandSide == ControllerHand.Left)
                InputBridge.Instance.GetLeftController().SendHapticImpulse(0, CalculateAmplitudeFromDistance(grabber), VibrateDuration);

            if (grabber.HandSide == ControllerHand.Right)
                InputBridge.Instance.GetRightController().SendHapticImpulse(0, CalculateAmplitudeFromDistance(grabber), VibrateDuration);
        }

        // utils
        private float CalculateAmplitudeFromDistance(Grabber theGrabber)
        {
            float distance = (this.transform.position - theGrabber.transform.position).magnitude;

            float amplitude = Remap(distance, 0, 3);
            Mathf.Lerp(0, 1, amplitude);
            Mathf.Clamp(amplitude, 0, 1);

            amplitude = Mathf.Sqrt(amplitude * amplitude);

            // make sure the value of amplitude is not negative
            if (amplitude < 0)
                amplitude = amplitude * -1;

            if (debug) Debug.Log("The vibration amplitude is: " + amplitude);

            return amplitude;
        }

        private float Remap(float value, float min, float max)
        {
            float scaledValue = (value - min) / (min - max);
            scaledValue = value / max;
            return scaledValue;
        }
    }
}
