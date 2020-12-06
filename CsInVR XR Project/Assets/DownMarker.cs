using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CSInVR.Football
{
    // Changes the display on the marker to correspond to the current down in the football game script
    public class DownMarker : MonoBehaviour
    {
        public bool debug;

        [SerializeField] private FootballGame footballGame;

        [SerializeField] private GameObject[] markers;

        private void Start()
        {
            if (markers != null)
            {
                foreach (GameObject mark in markers)
                {
                    mark.SetActive(false);
                }
            }
            else
                if (debug) Debug.Log("Markers are Not assigned");
        }


        private void Update()
        {
            switch (footballGame.currentDown)
            {
                case 1:
                    foreach (GameObject mark in markers)
                    {
                        if (mark.name == "1")
                            mark.SetActive(true);
                        else
                            mark.SetActive(false);
                    }
                    break;
                case 2:
                    foreach (GameObject mark in markers)
                    {
                        if (mark.name == "2")
                            mark.SetActive(true);
                        else
                            mark.SetActive(false);
                    }
                    break;
                case 3:
                    foreach (GameObject mark in markers)
                    {
                        if (mark.name == "3")
                            mark.SetActive(true);
                        else
                            mark.SetActive(false);
                    }
                    break;
                case 4:
                    foreach (GameObject mark in markers)
                    {
                        if (mark.name == "4")
                            mark.SetActive(true);
                        else
                            mark.SetActive(false);
                    }
                    break;
            }
        }
    }
}
