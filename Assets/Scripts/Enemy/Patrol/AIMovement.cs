using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum MovementType
{
    follow, waypoint, wander
}

public class AIMovement : MonoBehaviour
{
    public Animator anim; // Used to control the animator

    private Rigidbody rigid;

    public LayerMask blockingLayers;
    public float speed;

    private bool seeking;
    private GameObject target;
    public bool stop = false; // Variable for setting up if this enemy is dead

    private Vector3 lookPosition; // More facing the player variables
    public float turnSpeed = 100f; //How fast we turn to face the target

    public MovementType movementType;

    // Follow Variables
    public float followDistance;
    private float speedTotal; // Stores the speed upon initialization
    public GameObject followTarget;

    // Wander Around Variables
    public float  wanderMaxDistance;
    public GameObject wanderTarget;
    public int wanderDelay;
    private Vector3 startingPosition;

    // Waypoint Variables
    private bool waypointReached = false;
    List<GameObject> waypointList = new List<GameObject>();
    private int waypointCount = 0;
    public bool waypointRandomizer = false; // To be used so the starting waypoint will be randomly selected
    public bool waypointContinuous = false; // To be used to make waypoints continuously used
    public GameObject wpTest1;
    public GameObject wpTest2;
    public GameObject wpTest3;

    // Raycast Avoidance System
    private bool isThereAnyObstruction = false;
    private RaycastHit hit;
    private float rayDistanceSide = 3;
    private float rayDistanceBack = 2;
    private int range = 10;

    

    IEnumerator Start()
    {
        rigid = GetComponent<Rigidbody>();
        /* - Rigidbody Control Code
        rigid.constraints = RigidbodyConstraints.None;

        // Turn them on
        rigid.constraints = RigidbodyConstraints.FreezePositionY;
        rigid.constraints = RigidbodyConstraints.FreezeRotationX;
        rigid.constraints = RigidbodyConstraints.FreezeRotationZ;
        */

        // Sets up a starting position for wandering
        startingPosition = this.transform.position;

        // Set the speed total to the speed variable
        speedTotal = speed;
        anim = GetComponent<Animator>();

        // Adding each of the waypoints to the waypointList
        //foreach (GameObject waypointList in waypointList)
        //{
            waypointList.Add(wpTest1);
            waypointList.Add(wpTest2);
            waypointList.Add(wpTest3);
        //}

        if (movementType == MovementType.follow)
        {
            target = followTarget;
            StartCoroutine(FollowTarget());
        }
        else if (movementType == MovementType.wander)
        {
            target = wanderTarget;
            StartCoroutine(Wander());
        }
        else if (movementType == MovementType.waypoint)
        {
            if (waypointRandomizer == true) // This will setup a random waypoint locator
            {
                var randomValue = Random.Range(0, waypointList.Count); // Random Range returns one below the max value
                target = waypointList[randomValue];
            }
            else
                target = waypointList[0];

            StartCoroutine(Waypoint());
        }
            
        yield return null;
    }

    private void Update()
    {
        movementSystem(); // Makes the movement system call each frame

        // If we're on wander mode and on target stop temporarily
        if (Vector3.Distance(transform.position, target.transform.position) < 0.5f && movementType == MovementType.wander) 
        {
            speed = 0;
        }

    }

    IEnumerator Wander()
    {
        seeking = true;

        while (seeking)
        {
            if (stop) // If for whatever reason we need to stop randomly wandering
            {
                speed = 0;
                yield break;
            }
            else if (target != null)
            {
                if (Vector3.Distance(transform.position, target.transform.position) > 0.1f)
                {
                    speed = speedTotal;
                }

                Vector3 randomPoint = Random.insideUnitSphere * wanderMaxDistance;
                target.transform.position = new Vector3(randomPoint.x, this.transform.position.y, randomPoint.z);
                yield return new WaitForSeconds(wanderDelay);
            }

            yield return null;
        }

        yield return null;
    }

    IEnumerator Waypoint()
    {
        seeking = true;

        while (seeking)
        {
            Debug.Log(target);
            //Debug.Log(Random.Range(0, waypointList.Count + 1));
            if (stop)
            {
                //Debug.Log("Exit Waypoint Coroutine");
                yield break;
            }
            else if (target != null)
            {
                if (Vector3.Distance(transform.position, target.transform.position) < 0.1f) // Checks if we're on the waypoint
                {
                    // Establish the next waypoint
                    waypointCount++;
                    if (waypointCount <= waypointList.Count - 1)
                    {
                        if (waypointRandomizer == true)
                        {
                            var randomValue = Random.Range(0, waypointList.Count); // Random Range returns one below the max value
                            target = waypointList[randomValue];
                        }
                        else
                            target = waypointList[waypointCount];
                        //Debug.Log(target);
                    }
                    else
                    {
                        // If you've checked for continuous waypoints (for use with randomization)
                        if (waypointContinuous == true)
                        {
                            waypointCount = -1;
                        }
                        else
                        {
                            stop = true;
                            speed = 0;
                        }   
                    }
                        
                }

                yield return null;
            }
        }
        yield return null;
    }

    IEnumerator FollowTarget()
    {
        seeking = true;

        while (seeking)
        {
            if (stop)
            {
                yield return null;
            }
            else if (target != null)
            {
                if (Vector3.Distance(transform.position, target.transform.position) > followDistance)
                {
                    speed = speedTotal;
                    if (anim != null)
                    {
                        anim.SetBool("Walking", true); // Sets the animator to walk
                    }

                }
                else
                {
                    // Lower speed to zero so we don't move if we're following too closely
                    speed = 0;
                    if (anim != null)
                    {
                        anim.SetBool("Walking", false); // Sets it so it wont walk
                    }
                    yield return null;
                }

                yield return null;
            }
        }
        yield return null;
    }

    IEnumerator AttackPlayer() // Can be used by the AI to start an attack co-routine
    {

        yield return new WaitForSeconds(2f);
    }

    /*/ Not currently used but does an LOS check to a target (Useful for losing LOS and applying a stop = true) Also potentially useful for a future attack mode
    bool HasLOS()
    {

        // Target variable: Vector3 playerPos = PlayerManager.Instance.mainPlayer.transform.position;
        // Raycast to target: Ray ray = new Ray(transform.position, playerPos - transform.position);
        // Return true/false: return !Physics.Raycast(ray, Vector3.Distance(playerPos, transform.position), blockingLayers);
    }*/

    void movementSystem()
    {
        //Look Towards the Target if there is nothing in front.
        if (!isThereAnyObstruction)
        {
            Vector3 relativePos = target.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, turnSpeed * Time.deltaTime);
        }

        // Move the transform in the forward direction.
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        //Checking for any Obstacle in front.
        // Two rays left and right to the object to detect the obstacle.
        Transform leftRay = transform;
        Transform rightRay = transform;

        //Use Phyics.RayCast to detect the obstacle
        if (Physics.Raycast(leftRay.position + (transform.right * rayDistanceSide), transform.forward, out hit, range) || Physics.Raycast(rightRay.position - (transform.right * rayDistanceSide), transform.forward, out hit, range))
        {
            if (hit.collider.gameObject.CompareTag("Obstacle"))
            {
                isThereAnyObstruction = true;
                //Debug.Log("Obstruction == true");
                transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed);
            }
        }

        // Now Two More RayCast At The End of Object to detect that object has already pass the obsatacle.
        // Just making this boolean variable false it means there is nothing in front of object.
        if (Physics.Raycast(transform.position - (transform.forward * rayDistanceBack), transform.right, out hit, 10) || Physics.Raycast(transform.position - (transform.forward * rayDistanceBack), -transform.right, out hit, 10))
        {
            if (hit.collider.gameObject.CompareTag("Obstacle"))
            {
                //Debug.Log("Obstruction clear!");
                isThereAnyObstruction = false;
            }
        }

        // Use to debug the Physics.RayCast.
        Debug.DrawRay(transform.position + (transform.right * rayDistanceSide), transform.forward * 20, Color.red);
        Debug.DrawRay(transform.position - (transform.right * rayDistanceSide), transform.forward * 20, Color.red);
        Debug.DrawRay(transform.position - (transform.forward * rayDistanceBack), -transform.right * 20, Color.yellow);
        Debug.DrawRay(transform.position - (transform.forward * rayDistanceBack), transform.right * 20, Color.yellow);

    }
    

    void OnTriggerEnter(Collider col) // Checks for collision with a waypoint
    {
        if (col.gameObject.tag == "Waypoint")
        {
            waypointReached = true;
            //Debug.Log("Got to the waypoint collider!");

            if (waypointContinuous == false)
                col.gameObject.SetActive(false); // Disables that waypoint upon reaching it
        }
    }
}
