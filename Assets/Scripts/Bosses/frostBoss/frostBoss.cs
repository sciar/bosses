using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frostBoss : MonoBehaviour {

    // Movement
    [Header("Mobility Variables")]
    public float moveSpeed;
    public Transform player;
    public float minDistance;
    public float maxDistance;

    // Decision Tree
    private float attackTimer = 4f;
    private float attackTimerMax;
    private int attackSelection;

    // Various Attacks
    [Header("Attack Types")]
    public GameObject frostBlast;
    
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
	}
	
	// Update is called once per frame
	void Update () {

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


        if (attackTimer <= 0)
        {
            // Rolls a dice to pick his next attack.
            attackSelection = 1;

            attackTimer = attackTimerMax;
            // Makes a random circle aroundn the boss
            Vector3 pos = Random.insideUnitCircle * 5.0f;

            if (attackSelection == 1)
            {
                // Frost Blast
                GameObject currentBlast = (GameObject)Instantiate(frostBlast, new Vector3 (pos.x,1,pos.z), transform.rotation);
            }
        }
        else
        {
            attackTimer -= Time.deltaTime;

        }
    }

    private void FixedUpdate()
    { // Friday do this: https://docs.unity3d.com/Manual/nav-CouplingAnimationAndNavigation.html
        if (Vector3.Distance(transform.position, player.position) >= minDistance)
        {
            anim.SetTrigger("Walk");
        }
        else
        {
            anim.SetTrigger("Idle");
        }

        if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("creature1walkforward"))
        {
            // Avoid any reload. For Sub States Use this: "Sub-StateMachine_Name"."State_Name".
            transform.LookAt(player);
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
        
    }
}
