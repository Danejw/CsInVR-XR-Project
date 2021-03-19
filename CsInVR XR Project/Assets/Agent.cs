using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;


namespace CSInVR.Football
{
    [RequireComponent(typeof(BehaviorTree))]

    public class Agent : MonoBehaviour
    {
        public bool debug;

        public delegate void OnAgentEnabled(Transform agent);
        public static event OnAgentEnabled onAgentEnabled;

        public delegate void OnAgentDisabled(Transform agent);
        public static event OnAgentDisabled onAgentDisabled;

        private BehaviorTree btree;
        private Vector3 startingPosition;
        private Quaternion startingRotation;

        private void Start()
        {
            btree = GetComponent<BehaviorTree>();
            btree.enabled = false;

            startingPosition = transform.position;
            startingRotation = transform.rotation;
        }

        private void OnEnable()
        {
            HikeBall.onHike += EnableBehaviorTree;
            Agent_CenterAttacker.onBlock += DisableBehaviorTree;
            Reciever.onCatch += DisableBehaviorTree;
            FootballGame.onGameOver += DisableBehaviorTree;
            FootballGame.onGameStart += DisableBehaviorTree;
            HikeBall.onMissedCatch += DisableBehaviorTree;
            FootballGame.onReadyToStart += DisableBehaviorTree;
            FootballGame.onReadyToStart += ResetPosition;
            FootballGame.onGameStart += ResetPosition;

            onAgentEnabled?.Invoke(this.transform);
        }

        private void OnDisable()
        {
            onAgentDisabled?.Invoke(transform);

            HikeBall.onHike -= EnableBehaviorTree;
            Agent_CenterAttacker.onBlock -= DisableBehaviorTree;
            Reciever.onCatch -= DisableBehaviorTree;
            FootballGame.onGameOver -= DisableBehaviorTree;
            FootballGame.onGameStart -= DisableBehaviorTree;
            HikeBall.onMissedCatch -= DisableBehaviorTree;
            FootballGame.onReadyToStart -= DisableBehaviorTree;
            FootballGame.onReadyToStart -= ResetPosition;
            FootballGame.onGameStart -= ResetPosition;
        }

        private void EnableBehaviorTree()
        {
            btree.enabled = true;

            if (debug) Debug.Log("Enabling Behavior Tree");
        }

        private void DisableBehaviorTree(GameObject caughtObject)
        {
            btree.enabled = false;

            if (debug) Debug.Log("Disabling Behavior Tree");   
        }

        private void DisableBehaviorTree()
        {
            btree.enabled = false;

            if (debug) Debug.Log("Disabling Behavior Tree");
        }

        public void ResetPosition()
        {
            transform.position = startingPosition;
            transform.rotation = startingRotation;

            if (debug) Debug.Log("Resetting " + this.name + "'s position");
        }
    }
}
