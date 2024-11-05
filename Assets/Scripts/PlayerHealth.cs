using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private float knockBackThrustAmount;
    [SerializeField] private float damageRecoveryTime;
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private Slider healthBar;


    private int currentHealth;
    private bool canTakeDamage = true;
    private Knockback knockback;
    private Flash flash;

    private void Awake() {
        if (SceneManager.GetActiveScene().name == "Map1")
        {
            DataToKeep.CurrentHealth = 10;
        }
        flash = GetComponent<Flash>();
        knockback = GetComponent<Knockback>();
        gameOverCanvas.SetActive(false);
    }

    private void Start() {
        if (DataToKeep.CurrentHealth > 0)
        {
            currentHealth = DataToKeep.CurrentHealth;
        }
        else
        {
            currentHealth = maxHealth;
        }
        healthBar.value = currentHealth;
    }

    private void OnCollisionStay2D(Collision2D other) {
        EnemyAI enemy = other.gameObject.GetComponent<EnemyAI>();

        if (enemy && canTakeDamage) {
            TakeDamage(1);
            knockback.GetKnockedBack(other.gameObject.transform, knockBackThrustAmount);
            StartCoroutine(flash.FlashRoutine());          
        }
    }

    public void TakeDamage(int damageAmount) {
        canTakeDamage = false;
        Debug.Log("Current Health: " + currentHealth);
        currentHealth -= damageAmount;
        DataToKeep.CurrentHealth = currentHealth;
        Debug.Log(DataToKeep.CurrentHealth);
        healthBar.value = currentHealth;
        DetectDeath();
        StartCoroutine(DamageRecoveryRoutine());
    }

    public void KnockBack(Collider2D other)
    {
        knockback.GetKnockedBack(other.gameObject.transform, knockBackThrustAmount);
        StartCoroutine(flash.FlashRoutine());
    }
    private IEnumerator CheckDetectDeathRoutine()
    {
        yield return new WaitForSeconds(flash.GetRestoreMatTime());
        DetectDeath();
    }

    private IEnumerator DamageRecoveryRoutine() {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }

    public void DetectDeath()
    {
        if (currentHealth <= 0)
        {
            currentHealth = 10; // Ensure health does not go below zero
            DataToKeep.CurrentHealth = 10;
            ShowGameOver(); // Call to show the Game Over canvas
        }
    }
    private void ShowGameOver()
    {
        gameOverCanvas.SetActive(true); // Show the Game Over canvas
        Time.timeScale = 0f; // Stop the game
        // Optional: you can disable player controls or other elements here
    }

}
