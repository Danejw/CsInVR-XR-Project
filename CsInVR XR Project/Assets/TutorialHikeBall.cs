using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CSInVR.Football;

namespace CSInVR.Tutorial
{
    public class TutorialHikeBall : HikeBall
    {
        // iscaught
        // isactive
        // isgrabbed
        public delegate void OnGrabBall();
        public static event OnGrabBall onGrabBall;

        public delegate void OnThrownBall();
        public static event OnThrownBall onThrownBall;

        public delegate void OnSpiral();
        public static event OnSpiral onSpiral;

        [SerializeField] private bool sentGrabMessage = false;
        [SerializeField] private bool sentThrownMessage = false;
        [SerializeField] private bool sentSpiralMessage = false;


        protected override void Start()
        {
            base.Start();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
        }

        protected override void Update()
        {
            base.Update();

            // send the on grab ball message once
            if (isGrabbed && !sentGrabMessage)
            {
                onGrabBall?.Invoke();
                sentGrabMessage = true;
            }           

            // send the on thrown ball message once
            if (isActive && !isGrabbed && !sentThrownMessage)
            {
                onThrownBall?.Invoke();
                sentThrownMessage = true;
            }

            // send the on spiral message once
            if (isActive && rig.velocity.magnitude > minSpiralVelocity && !sentSpiralMessage)
            {
                onSpiral?.Invoke();
                sentSpiralMessage = true;
            }
        }

        protected override void OnCollisionEnter(Collision collision)
        {
            base.OnCollisionEnter(collision);
        }


        
    }
}
