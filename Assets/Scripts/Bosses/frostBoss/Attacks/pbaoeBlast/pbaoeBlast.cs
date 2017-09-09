using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pbaoeBlast : MonoBehaviour
{

    [Header("Parts")]
    public GameObject cyl1;
    public GameObject warningIndicator;
    public float growthRate;

    private Vector3 cyl1MaxSize;
    private bool reverse;

    public float initialWarningTime;

    private void Awake()
    { // Store the size of the cyls
        cyl1MaxSize = cyl1.transform.localScale;
    }

    // Use this for initialization
    void Start()
    {
        warningIndicator.SetActive(true);

        // Set all cyls to 0
        cyl1.transform.localScale = Vector3.zero;

        // First we make them inactive
        cyl1.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (initialWarningTime <= 0 && !reverse)
        {
            cyl1.SetActive(true);
            if (cyl1.transform.localScale.x < cyl1MaxSize.x) // Only grow if it's not max size yet
                cyl1.transform.localScale += new Vector3(growthRate, 0, growthRate);
            else
                reverse = true;
            if (cyl1.transform.localScale.y < cyl1MaxSize.y) // Same for Y scaling
                cyl1.transform.localScale += new Vector3(0, growthRate * 0.1f, 0); // But slower since Y doesn't go as high
        }
        else if (reverse)
        {
            cyl1.transform.localScale -= new Vector3(growthRate, 0, growthRate);
            if (cyl1.transform.localScale.y > 0)
                cyl1.transform.localScale -= new Vector3(0, growthRate * 0.1f, 0);
            if (cyl1.transform.localScale.x < 0)
                Destroy(this.gameObject);
        }
            

        if (initialWarningTime > 0)
            initialWarningTime -= Time.deltaTime;
    }



}

