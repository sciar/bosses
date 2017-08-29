using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damagePlayer : MonoBehaviour {

    public float attackDamage;

    void OnTriggerEnter(Collider col)
    {
        Debug.LogError("COLLIDED WITH: " + col.name);
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<playerHP>().takeDamage(attackDamage);
            Debug.LogError("Damage Applied");

        }
    }
}
