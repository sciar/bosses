using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class enemyHealth : MonoBehaviour {

    public float currentHealth;
    public float maxHealth;

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
        currentHealth -= amount;
        GameObject.Find("EnemyHP").GetComponent<enemyHealthUI>().showEnemyHealth(currentHealth, maxHealth); // Sets the fade timer to max every time we hit so the health bar will remain on screen
        if (currentHealth <= 0)
            OnDeath(); // If we're ded we die.
    }

    private void SetHealthUI()
    {

    }

    private void OnDeath()
    {
        Debug.LogError("Enemy dead! Good jorb you win");
    }

}
