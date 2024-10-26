using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagement : MonoBehaviour
{
    public static SceneManagement Instance { get; private set; }

    public string SceneTransitionName { get; private set; }
    private GameObject enemyParent;
    private GameObject exitObject;
    private bool exitActivated = false;

    private void Awake()
    {
        // Ensure only one instance of SceneManagement exists
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);  // Destroy duplicate instances
            return;
        }

        // Automatically find enemyParent by name or tag
        enemyParent = GameObject.Find("Enemy"); // or FindWithTag("EnemyParentTag")
        if (enemyParent == null)
        {
            Debug.LogError("Enemy Parent object not found! Ensure it exists in the scene with the correct name or tag.");
        }

        // Automatically find exitObject by name or tag
        exitObject = GameObject.Find("AreaExit"); // or FindWithTag("ExitTag")
        if (exitObject == null)
        {
            Debug.LogError("Exit object not found! Ensure it exists in the scene with the correct name or tag.");
        }
        DeactivateExit();
    }

    public void SetTransitionName(string sceneTransitionName)
    {
        this.SceneTransitionName = sceneTransitionName;
    }

    public void ActivateLevelCanvasWithDelay()
    {
        StartCoroutine(ActivateLevelCanvasAfterDelay(3f));
    }

    private IEnumerator ActivateLevelCanvasAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameObject levelCanvas = GameObject.Find("Level Canvas");
        if (levelCanvas != null)
        {
            levelCanvas.SetActive(false);
            Debug.Log("Level Canvas deactivated after 3 seconds!");
        }
        else
        {
            Debug.LogError("Level Canvas not found in the scene!");
        }
    }

    void Update()
    {
        // Check if enemyParent has any children (enemies)
        if (enemyParent != null && enemyParent.transform.childCount == 0 && !exitActivated)
        {
            // If there are no more enemies, activate the exit
            ActivateExit();
        }
    }

    private void ActivateExit()
    {
        if (exitObject != null)
        {
            exitObject.SetActive(true);
            exitActivated = true;  // Mark exit as activated to prevent repeated activation
            Debug.Log("Exit activated!");
        }
    }

    private void DeactivateExit()
    {
        if (exitObject != null)
        {
            exitObject.SetActive(false);
            exitActivated = false;  // Mark exit as activated to prevent repeated activation
            Debug.Log("Exit deactivated!");
        }
    }
}
