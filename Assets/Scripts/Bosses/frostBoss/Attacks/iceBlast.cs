using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iceBlast : MonoBehaviour
{

    private Vector3 scale = new Vector3(0f, 0f, 0f);
    public float timer;
    public string section;
    public GameObject damagePart;
    public GameObject indicator;

    // Use this for initialization
    void Start()
    {
        

        // Automatically assigns 

        if (section == "damage")
        {
            timer = 2f;
            // Sets our scale to start at zero
            this.transform.localScale = scale;
            // Disable the collider so we can't trigger damage early
            GetComponent<SphereCollider>().enabled = false;
        }
        else if (section == "location")
        {
            timer = 3.5f;
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (!damagePart)
            Destroy(gameObject);

        if (section == "damage")
        {
            // Once the timer is done counting down the attack will start
            if (timer > 0)
                timer -= Time.deltaTime;

            if (transform.localScale.x < 10f && timer <= 0)
            {
                transform.localScale += new Vector3(0.5f, 0.5f, 0.5f);
                // Enable the collider so we can take damage
                GetComponent<SphereCollider>().enabled = true;
                if (timer < 1)// Checks if the timer is almost out and turns off the particle indicator
                    Destroy(indicator);
            }
                
            else if (transform.localScale.x >= 10f)
                Destroy(gameObject);

        }

        if (section == "location")
        {
            if (timer > 0)
                timer -= Time.deltaTime;
            else
                Destroy(gameObject);

        }

    }
    

    
}

