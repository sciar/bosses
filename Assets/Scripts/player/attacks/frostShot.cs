using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frostShot : MonoBehaviour {

    private float duration = 10f;
    public Vector3 playerDirection;
    public float distance;

    public float damageValue;
    public float shotSpeed;

    private Rigidbody rigidbody;

    void Awake () {
        //Physics.IgnoreCollision(clone.collider, collider)
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (duration > 0)
        {
            duration -= Time.deltaTime;
        }
        else
            Destroy(this.gameObject);

        rigidbody.AddForce(transform.forward * shotSpeed);
        this.transform.position = playerDirection * shotSpeed;
    }
}
