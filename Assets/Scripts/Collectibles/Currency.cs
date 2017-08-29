using UnityEngine;
using System.Collections;

public class Currency : MonoBehaviour {

    void OnTriggerEnter(Collider col)
    {
        //Debug.Log("Trigger is triggered");
        if (col.gameObject.tag == "Player")
        {
            //Debug.Log("Got Key!");
            GameManager.Instance.CurrencyCount++;
            GameManager.Instance.currencyUpdate();
            Destroy(gameObject);
        }
    }
}
