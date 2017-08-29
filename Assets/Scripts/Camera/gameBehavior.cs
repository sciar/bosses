using UnityEngine;
using System.Collections;

public class gameBehavior : MonoBehaviour {
    // http://docs.unity3d.com/ScriptReference/MonoBehaviour.html This is where you go to find all of these

    // Screen Shake info
    static public float screenShake;
    private float shakeTime;
    static public float shakeAmount = 0.5f;

    // Use this for initialization
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        if (screenShake > 0)
        {
            //Debug.Log("Behavior Shake");
            screenShake--;
            float randomX = Random.Range(-shakeAmount, shakeAmount);
            float randomZ = Random.Range(-shakeAmount, shakeAmount);

            this.transform.position = this.transform.position + new Vector3(randomX, 0, randomZ);
            //this.transform.localPosition = Random.insideUnitSphere * shakeAmount; //(this rules for a random shake in a sphere which this game wont support)
        }

    }
    
    // Called every fixed framerate frame
    private void FixedUpdate()
    {

    }
}
