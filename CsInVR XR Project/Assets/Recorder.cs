using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Evereal.VideoCapture;

namespace CSInVR
{
    [RequireComponent(typeof(VideoCapture))]

    public class Recorder : MonoBehaviour
    {

        VideoCapture vRecorder;
        AudioCapture[] audioRecorders;
        public bool debug;
        public bool record;
        private bool isRecording;

        private void Start()
        {
            vRecorder = GetComponent<VideoCapture>();
            audioRecorders = GetComponents<AudioCapture>();
        }

        private void Update()
        {
            if (record & !isRecording)
            {
                StartRecording();
                isRecording = true;
            }


            if (!record & isRecording)
            {
                StopRecording();
                isRecording = false;
            }
        }

        public void StartRecording()
        {
            vRecorder.StartCapture();

            if (audioRecorders != null)
                foreach (AudioCapture audio in audioRecorders)
                    audio.StartCapture();

            if (debug) Debug.Log("Starting Recording");
        }

        public void StopRecording()
        {
            vRecorder.StopCapture();

            if (audioRecorders != null)
                foreach (AudioCapture audio in audioRecorders)
                    audio.StopCapture();

            if (debug) Debug.Log("Stoping Recording");

        }
    }
}
