using UnityEngine;
using System.Collections;

public class codeHolder : MonoBehaviour
{
    // Useful for making global variables
    // http://wiki.unity3d.com/index.php/Singleton

    // This may help me later with render depths
    //http://breadcrumbsinteractive.com/two-unity-tricks-isometric-games/

    // Good person for basic tutorials
    // https://www.youtube.com/channel/UCkUIs-k38aDaImZq2Fgsyjw/videos

    //The thing you want to check is how to declare "Attack" and that's done in the Unity Input Manager (Fuck if I know how you're supposed to know this)
    // Edit->Project Settings->Input
    //if (Input.GetButtonDown("Attack") ) 


    // Use this for initialization
    void Start () {
        Debug.Log("This will print things to the console line");
	}
	
	// Update is called once per frame
	void Update () {
        //int a = Attack (5.0f); This will take the function 
        // This is how you move an object through space
        //transform.Translate(new Vector3(-1, 0, 1) * speed * Time.deltaTime);
    }

    /*/ This code has the character always face the mouse
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    RaycastHit hit;
        
    if (Physics.Raycast(ray,out hit,100))
    {
        lookPosition = hit.point;
    }

    Vector3 lookDir = lookPosition - transform.position;
    lookDir.y = 0;

    transform.LookAt(transform.position + lookDir, Vector3.up);
    /*/ // Mouse facing ends

    /* This creates a joystick deadzone
    // Handling stick deadzone
        float deadzone = 0.25f;
        Vector2 stickInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (stickInput.magnitude < deadzone)
            stickInput = Vector2.zero;

    //Modifies the angle of the object based on joystick input
        float angle = Mathf.Atan2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
     */

    // Really cool way to override a function to give it a hierarchy : https://www.youtube.com/watch?v=zmf6-VIv31w
    // public override void *Name* 


    // This lets me set an items transform to a parent object (Aka the player when you create something)
    //Transform.SetParent()


    //Functions
    // void - Means we expect nothing back
    // Float - Integer that has decimal places
    // Int - Whole numbers only
    // String - Durrr

    public float Attack(float dataWePassToFunction) // If you declare public anything can call this function
    {

        // This is how you write functions in C# Just slap them in any public class. Not sure how to make them global yet
        return (dataWePassToFunction + 0.5f); // This lets us send data back out of a function. This wont work if it's a void function
    } 
}
