using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using Obi;

//This is a good tutorial for topdown shooters https://www.youtube.com/watch?v=F5a4Xo6ijLE
// This is how to pass variables between scripts in a ton of ways http://answers.unity3d.com/questions/246211/having-scripts-interact.html

// This dude has some kickass tutorials when you don't know what to do watch some https://www.youtube.com/channel/UCq9_1E5HE4c_xmhzD3r7VMw/videos

// Another good tutorial series https://www.youtube.com/channel/UCp_SOgsRYdLfIEWLjM62ZJg/videos (Girl who shows networking tutorial)

//[RequireComponent(typeof(CharacterController))]

/* Joystick inputs
D-pad up: joystick button (None) / 5
D-pad down: joystick button (None) / 6
D-pad left: joystick button (None) / 7
D-pad right: joystick button (None) / 8
start: joystick button 7 / 9 Mac
back: joystick button 6 / 10 Mac
left stick(click): joystick button 8 / 11
right stick(click): joystick button 9 / 12
left bumper: joystick button 4 / 13
right bumper: joystick button 5 / 14
center("x") button: joystick button 15
A: joystick button 0 / 16 on mac
B: joystick button 1 / 17
X: joystick button 2 / 18
Y: joystick button 3 / 19
*/

public class playerMovement : MonoBehaviour
{

    public Animator anim; // Used to control the animator

    Rigidbody rigidBody;
    // Attack Data
    private Vector3 currentLocation;
    public GameObject attackHitBox; // Spawns the hitbox well use for colliding with an enemy
    public Vector3 worldRotation;

    //Attack Information
    [SerializeField]
    private bool attacking = false;
    private bool attackSlide;
    private int attackDamage = 40;
    private bool crAttacking = false; // Co-Routine Attacking
    public float attackStaminaCost;

    // Movement Data
    public float xSpeed = 3.0f;
    public float zSpeed = 5.0f;
    public float turnSpeed = 100f;
    public float moveSpeed = 10f;
    public Vector3 lastDirection { get; private set; }
    private float moveUp;
    private float moveDown;
    private float moveLeft;
    private float moveRight;
    private GameObject faceDirection;
    private playerMovement movement;

    private bool root; // Roots the player in place

    public Transform lookTarget; // This is to set a look target
    private Vector3 lookPosition;
    private bool leftTrigger; // This is the variable well use to lock your facing direction so you can move and attack in the same direction

    //Dodge Information
    [SerializeField]
    public bool dodging = false;
    private float dodgeCooldown = 0f; //Adds a cooldown for being able to dodge (Starts at 0 so you can dodge as soon as the game loads)
    private float dodgeCooldownTotal = 2f; // A normal cooldown for dodging
    private Vector3 dodgeForward; // Stores the direction we are facing before we dodge
    private float dodgeDistance = 10f; // Since we normalize our dodge direction we need an extra force so this number dictates how far we go
    public float dodgeStaminaCost;
    private bool dodgeAnimationTrigger = false;
    private float dodgeDuration; // Hard coding this cause C# sucks at grabbing animation length

    // Sounds
    public AudioClip dodgeSound1;
    public AudioClip dodgeSound2;
    public AudioClip attackSound1;
    public AudioClip attackSound2;
    public AudioClip attackSound3;

    // Scarf
    public GameObject bigScarf;
    public GameObject littleScarf;
    private Vector3 scarfForward; // stores information for which way the scarf should blow when you stop moving

    public void Init(playerMovement movement)
    {
        this.movement = movement;
    }

    void Awake()
    {
        faceDirection = new GameObject("facingDirection");
        faceDirection.AddComponent<faceDirection>().Init(this);
    }

    void OnDestroy()
    {
        if (faceDirection != null)
            Destroy(faceDirection);
    }

    // Use this for initialization
    void Start()
    {
        // Sets up our Rigidbody
        if (GetComponent<Rigidbody>())
        {
            rigidBody = GetComponent<Rigidbody>();
        }
        else { Debug.LogError("The character needs a rigidbody"); }

        attackStaminaCost = 15f;
        dodgeStaminaCost = 25f;
    }

    void Update()
    {

        if (Input.GetAxisRaw("Triggers") > 0 || Input.GetMouseButton(1) ) // LEFT TRIGGER Lock your aim position and move you slower
        {
            leftTrigger = true;
        }
        else { leftTrigger = false; }


        if (Input.GetButtonDown("LBumper"))
        {
            //ATM DOES NOTHING
        }

        // This code will make the character face the direction they're moving
        lookTarget = GameObject.Find("facingDirection").transform;
        Vector3 towards = lookTarget.position - transform.position;
        towards.Normalize();
        
        Quaternion target = Quaternion.LookRotation(towards, Vector3.up);

        if (!attacking && !leftTrigger && !dodging)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, target, turnSpeed * Time.deltaTime);


        /////////////////////// ATTACK ///////////////////////
        if (Input.GetButtonDown("Attack") && !attacking && !dodging) // E / A Xbox Controller / Mouse 1
        {

            if (GetComponent<playerHP>().currentStamina < attackStaminaCost) // If we don't have the stamina for it don't attack
            {
                attacking = false;
            }
            else
            {
                // SFX Info
                AudioManager.Instance.RandomSFX(attackSound1, attackSound2, attackSound3); // Randomly chooses an attack sound when we attack

                // Attack code removed here
                /*/Quaternion playerRotation = this.transform.rotation; // gives a variable to store the current rotation which we currently don't use
                Vector3 playerPosition = this.transform.position;
                Vector3 playerDirection = this.transform.forward;

                // How far away to spawn the hitbox
                float spawnDistance = 2;
                Vector3 spawnPos = playerPosition + playerDirection * spawnDistance;

                GameObject playerHitbox = (GameObject)Instantiate(attackHitBox, spawnPos, transform.rotation); // Name of object spawned, position it spawns, Quaternion.Identity takes rotate value from this object
                playerHitbox.GetComponent<hitbox>().damageValue = attackDamage; // Sends this var to the latest hitbox
                playerHitbox.GetComponent<hitbox>().player = this.gameObject; // Sets a variable to track the player in the hitbox
                playerHitbox.GetComponent<hitbox>().playerDirection = playerDirection; // Passes the direction variable to the hitbox
                playerHitbox.GetComponent<hitbox>().distance = spawnDistance; // Tells the box how far it should stay and lets us edit it in this script
                */

                attacking = true;
                GetComponent<playerHP>().staminaCost = 15; // Send over a stamina cost to the player HP script to track our stamina
            }

        }
            

        /////////////////////// Dodge roll ///////////////////////
        if (Input.GetButtonDown("Dodge") && !dodging && dodgeCooldown <= 0 && GetComponent<playerHP>().currentStamina > dodgeStaminaCost)
        {
            dodging = true;
            dodgeDuration = 0.75f;
            dodgeAnimationTrigger = true;
            dodgeCooldown = dodgeCooldownTotal;

            GetComponent<playerHP>().staminaCost = 25; // Send over a stamina cost to the player HP script to track our stamina
        }
        

        if (attacking && !crAttacking) // Countdown to turn off our attack state
        {
            float attackDuration = 1.5f;

            StartCoroutine(Attacking(attackDuration));
            crAttacking = true;
            attackSlide = true;
        }

        if (dodgeCooldown > 0)
            dodgeCooldown -= Time.deltaTime;
        
    }


    // FIXED UPDATE CALLED ONCE PER FRAME
    void FixedUpdate()
    {

        if (Input.GetAxis("Triggers") < 0) //RIGHT TRIGGER Roots the character in place so they can turn but not move
        {
            root = true;
            //Debug.Log("Triggers");
        }
        else { root = false; }

        Vector3 inputVector = new Vector3();
        inputVector.x = Input.GetAxis("Vertical") * -1f * xSpeed;
        inputVector.z = Input.GetAxis("Horizontal") * zSpeed;
        inputVector = Quaternion.Euler(worldRotation) * inputVector; // Rotates the inputs to the proper directions


        if (inputVector != Vector3.zero)
        {
            anim.SetBool("Running", true); // Sets the animator to walk
            lastDirection = inputVector.normalized; // Makes you face the proper direction when you move/stop
        }
        else
            anim.SetBool("Running", false); // Sets the animator to idle


        if (!dodging) // Stores our current forward direction and stops updating it once the dodge starts
        {
            dodgeForward = inputVector.normalized;
        }
        else if (dodging) // While we dodge we spin the character and slow down the movespeed
        {
            if (dodgeAnimationTrigger == true)
            {
                anim.SetTrigger("RollForward");
                dodgeAnimationTrigger = false;
            }
            GetComponent<playerHP>().forwardDodge = true; // Lets the player HP script know to pause stamina regen slightly longer
            rigidBody.velocity += (dodgeForward * dodgeDistance) * Time.deltaTime * (moveSpeed * 2f); // This is the dodge forward code. The number after movespeed is how many times faster you go than usual
            Quaternion target = Quaternion.LookRotation(dodgeForward, Vector3.up);
            if(dodgeDuration > 0)
            {
                dodgeDuration -= Time.deltaTime;
            }
            else
                dodging = false;
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, target, turnSpeed * Time.deltaTime); // Adds rotation to the dodge so you always face the way you roll
        }      

        if (!root && !attacking && !dodging)
        {
            rigidBody.velocity += inputVector * Time.deltaTime * moveSpeed;
        }
            

        if (attacking && attackSlide)// Gives you a slight slide forward on attack
        {
            rigidBody.velocity += this.transform.forward * 0.3f; // Slides you forward while you attack
            anim.SetBool("Attacking", true);
        }

        // Code to make the scarf wave around
        if (inputVector != Vector3.zero)
        {
            var inputCapperX = Mathf.Clamp(inputVector.x, -4, 4);
            var inputCapperZ = Mathf.Clamp(inputVector.z, -4, 4);
            var randomX = -UnityEngine.Random.Range(0,inputCapperX);
            var randomZ = -UnityEngine.Random.Range(0, inputCapperZ);
            bigScarf.GetComponent<ObiAerodynamicConstraints>().windVector = new Vector3(randomX, 3f, randomZ);
            bigScarf.GetComponent<ObiAerodynamicConstraints>().PushDataToSolver(new ObiSolverData(ObiSolverData.AerodynamicConstraintsData.WIND));
            littleScarf.GetComponent<ObiAerodynamicConstraints>().windVector = new Vector3(randomX, 4f, randomZ);
            littleScarf.GetComponent<ObiAerodynamicConstraints>().PushDataToSolver(new ObiSolverData(ObiSolverData.AerodynamicConstraintsData.WIND));
            scarfForward = inputVector.normalized;
        }
        else // If you're not moving the scarf blows behind you
        {
            float scarfX;
            float scarfZ;

            if (scarfForward.x > 0)
                scarfX = UnityEngine.Random.Range(0, scarfForward.x);
            else
                scarfX = UnityEngine.Random.Range(scarfForward.x, 2);

            if (scarfForward.z > 0)
                scarfZ = UnityEngine.Random.Range(0, scarfForward.z);
            else
                scarfZ = UnityEngine.Random.Range(scarfForward.z, 2);

            bigScarf.GetComponent<ObiAerodynamicConstraints>().windVector = new Vector3(scarfX, 0f, scarfZ);
            bigScarf.GetComponent<ObiAerodynamicConstraints>().PushDataToSolver(new ObiSolverData(ObiSolverData.AerodynamicConstraintsData.WIND));
            littleScarf.GetComponent<ObiAerodynamicConstraints>().windVector = new Vector3(scarfX, 0f, scarfZ);
            littleScarf.GetComponent<ObiAerodynamicConstraints>().PushDataToSolver(new ObiSolverData(ObiSolverData.AerodynamicConstraintsData.WIND));
        } // SCARF FINISHED

    }

    IEnumerator Attacking(float duration)
    {
        float timer = duration;  // Takes the duration when the CR is called
        bool slideEndOnce = false;

        while (timer > 0) // As long as the duration is still going keep repeating the code
        {
            if ((timer < duration * .2) && !slideEndOnce) // Checks if less than 20% of the duration is left
            {
                slideEndOnce = true;
                attackSlide = false;
            }
                
            timer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();   
        }
        // Tells the rest of the code the attack is done
        attacking = false;
        crAttacking = false;
        anim.SetBool("Attacking", false);
    }

    public void HitboxSpawn()
    {
        Vector3 playerPosition = this.transform.position;
        Vector3 playerDirection = this.transform.forward;
        // How far away to spawn the hitbox
        float spawnDistance = 2;
        Vector3 spawnPos = playerPosition + playerDirection * spawnDistance;

        GameObject playerHitbox = (GameObject)Instantiate(attackHitBox, spawnPos, transform.rotation); // Name of object spawned, position it spawns, Quaternion.Identity takes rotate value from this object
        playerHitbox.GetComponent<hitbox>().damageValue = attackDamage; // Sends this var to the latest hitbox
        playerHitbox.GetComponent<hitbox>().player = this.gameObject; // Sets a variable to track the player in the hitbox
        playerHitbox.GetComponent<hitbox>().playerDirection = playerDirection; // Passes the direction variable to the hitbox
        playerHitbox.GetComponent<hitbox>().distance = spawnDistance; // Tells the box how far it should stay and lets us edit it in this script
    }

    // Collision check
    void OnTriggerEnter(Collider col)
    {
        //Boss Room Portal Teleport
        if (col.gameObject.tag == "BossPortal")
        {
            // Teleport to the boss room!
            transform.position = new Vector3(0, 0, 0);
            GameManager.Instance.screenWhite = true;
        }
    }
}
