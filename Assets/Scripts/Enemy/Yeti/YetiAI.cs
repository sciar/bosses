using UnityEngine;
using System.Collections;

public class YetiAI : MonoBehaviour
{
    public Animator anim; // Used to control the animator

    public float watchDelay;
    public LayerMask blockingLayers;
    public float speed;
    public float attackRange;
    public AnimationCurve distanceModifierPercent;

    private bool seeking;
    private GameObject target;
    public bool dead; // Variable for setting up if this enemy is dead

    private Transform lookTarget; // This is to set a look target
    private Vector3 lookPosition; // More facing the player variables
    public float turnSpeed = 100f; //How fast we turn to face the target

    // Hitbox info
    private string hitboxType;

    // Attacking Information
    private Object eHitboxPrefab; // Loads the prefab into a game object
    public float spawnDistance; // How far away the hitbox spawns (useful for larger enemies)

    //Animation variables
    //int attackHash = Animator.StringToHash("Attack");

    private void Update()
    {
        // This code will make the character face the direction they're moving
        if (HasLOS() && !anim.GetBool("Attacking") && !dead)
        {
            lookTarget = GameObject.Find("Player").transform;
            Vector3 towards = lookTarget.position - transform.position;
            towards.Normalize();

            Quaternion target = Quaternion.LookRotation(towards, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, target, turnSpeed * Time.deltaTime);
        }

    }

    IEnumerator Start()
    {
        // Hitbox type declaration
        hitboxType = "Hitboxes/YetiHitbox";

        anim = GetComponent<Animator>();
        yield return null;
        StartCoroutine(WatchForPlayer());
    }

    IEnumerator WatchForPlayer()
    {
        while (true)
        {
            if (seeking)
                CheckStillSee();
            else
                CheckCanSee();

            yield return new WaitForSeconds(watchDelay);
        }
    }

    void CheckStillSee()
    {
        if (!HasLOS())
        {
            seeking = false;
            anim.SetBool("Walking", false); // Sets the animator to walk
            anim.SetBool("Attacking", false); // Stops attack animations
        }
            
    }

    void CheckCanSee()
    {
        if (HasLOS())
            StartCoroutine(HuntPlayer());
    }

    bool HasLOS()
    {
        Vector3 playerPos = PlayerManager.Instance.mainPlayer.transform.position;
        Ray ray = new Ray(transform.position, playerPos - transform.position);
        return !Physics.Raycast(ray, Vector3.Distance(playerPos, transform.position), blockingLayers);
    }

    IEnumerator HuntPlayer()
    {
        seeking = true;
        target = PlayerManager.Instance.mainPlayer.gameObject;

        while (seeking)
        {
            if (dead)
            {
                yield return null;
            }
            else if (target != null)
            {
                if (Vector3.Distance(transform.position, target.transform.position) > attackRange)
                {
                    transform.position = transform.position + (target.transform.position - transform.position).normalized * DetermineSpeed(target.transform.position) * Time.deltaTime;
                    anim.SetBool("Walking", true); // Sets the animator to walk
                    anim.SetBool("Attacking", false); // Makes us not attack
                }
                else
                {
                    anim.SetBool("Attacking", true); // Sets the animator to attack
                    anim.SetBool("Walking", false); // Sets it so it wont walk
                    //yield return StartCoroutine(AttackPlayer());
                    yield return new WaitForSeconds(1.3f);
                    StartCoroutine(AttackPlayer());
                }
                    
                yield return null;
            }
        }
        yield return null;
    }

    float DetermineSpeed(Vector3 targetPos)
    {
        return distanceModifierPercent.Evaluate(Vector3.Distance(transform.position, targetPos)) * speed;
    }

    IEnumerator AttackPlayer()
    {
        
        yield return new WaitForSeconds(2f);
    }

    void Strike() // Spawns the hitboxes
    {
        eHitboxPrefab = Resources.Load(hitboxType);
        Vector3 enemyPosition = this.transform.position;
        Vector3 enemyDirection = this.transform.forward;
        spawnDistance = 4;
        Vector3 spawnPos = enemyPosition + enemyDirection * spawnDistance;

    }
}
