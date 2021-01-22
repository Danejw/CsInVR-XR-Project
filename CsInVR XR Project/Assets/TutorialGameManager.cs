using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace CSInVR.Tutorial
{
    public class TutorialGameManager : MonoBehaviour
    {
        public bool debug;
        public bool test;

        // Targets array
        public GameObject[] simpleTargets;
        public GameObject[] complexTargets;

        // Distances array
        public int[] distances;

        // level increment
        [SerializeField] private int level = 0;

        // When to allow more complexity
        [SerializeField] private bool allowComplexTargets = false;
        private bool allowDistances = false;

        // Time
        public bool startGame;
        private int currentTime;
        private int minTime = 0;
        [SerializeField] private int maxTime = 60;
        [SerializeField] private int targetAddTimeAmount = 10;

        // Probability
        private float probabilityOfComplexTargets = 0f;
        private float probabilityOfDistance;

        // Target managing
        private List<GameObject> targetsInScene;
        [SerializeField] private int targetsHit = 0;


        // a number that iterates up when a target is created, used to save the last targets position, and to create the target elsewhere 
        private int lastCreationPosition;


        private void Start()
        {
            initGame();

            InstantiateInCircle(simpleTargets[0], 1, distances[0]);
        }

        private void OnEnable()
        {
            TargetGoal.onTargetHit += IncrementTargetsHit;
        }

        private void OnDisable()
        {
            TargetGoal.onTargetHit -= IncrementTargetsHit;
        }

        private void Update()
        {
            if (targetsHit == targetsInScene.Count)
            {
                // next level
                levelComplete();
            }






            // test functions
            if (test)
            {
                //test = false;

                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    InstantiateInCircle(simpleTargets[0], 5, distances[0]);
                }
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    InstantiateInCircle(simpleTargets[0], 5, distances[1]);
                }
                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    InstantiateInCircle(simpleTargets[0], 5, distances[2]);
                }

                if (Input.GetKeyDown(KeyCode.Q))
                {
                    levelComplete();
                }
            }
        }


        private void initGame()
        {
            // setting initial values
            if (distances.Length != 0) probabilityOfDistance = 1 / distances.Length;

            currentTime = maxTime;

            startGame = false;
            level = 0;

            // creating the list of targets
            if (targetsInScene == null) targetsInScene = new List<GameObject>();
            else targetsInScene.Clear();
        }

        private void levelComplete()
        {
            level += 1;
            targetsHit = 0;

            // destroy targets in scene
            DestroyTargets();

            // create next level
            CreateNextLevelOfTargets();

            // send next level event
        }

        private void IncrementTargetsHit()
        {
            targetsHit += 1;
        }

        
        private void CreateNextLevelOfTargets()
        {
            // figure out how many targets to create. It doubles the level number... for example: level 5 would create 10 simple targets
            int targetAmt;

            if (level != 0)
                targetAmt = level * 2;
            else
            {
                level += 1;
                targetAmt = level * 2;
            }

            if (!allowComplexTargets)
            {
                // create amount of type simple targets and random distance ring
                for (int i = 0; i <= targetAmt; i++)
                {
                    InstantiateInCircle(simpleTargets[Random.Range(0, simpleTargets.Length)], 1, distances[Random.Range(0, distances.Length)]);
                }
            }
            else
            {
                InstantiateWithComplexTargets(6);
            }
  
        }

        private void DestroyTargets()
        {
            if (targetsInScene != null)
            {
                foreach (GameObject target in targetsInScene)
                {
                    Destroy(target, 2);
                }

                targetsInScene.Clear();
            }
        }


        private void InstantiateWithComplexTargets(int minThreshold)
        {
            // create levels with complex targets when the amount of target reaches the minimum threshold
            if (level * 2 >= minThreshold)
            {
                int complexTargetsAmt = level * 2 % 4;

                if (complexTargetsAmt == 0)
                    complexTargetsAmt = level * 2 / 4;

                if (debug) Debug.Log("Complex Targets: " + complexTargetsAmt);
                if (debug) Debug.Log("Simple Targets: " + ((level * 2) - complexTargetsAmt));


                // create complex targets
                for (int i = 0; i < complexTargetsAmt; i++)
                {
                    InstantiateInCircle( complexTargets[ Random.Range( 0, complexTargets.Length ) ], 1, distances[Random.Range(0, distances.Length)] );
                }
                // create simple targets
                for ( int j = 0; j < (level * 2) - complexTargetsAmt; j++ )
                {
                    InstantiateInCircle(simpleTargets[Random.Range(0, simpleTargets.Length)], 1, distances[Random.Range(0, distances.Length)]);
                }
            }
            else
                InstantiateInCircle(simpleTargets[Random.Range(0, simpleTargets.Length)], 1, distances[Random.Range(0, distances.Length)]);
        }


        // Utility
        private void InstantiateInCircle(GameObject obj, int howMany, float radius)
        {          
            for (int i = 0; i < howMany; i++)
            {
                lastCreationPosition += 1;
                
                float angle = lastCreationPosition + i * Mathf.PI * 2f / radius;


                float height;
                if (radius < 6)
                    height = 2;
                else if (radius > 5 && radius < 11)
                    height = 3;
                else
                    height = 4;

                Vector3 newPos = new Vector3(Mathf.Cos(angle) * radius, height, Mathf.Sin(angle) * radius);

                GameObject target = Instantiate(obj, newPos, Quaternion.identity, transform);
                target.transform.LookAt(Vector3.zero + new Vector3(0, 2, 0));
                targetsInScene.Add(target);

                if (debug) Debug.Log("Instantiating " + obj.name + "s");
            }
        }

    }
}
