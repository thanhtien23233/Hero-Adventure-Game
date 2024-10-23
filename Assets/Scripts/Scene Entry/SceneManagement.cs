using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagement : Singleton<SceneManagement>
{
    public string SceneTransitionName { get; private set; }
    [SerializeField] private GameObject enemyParent; 
    [SerializeField] private GameObject exitObject;
    private bool exitActivated = false;
    public void SetTransitionName(string sceneTransitionName) {
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
        if (enemyParent.transform.childCount == 0 && !exitActivated)
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
}