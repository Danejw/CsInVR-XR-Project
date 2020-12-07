using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CSInVR.Football
{
    [RequireComponent(typeof(Collider))]

    public class Reciever : MonoBehaviour
    {
        public bool debug;

        [SerializeField] private Transform caughtPosition;

        public delegate void OnCatch(GameObject reciever);
        public static event OnCatch onCatch;

        public bool hasCaught;

        public Vector3 startingPosition;

        public FootballGame footballGame;
        public Transform divisionPoint;

        // [HideInInspector] public Animator anim;

        // parameters
        [SerializeField] private int numberOfDivisions = 4;
        [SerializeField] private float extraDistanceAmount = 10;
        [SerializeField] private float recieverSpeed = 1;
        private List<Transform> divisionPoints;
        [SerializeField] private float randomness = 4;
        [SerializeField] private float waypointRadius = 0.5f;
        private int count;
        [SerializeField] private bool isRunning = false;

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
            hasCaught = false;
            isRunning = false;

            startingPosition = this.transform.position;
        }

        private void OnDisable()
        {
            HikeBall.onHike -= Run;
            Reciever.onCatch -= CatchEvent;
        }

        private void Update()
        {
            if (regenerateRoute)
            {
                RegenerateRoute();
                regenerateRoute = false;
            }

            if (isRunning)
                MoveAlongRoute();
        }


        public void Run()
        {
            isRunning = true;

            if (debug) Debug.Log("The Reciever started to run");        
        }

        private void CatchBall(GameObject reciever)
        {
            onCatch?.Invoke(reciever);
            hasCaught = true;
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
                    if (!hasCaught)
                    {
                        if (caughtPosition) MoveBallToCaughtPosition(ball, caughtPosition);
                        CatchBall(this.gameObject);
                    }
                }
            }
            else
                hasCaught = false;
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
            float divisionIterator = (footballGame.GetDistanceTillFirstdown() + extraDistanceAmount) / numberOfDivisions;

            for (int i = 0; i < numberOfDivisions; i++)
            {
                // create division points
                Transform point = Instantiate(divisionPoint, new Vector3(startingPosition.x, 0.1f, startingPosition.z), Quaternion.identity);
                // put division points into a list
                divisionPoints.Add(point);
                // place the division points onto the field
                point.position += new Vector3( Random.Range(-randomness, randomness) , 0 , divisionIterator);
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
                    isRunning = false;
            }
            else
                transform.position = Vector3.MoveTowards(transform.position, divisionPoints[count].position, Time.deltaTime * recieverSpeed);
           
            if (debug) Debug.Log("Moving along " + this.name + "'s route");
        }

        private void DestroyRoute()
        {
            isRunning = false;
            count = 0;

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

        // events
        private void CatchEvent(GameObject reciever)
        {
            isRunning = false;
        }
    }
}

