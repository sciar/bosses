using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frostShot : MonoBehaviour {

    private float duration = 10f;
    public Vector3 playerDirection;
    public float distance;

    public float damageValue;
    public float shotSpeed;

    private Rigidbody rigidBody;

    void Awake () {
        //Physics.IgnoreCollision(clone.collider, collider)
    }

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        this.GetComponent<damageBadguy>().attackDamage = damageValue; // Sends this attacks damage value to the attack damage script
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (duration > 0)
        {
            duration -= Time.deltaTime;
        }
        else
            Destroy(this.gameObject);

        rigidBody.AddForce(transform.forward * shotSpeed);
        //this.transform.position = playerDirection * shotSpeed;
    }
}
