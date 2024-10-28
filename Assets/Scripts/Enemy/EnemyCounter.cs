using UnityEngine;
using UnityEngine.UI;

public class EnemyCounter : MonoBehaviour
{
    public Text enemyCountText; // Reference to the Text UI element
    public Transform enemyParent; // Reference to the Enemy parent GameObject

    void Update()
    {
        // Count the active child objects under the Enemy parent
        int remainingEnemies = enemyParent.childCount;

        // Update the text on screen
        enemyCountText.text = "Remaining Enemy: " + remainingEnemies;
    }
}
