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

        private void Start()
        {
            gameoverText.enabled = false;
            readyToStartText.enabled = false;
            firstdownText.enabled = false;
        }


        private void OnEnable()
        {
            FootballGame.onReadyToStart += DisplayReadyToStart;
            FootballGame.onGameOver += DisplayGameOver;
            Goal.onFirstDown += DisplayFirstDown;
        }

        private void OnDisable()
        {
            FootballGame.onReadyToStart -= DisplayReadyToStart;
            FootballGame.onGameOver += DisplayGameOver;
            Goal.onFirstDown -= DisplayFirstDown;
        }

        private void DisplayFirstDown()
        {
            StartCoroutine(Counter(firstdownText));
        }

        private void DisplayReadyToStart()
        {
            StartCoroutine(Counter(readyToStartText));
        }

        private void DisplayGameOver()
        {
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
