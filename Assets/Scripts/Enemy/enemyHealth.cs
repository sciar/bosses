using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class enemyHealth : MonoBehaviour {

    [Header("Health")]
    public float currentHealth;
    public float maxHealth;
    [Header("Animator")]
    public Animator anim;

    [HideInInspector]
    public bool dead;
    [HideInInspector]
    public bool active;


    // Use this for initialization
    void Start()
    {
        currentHealth = maxHealth; // Sets up our proper total HP pool
    }

    //When the enemy is created
    private void OnEnable()
    {

    }

    public void takeDamage(float amount)
    {
        if (!active) active = true;
        if (currentHealth > 0)
        {
            anim.SetTrigger("Hit");
            currentHealth -= amount;
            GameObject.Find("EnemyHP").GetComponent<enemyHealthUI>().showEnemyHealth(currentHealth, maxHealth); // Sets the fade timer to max every time we hit so the health bar will remain on screen
        }
        else if (currentHealth <= 0 && dead == false)
            OnDeath(); // If we're ded we die.
    }

    private void SetHealthUI()
    {

    }

    private void OnDeath()
    {
        Debug.LogError("Enemy dead! Good jorb you win");
        anim.SetTrigger("Die");
        dead = true; // So we only die once!
    }

}
