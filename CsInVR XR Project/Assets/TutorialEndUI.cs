using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;

namespace CSInVR.Tutorial
{
    // displays and hide the end game ui, when the game's start and end events are thrown
    public class TutorialEndUI : MonoBehaviour
    {
        private void Start()
        {
            gameObject.SetActive(false);

            SimpleTutorialManager.onTutorialEnd += DisplayUI;
            FootballTutorialEvents.onTutorialStart += HideUI;
        }

        private void OnDestroy()
        {
            SimpleTutorialManager.onTutorialEnd -= DisplayUI;
            FootballTutorialEvents.onTutorialStart -= HideUI;
        }

        public void DisplayUI()
        {
            gameObject.SetActive(true);
        }

        public void HideUI()
        {
            gameObject.SetActive(false);
        }

        public void ToggleUI()
        {
            if (this.gameObject.activeSelf == false)
                DisplayUI();
            else
                HideUI();
        }


    }
}
