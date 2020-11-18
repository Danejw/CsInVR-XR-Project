using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;

namespace CSInVR
{
    public class playhapticonawake : MonoBehaviour
    {
        [SerializeField] private ControllerHand handside = ControllerHand.Left;


        [SerializeField] private float VibrateAmplitude;
        [SerializeField] private float VibrateDuration;

        private void OnEnable()
        {
            if (handside == ControllerHand.Left)
            {
                InputBridge.Instance.GetLeftController().SendHapticImpulse(0, VibrateAmplitude, VibrateDuration);
            }

            if (handside == ControllerHand.Right)
            {
                InputBridge.Instance.GetRightController().SendHapticImpulse(0, VibrateAmplitude, VibrateDuration);
            }
            else
                return;
        }
    }
}
