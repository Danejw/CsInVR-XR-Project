using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSInVR
{
    public class BallSpawner : MonoBehaviour
    {
        public bool reSpawnBall;
        public Transform spawnPoint;


        private void Update()
        {
            if (reSpawnBall)
                ResetBalls();
        }


        public void ResetBalls()
        {
            if (spawnPoint)
            {
                GetComponent<Transform>().SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);
                GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                reSpawnBall = false;
            }
        }
    }
}
