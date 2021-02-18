using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using CSInVR.Football;

namespace CSInVR.BehaviorDesigner
{
    [RequireComponent(typeof(BehaviorTree))]

    public class WhenHikeEvent : Action
    {
        public bool debug;

        private bool isPlaying = false;


        public override void OnAwake()
        {
            base.OnAwake();
        }

        public override void OnStart()
        {
            base.OnStart();

            HikeBall.onHike += Play;         
        }

        public override void OnEnd()
        {
            base.OnEnd();

            HikeBall.onHike -= Play;

        }


        public override TaskStatus OnUpdate()
        {
            base.OnUpdate();

            if (isPlaying)
            {
                isPlaying = false;
                return TaskStatus.Success;
            }      
            else
            {
                return TaskStatus.Failure;
            }
        }

        private void Play()
        {
            if (debug) Debug.Log("Playing Behavior trees");

            isPlaying = true;
        }

    }
}
