using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class setupLocalPlayer : NetworkBehaviour {

    // Use this for initialization
    private void Start()
    {
        if (isLocalPlayer)
            GetComponent<playerMovement>().enabled = true;
    }

}
