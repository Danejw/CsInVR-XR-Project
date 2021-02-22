using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CSInVR.Football
{
    public class QuarterbackUI : MonoBehaviour
    {
        FootballGame football;


        private void Start()
        {
            // subscribe to events
            FootballGame.onGameOver += DisplayUI;
            Goal.onGoal += DisplayUI;

            HideUI();
        }

        private void OnEnable()
        {
            // subscribe to events
            FootballGame.onGameOver += DisplayUI;
            Goal.onGoal += DisplayUI;
        }

        private void OnDisable()
        {
            FootballGame.onGameOver -= DisplayUI;
            Goal.onGoal -= DisplayUI;
        }


        public void DisplayUI()
        {
            if(!gameObject.activeSelf) gameObject.SetActive(true);
        }

        public void HideUI()
        {
            if (gameObject.activeSelf) gameObject.SetActive(false);
        }


        // play sound
        // play tactile on controller
    }
}
