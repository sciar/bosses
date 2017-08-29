using UnityEngine;
using System.Collections;

public class playerAnimations : MonoBehaviour {

	void attackAnimation()
    {
        GetComponentInParent<playerMovement>().HitboxSpawn();
    }
}
