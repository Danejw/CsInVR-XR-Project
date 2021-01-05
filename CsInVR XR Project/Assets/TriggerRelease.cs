using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;

namespace CSInVR
{
    [RequireComponent(typeof(Grabber))]

    public class TriggerRelease : MonoBehaviour
    {
        public float gripRelease = .5f;

        protected Grabber grabber;
        ControllerHand hand;
        

        private void Start()
        {
            grabber = GetComponent<Grabber>();
            hand = grabber.HandSide;
        }

        public virtual void Update()
        {
            if (grabber.HoldingItem)
            {
                if (hand == ControllerHand.Right)
                {
                    if (grabber.HoldingItem && InputBridge.Instance.RightTrigger < gripRelease)
                    {
                        // release ball from grabber
                        grabber.TryRelease();
                    }
                }
                else if (hand == ControllerHand.Left)
                {
                    if (grabber.HoldingItem && InputBridge.Instance.LeftTrigger < gripRelease)
                    {
                        // release ball from grabber
                        grabber.TryRelease();
                    }
                }
            }
        }
    }
}
