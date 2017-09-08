using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pbaoeBlast : MonoBehaviour
{

    [Header("Parts")]
    public GameObject cyl1;
    public GameObject cyl2;
    public GameObject cyl3;
    public GameObject cyl4;
    public GameObject warningIndicator;
    public float growthRate;

    private Vector3 cyl1MaxSize;
    private Vector3 cyl2MaxSize;
    private Vector3 cyl3MaxSize;
    private Vector3 cyl4MaxSize;

    public float initialWarningTime;
    public float blastDelay;
    private float blastDelayMax;
    private int cylCount;

    private void Awake()
    { // Store the size of the cyls
        cyl1MaxSize = cyl1.transform.localScale;
        cyl2MaxSize = cyl2.transform.localScale;
        cyl3MaxSize = cyl3.transform.localScale;
        cyl4MaxSize = cyl4.transform.localScale;
    }

    // Use this for initialization
    void Start()
    {
        blastDelayMax = blastDelay;
        warningIndicator.SetActive(true);

        // Set all cyls to 0
        cyl1.transform.localScale = Vector3.zero;
        cyl2.transform.localScale = Vector3.zero;
        cyl3.transform.localScale = Vector3.zero;
        cyl4.transform.localScale = Vector3.zero;

        // First we make them inactive
        cyl1.SetActive(false);
        cyl2.SetActive(false);
        cyl3.SetActive(false);
        cyl4.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (cylCount >= 1)
        {
            if (cyl1.transform.localScale.x < cyl1MaxSize.x)
                cyl1.transform.localScale += new Vector3(growthRate, 0, growthRate);
            if (cyl1.transform.localScale.y < cyl1MaxSize.y)
                cyl1.transform.localScale += new Vector3(0, growthRate * 0.1f, 0);
        }
        if (cylCount >= 2)
        {
            if (cyl2.transform.localScale.x < cyl2MaxSize.x)
                cyl2.transform.localScale += new Vector3(growthRate, 0, growthRate);
            if (cyl2.transform.localScale.y < cyl2MaxSize.y)
                cyl2.transform.localScale += new Vector3(0, growthRate * 0.2f, 0);
        }
        if (cylCount >= 3)
        {
            if (cyl3.transform.localScale.x < cyl3MaxSize.x)
                cyl3.transform.localScale += new Vector3(growthRate, 0, growthRate);
            if (cyl3.transform.localScale.y < cyl3MaxSize.y)
                cyl3.transform.localScale += new Vector3(0, growthRate * 0.3f, 0);
        }
        if (cylCount >= 4)
        {
            if (cyl4.transform.localScale.x < cyl4MaxSize.x)
                cyl4.transform.localScale += new Vector3(growthRate, 0, growthRate);
            if (cyl4.transform.localScale.y < cyl4MaxSize.y)
                cyl4.transform.localScale += new Vector3(0, growthRate * 0.4f, 0);
        }


        if (blastDelay <= 0)
        {
            cylCount++;
            if (cylCount == 1)
                cyl1.SetActive(true);
            else if (cylCount == 2)
                cyl2.SetActive(true);
            else if (cylCount == 3)
                cyl3.SetActive(true);
            else if (cylCount == 4)
                cyl4.SetActive(true);
            else if (cylCount == 5)
                Destroy(this.gameObject);
            blastDelay = blastDelayMax;
        }
        else if (initialWarningTime <= 0)
            blastDelay -= Time.deltaTime;

        if (initialWarningTime > 0)
            initialWarningTime -= Time.deltaTime;
    }



}

