using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerShot : MonoBehaviour {

    // All of this information is passed in the playerMovement script upon instantiating the prefab
    public float duration;
    private float durationMax;
    public Vector3 playerDirection;

    public float damageValue;
    public float shotSpeed;

    private Rigidbody rigidBody;
    private bool shatter;

    void Awake () {
        //Physics.IgnoreCollision(clone.collider, collider)
    }

    private void Start()
    {
        durationMax = duration;
        rigidBody = GetComponent<Rigidbody>();
        this.GetComponent<damageBadguy>().attackDamage = damageValue; // Sends this attacks damage value to the attack damage script
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (duration < durationMax * 0.4)
        {
            rigidBody.AddForce(new Vector3(0, -3.5f, 0));
        }
        if (duration > 0)
        {
            duration -= Time.deltaTime;
        }
        else if (duration < durationMax * 0.2f && !shatter)
        {
            //gameObject.AddComponent<meshShatter>();
            //StartCoroutine(gameObject.GetComponent<meshShatter>().SplitMesh());
            shatter = true;
        }
        else 
            Destroy(this.gameObject);

        rigidBody.AddForce(transform.forward * shotSpeed);
        //this.transform.position = playerDirection * shotSpeed;
    }
}
