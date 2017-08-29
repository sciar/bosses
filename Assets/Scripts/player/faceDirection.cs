using UnityEngine;
using System.Collections;

public class faceDirection : MonoBehaviour {

    private playerMovement movement;

    public void Init(playerMovement movement)
    {
        this.movement = movement;
    }


    // Update is called once per frame
    private void Update()
    {
        transform.position = movement.transform.position + movement.lastDirection * 5f;
    }

}
