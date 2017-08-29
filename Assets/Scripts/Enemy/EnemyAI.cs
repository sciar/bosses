using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{

    public float watchDelay;
    public LayerMask blockingLayers;
    public float speed;
    public float attackRange;
    public AnimationCurve distanceModifierPercent;

    private bool seeking;
    private GameObject target;

    private Transform lookTarget; // This is to set a look target
    private Vector3 lookPosition; // More facing the player variables
    public float turnSpeed = 100f; //How fast we turn to face the target

    // Attacking Information
    private Object eHitboxPrefab; // Loads the prefab into a game object

    private void Update()
    {
        // This code will make the character face the direction they're moving
        lookTarget = GameObject.Find("Player").transform;
        Vector3 towards = lookTarget.position - transform.position;
        towards.Normalize();

        Quaternion target = Quaternion.LookRotation(towards, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, target, turnSpeed * Time.deltaTime);
    }

    IEnumerator Start()
    {
 
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
            seeking = false;
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
            if (target != null)
            {
                if (Vector3.Distance(transform.position, target.transform.position) > attackRange)
                    transform.position = transform.position + (target.transform.position - transform.position).normalized * DetermineSpeed(target.transform.position) * Time.deltaTime;
                else
                    yield return StartCoroutine(AttackPlayer());

                yield return null;
            }
        }
    }

    float DetermineSpeed(Vector3 targetPos)
    {
        return distanceModifierPercent.Evaluate(Vector3.Distance(transform.position, targetPos)) * speed;
    }

    IEnumerator AttackPlayer()
    {
        eHitboxPrefab = Resources.Load("eAttackHitbox");
        Debug.Log("ATTACK!");
        Vector3 enemyPosition = this.transform.position;
        Vector3 enemyDirection = this.transform.forward;
        float spawnDistance = 2;
        Vector3 spawnPos = enemyPosition + enemyDirection * spawnDistance;

        /* This used to create hitboxes to hit the player but it was sloppy and bad so now we comment it out
        GameObject latestHitbox = (GameObject)Instantiate(eHitboxPrefab, spawnPos, Quaternion.identity); // This assigns latestHitbox so we can send data if necessary
        latestHitbox.transform.parent = transform;
        latestHitbox.GetComponent<eHitbox>().type = "Placeholder"; // Sends this var to the latest hitbox
        */
        yield return new WaitForSeconds(2f);
    }
}
