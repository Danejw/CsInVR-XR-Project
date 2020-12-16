using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CSInVR.Football
{
    [RequireComponent(typeof(Collider))]

    public class Blocker : MonoBehaviour
    {
        public bool debug;

        [SerializeField] private Transform caughtPosition;

        public delegate void OnBlock(GameObject blocker);
        public static event OnBlock onBlock;

        public bool hasBlocked;

        public Vector3 startingPosition;

        public FootballGame footballGame;
        public Transform divisionBlockPoint;

        // [HideInInspector] public Animator anim;

        // parameters
        [SerializeField] private int numberOfDivisions = 4;
        [SerializeField] private float extraDistanceAmount = 10;
        [SerializeField] private float blockerSpeed = 1;
        private List<Transform> divisionPoints;
        [SerializeField] private float randomness = 4;
        [SerializeField] private float waypointRadius = 0.5f;
        private int count;
        [SerializeField] private bool isRunning = false;
        [SerializeField] private bool isAttacking = false;

        private Reciever reciever;
        [SerializeField] private Vector3 distanceBetweenReciever = new Vector3(0, 0, -1);

        public Transform ball;
        [SerializeField] private Vector3 distanceBetweenBall = new Vector3(0, 0, -0.2f);

        public Transform player;
        [SerializeField] private Vector3 distanceBetweenPlayer = new Vector3(0, 0, -0.2f);

        public bool regenerateRoute;


        private void Start()
        {
            divisionPoints = new List<Transform>();
            CreateRoute();
            isRunning = false;
        }

        private void OnEnable()
        {
            HikeBall.onHike += Run;
            Reciever.onCatch += CatchEvent;
            Blocker.onBlock += BlockEvent;
            HikeBall.onMissedCatch += MissedCatchEvent;

            hasBlocked = false;
            isRunning = false;

            startingPosition = this.transform.position;
        }

        private void OnDisable()
        {
            HikeBall.onHike -= Run;
            Reciever.onCatch -= CatchEvent;
            Blocker.onBlock -= BlockEvent;
            HikeBall.onMissedCatch -= MissedCatchEvent;
        }

        private void Update()
        {
            if (regenerateRoute)
            {
                RegenerateRoute();
                regenerateRoute = false;
            }

            if (isRunning)
            {
                // flip if, else statements around to priority defending over attacking and vice versa
                if (ball && isAttacking) Follow(ball.transform, distanceBetweenBall);
                else if (reciever) Follow(reciever.transform, distanceBetweenReciever);
                else MoveAlongRoute();
            }
        }


        public void Run()
        {
            isRunning = true;

            if (debug) Debug.Log("The Reciever started to run");        
        }

        private void BlockBall(GameObject blocker)
        {
            onBlock?.Invoke(blocker);
            hasBlocked = true;
            isRunning = false;

            if (debug) Debug.Log("The ball has been caught");
        }

        private void MoveBallToCaughtPosition(Ball ballObject, Transform moveTo)
        {
            Rigidbody ballRig = ballObject.GetComponent<Rigidbody>();

            // move the ball into the caught position
            ballObject.transform.position = moveTo.position;

            // Set the ball rigidbody state, to keep the ball with the reciever
            ballRig.isKinematic = true;
            ballRig.velocity = new Vector3(0,0,0);
            ballObject.transform.SetParent(this.transform);

            if (debug) Debug.Log("The ball has been moved");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Ball")
            {
                Ball ball = other.gameObject.GetComponent<Ball>();

                if (ball && ball.GetBallIsActive())
                {
                    if (!hasBlocked)
                    {
                        if (caughtPosition) MoveBallToCaughtPosition(ball, caughtPosition);
                        BlockBall(this.gameObject);
                    }
                }
            }
            else
                hasBlocked = false;

            if (other.gameObject.tag == "Reciever")
            {
                reciever = other.GetComponent<Reciever>();
            }

            if (other.gameObject.tag == "Pocket" && isRunning)
            {
                isAttacking = true;
            }
        }

        private void ResetPosition()
        {
            transform.position = startingPosition;
            isRunning = false;
            count = 0;

            if (debug) Debug.Log("Resetting " + this.name + "'s position");

        }

        // Reviever's Route
        private void CreateRoute()
        {
            isAttacking = false;

            float divisionIterator = (footballGame.GetDistanceTillFirstdown() - extraDistanceAmount) / numberOfDivisions;

            for (int i = 0; i < numberOfDivisions; i++)
            {
                // create division points
                Transform point = Instantiate(divisionBlockPoint, new Vector3(startingPosition.x, 0.1f, startingPosition.z), Quaternion.identity);
                // put division points into a list
                divisionPoints.Add(point);
                // place the division points onto the field
                point.position -= new Vector3( Random.Range(-randomness, randomness) , 0 , divisionIterator);
                divisionIterator += divisionIterator;
            }

            if (debug) Debug.Log("Creating " + this.name + "'s route");
        }

        private void MoveAlongRoute()
        {
            if (Vector3.Distance(divisionPoints[count].position, transform.position) < waypointRadius)
            {
                count++;

                if (count >= divisionPoints.Count)
                    Follow(ball, distanceBetweenBall);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, divisionPoints[count].position, Time.deltaTime * blockerSpeed);
                transform.LookAt(divisionPoints[count].position);
            }


            if (debug) Debug.Log("Moving along " + this.name + "'s route");
        }

        private void DestroyRoute()
        {
            isRunning = false;
            count = 0;

            reciever = null;

            if (divisionPoints != null)
            {
                foreach (Transform point in divisionPoints)
                    Destroy(point.gameObject);

                divisionPoints.Clear();
            }

            if (debug) Debug.Log("Destroying " + this.name + "'s route");            
        }

        public void RegenerateRoute()
        {
            DestroyRoute();
            ResetPosition();
            CreateRoute();

            if (debug) Debug.Log("Regenerating " + this.name + "'s route");
        }

        private void Follow(Transform position, Vector3 distance)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(position.position.x, 0, position.position.z) + distance, Time.deltaTime * blockerSpeed);

            if (reciever) transform.LookAt(reciever.transform);
            else if (player) transform.LookAt(player.transform);

            if (debug) Debug.Log("Following " + position.name);
        }

        // events
        private void CatchEvent(GameObject reciever)
        {
            isRunning = false;
        }

        private void BlockEvent(GameObject blocker)
        {
            isRunning = false;
        }

        private void MissedCatchEvent()
        {
            isRunning = false;
        }
    }
}

