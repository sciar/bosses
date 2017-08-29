using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class lockedChest : MonoBehaviour
{
    private float itemGrabTimer = 0.5f; // How long before you can get the powerup that spawns
    private bool grabTimerCountdown = false;
    public bool openChest = false;
    public bool gotItem = false;
    private bool spawnOnce = false; // makes sure we only spawn one powerup
    public GameObject chestLid;

    private GameObject newItem;

    // List of all available powerups
    public GameObject HPMax;
    public GameObject StaminaMax;

    List<GameObject> powerupList = new List<GameObject>(); // Creates a list where we store all the enemies in the game


    void Update()
    {
        if (openChest == true)
        {
            //var lidVariable = transform.FindChild("ChestLid");
            //Destroy(lidVariable);
            if (chestLid != null)
            GameObject.Destroy(chestLid);
            //this.gameObject
        }

        if (grabTimerCountdown == true)
            itemGrabTimer-= Time.deltaTime;

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            //Debug.Log("Got Key!");
            if (GameManager.Instance.KeyCount > 0 && openChest == false)
            {
                GameManager.Instance.KeyCount--;
                GameManager.Instance.keyUpdate(); // Update the key count
                openChest = true;
                SpawnPowerup();
                grabTimerCountdown = true;
            }
            else if (itemGrabTimer <= 0)
            {
                gotItem = true; // Sets up the component that item scripts will call
                grabTimerCountdown = false; // Resets the cooldown so it can be used again
                itemGrabTimer = 1f; // Resets the timer so it can be countdown again

                //newItem.GetComponent<HPMax>().gotItem = true; // Sets the child object script but wont work on dynamic items
                //Destroy(newItem);
            }
            
        }
    }

    void SpawnPowerup()
    {
        if (!spawnOnce) // Safety check to never spawn two items
        {
            spawnOnce = true;
            // Stores our list of available powerups
            powerupList.Add(HPMax);//Dragged in inspector
            powerupList.Add(StaminaMax);

            Vector3 place = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1, gameObject.transform.position.z);

            int powerupRandomizer = Random.Range(0, powerupList.Count); // Randomly picks an enemy from the list each loop
            newItem = (GameObject)Instantiate(powerupList[powerupRandomizer], place, Quaternion.identity); // This should take a value from the list of enemies and spawn it for each loop
            newItem.transform.parent = transform; // This attaches the object to this one as a child.
            newItem.name = newItem.name; // Does nothing but gives an example on how to pass vars to new spawned enemies
        }

    }

}
