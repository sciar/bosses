using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class enemyHealthUI : MonoBehaviour {

    public float fadeTimer;
    public float fadeTimerMax;
    public Slider slider;

    public void showEnemyHealth(float current, float max) //Lets the script damageBadguy.cs call this function to reset the visibility of the UI element
    {
        fadeTimer = fadeTimerMax;
        slider.maxValue = max;
        slider.value = current;
    }

	// Update is called once per frame
	void Update () {
        if (fadeTimer > 0)
        {
            fadeTimer -= Time.deltaTime;
        }else // If we've run out of time we disable the HP bar at the top.
        {
            slider.value = 0;
        }
	}
}
