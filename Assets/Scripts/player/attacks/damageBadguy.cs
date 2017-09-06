using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageBadguy : MonoBehaviour
{
    public float attackDamage;

    void OnTriggerEnter(Collider col)
    {
        //Debug.LogError("COLLIDED WITH: " + col.name);
        if (col.gameObject.tag == "Enemy")
        {
            col.gameObject.GetComponent<enemyHealth>().takeDamage(attackDamage);
            Debug.LogError("Enemy Takes: " + attackDamage);
        }
    }
}
