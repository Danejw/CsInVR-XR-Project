using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CSInVR.Tutorial
{
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(Collider))]

    public class TargetGoal : MonoBehaviour
    {
        public bool debug;

        // Play Goal effects if trigger is hit
        AudioSource sound;
        public ParticleSystem particle;


        // materials & mesh used to change the visual state of the target
        [SerializeField] private Material activeMat;
        [SerializeField] private Material nonActiveMat;
        [SerializeField] private Renderer hoopMesh;


        // event to send if target is hit
        public delegate void OnTargetHit();
        public static event OnTargetHit onTargetHit;

        [SerializeField] private bool targethit = false;


        private void Awake()
        {
            sound = GetComponent<AudioSource>();

            if (!particle) particle = GetComponent<ParticleSystem>();

            if (hoopMesh) hoopMesh.GetComponent<Renderer>();
        }

        private void OnEnable()
        {
            targethit = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Ball")
            {
                if (!targethit)
                {
                    // Play particle
                    if (particle && !particle.isPlaying)
                        particle.Play();

                    // Play Sound
                    if (!sound.isPlaying)
                        sound.Play();

                    // Change materials
                    hoopMesh.material = nonActiveMat;

                    // send on target hit event once
                    onTargetHit.Invoke();
                    targethit = true;

                    if (debug) Debug.Log("Target Hit!");
                }
            }
        }

        private void Update()
        {
            // checks to see if the target has been hit, sets the material
            if (!targethit && hoopMesh.material != activeMat)
                hoopMesh.material = activeMat;
        }
    }
}
