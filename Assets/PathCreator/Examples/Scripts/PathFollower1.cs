using UnityEngine;

namespace PathCreation.Examples
{
    // Moves along a path at constant speed.
    // Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
    public class PathFollower1 : MonoBehaviour
    {
        public static PathFollower1 Instance;
        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        float distanceTravelled;

        private void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            if (pathCreator != null)
            {
                // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
                pathCreator.pathUpdated += OnPathChanged;
            }
        }

        public float minSpeed = 10f;
        public float maxSpeed = 30f;
        public bool checkplayer;
        public float currentSpeed;
        private float timer = 0f;
        private float changeSpeedInterval = 4f;
        public int speed;

        void FixedUpdate()
        {
            if (pathCreator != null)
            {
                // Update the timer
                timer += Time.deltaTime;

                // Check if it's time to change the speed
                if (timer >= changeSpeedInterval)
                {
                    // Assign a new random speed within the specified range
                    // currentSpeed = Random.Range(minSpeed, maxSpeed);

                    // Reset the timer
                    timer = 0f;
                }

                // Update the distance traveled based on the current speed
                distanceTravelled += currentSpeed * Time.deltaTime;


                transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);

            }

        }

        // If the path changes during the game, update the distance travelled so that the follower's position on the new path
        // is as close as possible to its position on the old path
        void OnPathChanged()
        {
            distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }
    }
}