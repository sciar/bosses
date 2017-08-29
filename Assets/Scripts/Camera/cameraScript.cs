using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
using System.Collections.Generic;

public class cameraScript : MonoBehaviour {

    public float dampTime = 0.2f; // Approximate time for the camera to refocus.
    private float Xpos;
    private float Ypos;
    private float Zpos;
    private Vector3 playerPosition;
    private Vector3 desiredPosition;
    private Vector3 velocity;
    private PostEffectsBase[] effects;

    

    // Use this for initialization
    void Start () {
        Xpos = transform.position.x;
        Ypos = transform.position.y;
        Zpos = transform.position.z;
        //cameraPosition = transform.position;
        //cameraPosition = GameObject.Find("Player");

        GetComponentInChildren<SepiaTone>().enabled = false;
        effects = GetComponentsInChildren<PostEffectsBase>();
    }
	

    void FixedUpdate()
    {
        if (GameObject.Find("Player" ) != null)
        {
            playerPosition = GameObject.Find("Player").transform.position;
            desiredPosition = new Vector3(playerPosition.x + Xpos - 3, playerPosition.y + Ypos, playerPosition.z + Zpos + 5);
            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, dampTime);
        }
        
    }

}
