using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frostBoss : MonoBehaviour {

    // Decision Tree
    private float attackTimer = 4f;
    private float attackTimerMax;
    private int attackSelection;

    // Various Attacks
    public GameObject frostBlast;
    
    //Boss Mesh Variables
    public Renderer bossMesh;
    private string meshDirection;
    private float meshX;
    private float meshY;

    // Use this for initialization
    void Start () {
        attackTimerMax = attackTimer;
        meshDirection = "up";
	}
	
	// Update is called once per frame
	void Update () {
        if (meshDirection == "up")
        {
            if (meshX > 10)
                meshDirection = "down";

            meshX += 0.01f;
            meshY += 0.01f;
        }
        else if (meshDirection == "down")
        {
            if (meshX <= 0)
                meshDirection = "up";
            meshX -= 0.01f;
            meshY -= 0.01f;
        }
            
        bossMesh.material.mainTextureScale = new Vector2(meshX, 1);
        if (attackTimer <= 0)
        {
            // Rolls a dice to pick his next attack.
            attackSelection = 1;

            attackTimer = attackTimerMax;
            // Makes a random circle aroundn the boss
            Vector3 pos = Random.insideUnitCircle * 5.0f;

            if (attackSelection == 1)
            {
                // Frost Blast
                GameObject currentBlast = (GameObject)Instantiate(frostBlast, new Vector3 (pos.x,1,pos.z), transform.rotation);
            }
        }
        else
        {
            attackTimer -= Time.deltaTime;

        }
    }
}
