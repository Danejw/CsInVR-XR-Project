using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace CSInVR.Football.UI
{
    public class FootballGameUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text gameoverText;
        [SerializeField] private TMP_Text readyToStartText;
        [SerializeField] private TMP_Text firstdownText;
        [SerializeField] private TMP_Text touchdownText;


        private void Start()
        {
            gameoverText.enabled = false;
            readyToStartText.enabled = false;
            firstdownText.enabled = false;
            touchdownText.enabled = false;
        }


        private void OnEnable()
        {
            FootballGame.onReadyToStart += DisplayReadyToStart;
            FootballGame.onGameOver += DisplayGameOver;
            FootballGame.onFirstdown += DisplayFirstDown;
            Goal.onGoal += DisplayTouchdown;
        }

        private void OnDisable()
        {
            FootballGame.onReadyToStart -= DisplayReadyToStart;
            FootballGame.onGameOver -= DisplayGameOver;
            FootballGame.onFirstdown -= DisplayFirstDown;
            Goal.onGoal -= DisplayTouchdown;
        }

        private void DisplayTouchdown()
        {
            if (!touchdownText.enabled)
                if (!readyToStartText.enabled && !gameoverText.enabled && !firstdownText.enabled)
                    StartCoroutine(Counter(touchdownText));
        }

        private void DisplayFirstDown()
        {
            if (!firstdownText.enabled)
                if (!readyToStartText.enabled && !gameoverText.enabled && !touchdownText.enabled)
                    StartCoroutine(Counter(firstdownText));
        }

        private void DisplayReadyToStart()
        {
            if (!readyToStartText.enabled)
                if (!gameoverText.enabled && !firstdownText.enabled && !touchdownText.enabled)
                    StartCoroutine(Counter(readyToStartText));
        }

        private void DisplayGameOver()
        {
            if (!gameoverText.enabled)
                if (!readyToStartText.enabled && !firstdownText.enabled)
                    StartCoroutine(Counter(gameoverText));
        }

        IEnumerator Counter(TMP_Text text)
        {         
            text.enabled = true;
            yield return new WaitForSeconds(5);
            text.enabled = false;
        }
    }
}
