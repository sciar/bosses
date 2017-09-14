using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerUI : MonoBehaviour {

    public GameObject UIattackCooldown;
    public bool attackOnCooldown;
    private Animation currentAttackAnimatorState;
    public Animator attackCooldownAnim;

    private void Update()
    {
        currentAttackAnimatorState = attackCooldownAnim.GetCurrentAnimatorStateInfo(0);

        if (attackOnCooldown && currentBaseState.nameHash == atakState)
        {
            Debug.Log("Do Stuff Here");
        }
    }

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
