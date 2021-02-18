using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CSInVR.Tutorial
{
    // displays and hide the end game ui, when the game's start and end events are thrown
    public class TutorialEndUI : MonoBehaviour
    {
        private void Start()
        {
            gameObject.SetActive(false);

            SimpleTutorialManager.onTutorialEnd += DisplayEndUI;
            FootballTutorialEvents.onTutorialStart += HideEndUI;
        }

        private void OnDestroy()
        {
            SimpleTutorialManager.onTutorialEnd -= DisplayEndUI;
            FootballTutorialEvents.onTutorialStart -= HideEndUI;
        }

        private void DisplayEndUI()
        {
            gameObject.SetActive(true);
        }

        private void HideEndUI()
        {
            gameObject.SetActive(false);
        }
    }
}
