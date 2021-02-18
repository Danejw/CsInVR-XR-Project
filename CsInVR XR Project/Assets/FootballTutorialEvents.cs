using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CSInVR.Tutorial
{
    public class FootballTutorialEvents : MonoBehaviour
    {
        public bool debug;

        public delegate void OnTutorialStart();
        public static event OnTutorialStart onTutorialStart;

        public UnityEvent TutorialStart;
        public UnityEvent TutorialEnd;
        public UnityEvent TutorialOnHike;
        public UnityEvent TutorialOnMissedCatch;   
        public UnityEvent TutorialOnGrabBall;
        public UnityEvent TutorialOnThrownBall;
        public UnityEvent TutorialOnSpiral;
        public UnityEvent TutorialOnHitTarget;


        private void Start()
        {
            TutorialHikeBall.onHike += OnHike;
            TutorialHikeBall.onMissedCatch += OnMissedCatch;
            TutorialHikeBall.onGrabBall += OnGrabBall;
            TutorialHikeBall.onThrownBall += OnThrownBall;
            TutorialHikeBall.onSpiral += OnSpiral;
            TargetGoal.onTargetHit += OnHitTarget;
            SimpleTutorialManager.onTutorialEnd += OnEnd;

            OnStart();
        }

        private void OnDisable()
        {
            TutorialHikeBall.onHike -= OnHike;
            TutorialHikeBall.onMissedCatch -= OnMissedCatch;
            TutorialHikeBall.onGrabBall -= OnGrabBall;
            TutorialHikeBall.onThrownBall -= OnThrownBall;
            TutorialHikeBall.onSpiral -= OnSpiral;
            TargetGoal.onTargetHit -= OnHitTarget;
            SimpleTutorialManager.onTutorialEnd -= OnEnd;
        }

        public void OnStart()
        {
            TutorialStart?.Invoke();
            onTutorialStart?.Invoke();

            if (debug) Debug.Log("Tutorial TutorialStart Event");
        }

        private void OnEnd()
        {
            TutorialEnd?.Invoke();

            if (debug) Debug.Log("Tutorial TutorialEnd Event");
        }

        private void OnHike()
        {
            TutorialOnHike?.Invoke();

            if (debug) Debug.Log("Tutorial OnHike Event");
        }

        private void OnMissedCatch()
        {
            TutorialOnMissedCatch?.Invoke();

            if (debug) Debug.Log("Tutorial OnMissedCatch Event");
        }

        private void OnGrabBall()
        {
            TutorialOnGrabBall?.Invoke();

            if (debug) Debug.Log("Tutorial OnGrabBall Event");
        }

        private void OnThrownBall()
        {
            TutorialOnThrownBall?.Invoke();

            if (debug) Debug.Log("Tutorial OnThrownBall Event");
        }

        private void OnSpiral()
        {
            TutorialOnSpiral?.Invoke();

            if (debug) Debug.Log("Tutorial OnSpiral Event");
        }
    
        private void OnHitTarget()
        {
            TutorialOnHitTarget?.Invoke();

            if (debug) Debug.Log("Tutorial OnHitTarget Event");
        }
    }
}
