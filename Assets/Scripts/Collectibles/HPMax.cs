using UnityEngine;
using System.Collections;

public class HPMax : MonoBehaviour {

    public bool gotItem;
    private bool powerupOnce = false;

    void Update()
    {
        
        gotItem = this.transform.parent.GetComponentInParent<lockedChest>().gotItem; // Grabs info from the chest to see if we've retrieved the item

        if (gotItem && !powerupOnce)
        {
            //Debug.Log("Gothere");
            powerup();
            powerupOnce = true;
        }
            
    }

    void powerup()
    {
        PlayerManager.maxHealth += 10;
        PlayerManager.Instance.mhFunction = true;
        Destroy(gameObject);
    }

    /*
    void OnTriggerEnter(Collider col)
    {
        //Debug.Log("Trigger is triggered");
        if (col.gameObject.tag == "Player")
        {
            //Debug.Log("Got Key!");
            PlayerManager.maxHealth += 10;
            PlayerManager.Instance.mhFunction = true;
            // This is where we send the take damage code to the player
            Destroy(gameObject);
        }
    }*/
}
