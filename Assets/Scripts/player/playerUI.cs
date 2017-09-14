using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerUI : MonoBehaviour {

    public GameObject UIattackCooldown;
    public bool attackOnCooldown;
    public Animator attackCooldownAnim;
	
    public void uiAttack(string status)
    {
        if (status == "start")
        {
            attackCooldownAnim.Play("attackUsed");
        }
        else
        {
            attackCooldownAnim.Play("attackReady");
        }
            
    }
}
