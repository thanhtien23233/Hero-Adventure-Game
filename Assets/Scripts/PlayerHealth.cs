using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float knockBackThrustAmount = 10f;
    [SerializeField] private float damageRecoveryTime = 1f;
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private Slider healthBar;


    private int currentHealth;
    private bool canTakeDamage = true;
    private Knockback knockback;
    private Flash flash;

    private void Awake() {
        flash = GetComponent<Flash>();
        knockback = GetComponent<Knockback>();
        gameOverCanvas.SetActive(false);
    }

    private void Start() {
        currentHealth = maxHealth;
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
            currentHealth = 4; // Ensure health does not go below zero
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
