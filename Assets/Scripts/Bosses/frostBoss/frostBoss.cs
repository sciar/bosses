﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frostBoss : MonoBehaviour {

    // Movement
    [Header("Mobility Variables")]
    public float moveSpeed;
    public float turnSpeed;
    public Transform player;
    public float minDistance;
    public float maxDistance;

    // Decision Tree
    private float attackTimer = 4f;
    private float attackTimerMax;
    private int attackSelection;
    private bool active = false;

    // Various Attacks
    [Header("Attack Types")]
    public GameObject frostBlast;
    public int blastCount;
    public blastAttack bAttack;

    public GameObject pulse;
    public GameObject pulseWarningParticles;
    public float pulseTimer;
    private float pulseTimerMax;
    
    //Boss Mesh Variables
    public Renderer bossMesh;
    private string meshDirection;
    private float meshX;
    private float meshY;

    //Animation Vars
    public Animator anim;
    private bool spawnFinished;

    // Use this for initialization
    void Start () {
        attackTimerMax = attackTimer;
        meshDirection = "up";

        pulseTimerMax = pulseTimer;
	}
	
	// Update is called once per frame
	void Update () {
        active = GetComponent<enemyHealth>().active;

        // Makes his mesh do that cool woshy thing
        if (meshDirection == "up")
        {
            if (meshX > 10)
                meshDirection = "down";

            meshX += 0.01f;
            meshY += 0.01f;
        }
        else if (meshDirection == "down")
        {
            if (meshX <= 0)
                meshDirection = "up";
            meshX -= 0.01f;
            meshY -= 0.01f;
        }
        bossMesh.material.mainTextureScale = new Vector2(meshX, 1);

        // If boss is activated and not dead we do attack stuff
        if (active && !GetComponent<enemyHealth>().dead)
        {
            if (attackTimer <= 0)
            {
                // Rolls a dice to pick his next attack.
                attackSelection = 1;
                attackTimer = attackTimerMax;

                if (attackSelection == 1)
                {
                    // Frost Blast
                    for (int i = 0; i < blastCount; i++)
                    {
                        bAttack.bAttack();
                        GameObject currentBlast = (GameObject)Instantiate(frostBlast, bAttack.blastPosition, transform.rotation);
                    }
                }
            }
            else
            {
                attackTimer -= Time.deltaTime;
            }

            // AOE PULSE ATTACK
            if (pulseTimer <= 0)
            {
                GameObject pulseGo = (GameObject)Instantiate(pulse, new Vector3(transform.position.x, 0.5f, transform.position.z), transform.rotation);
                pulseGo.transform.parent = this.transform;
                pulseTimer = pulseTimerMax;
            }
            else pulseTimer -= Time.deltaTime;
        }
        
    }

    private void FixedUpdate()
    { // Friday do this: https://docs.unity3d.com/Manual/nav-CouplingAnimationAndNavigation.html
        if (Vector3.Distance(transform.position, player.position) >= minDistance)
        {
            anim.SetBool("Walk", true);
            anim.SetBool("Idle", false);
        }
        else
        {
            anim.SetBool("Idle", true);
            anim.SetBool("Walk", false);
        }

        if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("creature1walkforward"))
        {
            // Avoid any reload. For Sub States Use this: "Sub-StateMachine_Name"."State_Name".
            //transform.LookAt(player);

            // All this shit to walk forward towards the player
            var lookPos = player.position - transform.position; // Gets the players position and subtracts our current position
            lookPos.y = 0; // Removes the Y component so the boss doesn't tilt upwards if you're close to him
            var rotation = Quaternion.LookRotation(lookPos); // Sets a variatble for the Slerp
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * turnSpeed);
            // Turning Complete
            Vector3 horizontalForward = transform.forward; // Set a variable to hold the forward motion
            horizontalForward.y = 0f; // Remove any potential to rise on the Y plane
            horizontalForward.Normalize(); // Normalize it (not sure why but fuck it)
            transform.position += horizontalForward* moveSpeed * Time.deltaTime; // Then we move forward at our move speed
        }
        
    }
}
