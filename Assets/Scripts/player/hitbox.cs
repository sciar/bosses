using UnityEngine;
using System.Collections;

public class hitbox : MonoBehaviour {
    // http://docs.unity3d.com/ScriptReference/MonoBehaviour.html This is where you go to find all of these

    public float destroyTimer;
    public int damageValue; // Passed from playerMovement when hitbox is instantiated

    // Variables passed in on spawn
    public GameObject player;
    public Vector3 playerDirection;
    public float distance;


    // Use this for initialization
    private void Start()
    {
        if (destroyTimer == 0)
        destroyTimer = 30f;

        if (distance == 0)
            distance = 2f;

    }

    // Update is called once per frame
    private void Update()
    {
        /*if (collision.collider.tag == "Enemy")
        {

            Destroy(collision.gameObject);
        }*/
    }
    
    // Called every fixed framerate frame
    private void FixedUpdate()
    {
        destroyTimer--;
        if (destroyTimer <= 0)
        {
            Destroy(gameObject);
        }

        // Sets the hitbox to track the player movement during the attack
        this.transform.position = player.transform.position + playerDirection * distance; // 2 adds distance from player same as instantiate distance in playerMovement
    }

    
    void OnTriggerEnter(Collider col)
    {
        enemyHealth eh1 = col.gameObject.GetComponent<enemyHealth>();
        
        //Debug.Log("Trigger is triggered");
        if (eh1 != null) // Checks if the component has the enemyHealth script
        {
            //Destroy(col.gameObject);
            eh1.damageValue = damageValue; // How I used to do it with static - Prob wrong need to pass var a dif way since static wont run per script (dunno how yet though)
        }
    }
    
}
