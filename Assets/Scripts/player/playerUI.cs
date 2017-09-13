using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerUI : MonoBehaviour {

    public GameObject UIattackCooldown;
	
	// Update is called once per frame
	void Update () {
		if (GetComponent<playerMovement>().playerShotCooldown > 0)
        {
            UIattackCooldown.GetComponent<Image>().color = Color.red;
        }
        else
            UIattackCooldown.GetComponent<Image>().color = Color.blue;
    }
}
