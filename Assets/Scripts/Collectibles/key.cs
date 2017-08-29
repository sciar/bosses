using UnityEngine;
using System.Collections;

public class key : MonoBehaviour {

    void OnTriggerEnter(Collider col)
    {
        //Debug.Log("Trigger is triggered");
        if (col.gameObject.tag == "Player")
        {
            //Debug.Log("Got Key!");
            GameManager.Instance.KeyCount++;
            GameManager.Instance.keyUpdate();
            Destroy(gameObject);
        }
    }
}
