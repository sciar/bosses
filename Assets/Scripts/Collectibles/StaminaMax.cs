using UnityEngine;
using System.Collections;

public class StaminaMax : MonoBehaviour
{

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
        PlayerManager.maxStamina += 10;
        PlayerManager.Instance.msFunction = true;
        Destroy(gameObject);
    }

}
