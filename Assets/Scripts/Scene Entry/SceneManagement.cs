using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagement : MonoBehaviour 
{
    public static SceneManagement Instance { get; private set; }
    public string SceneTransitionName { get; private set; }
    [SerializeField] private GameObject enemyParent;
    [SerializeField] private GameObject exitObject;
    [SerializeField] private GameObject levelCanvas;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject levelClear;
    private bool isPaused = false;
    private bool exitActivated = false;
    public void Awake()
    {
        ActivateLevelCanvasWithDelay();
        //CameraMove.Instance.SetPlayerCameraFollow();
        //CameraMove.Instance.AssignCameraBounds();
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        Debug.Log("Exit Hidden");
        exitObject.SetActive(false);
        Debug.Log("Pause Hidden");
        pauseMenu.SetActive(false);
        Debug.Log("Clear Level Hidden");
        levelClear.SetActive(false);
    }

    void Update()
    {
        // Check if the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Pressed Escape");
            TogglePause();
        }
        // Check if enemyParent has any children (enemies)
        if (enemyParent.transform.childCount == 0 && !exitActivated)
        {
            levelClear.SetActive(true);
            ActivateLevelClearWithDelay();
            ActivateExit();
        }
    }
    private void TogglePause()
    {
        // Toggle the pause state
        isPaused = !isPaused;

        // Show or hide the pause menu
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(isPaused);
        }
        levelCanvas.SetActive(false);
        // Pause or unpause the game
        Time.timeScale = isPaused ? 0f : 1f;  // 0 pauses, 1 resumes normal speed
    }
    public void SetTransitionName(string sceneTransitionName)
    {
        this.SceneTransitionName = sceneTransitionName;
    }
    public void ActivateLevelCanvasWithDelay()
    {
        StartCoroutine(ActivateLevelCanvasAfterDelay(2f));
    }
    public void ActivateLevelClearWithDelay()
    {
        StartCoroutine(DeActivateLevelClearAfterDelay(2f));
    }
    private IEnumerator ActivateLevelCanvasAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (levelCanvas != null)
        {
            levelCanvas.SetActive(false);
            Debug.Log("Level Canvas deactivated after 2 seconds!");
        }
    }
    private IEnumerator DeActivateLevelClearAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (levelClear != null)
        {
            levelClear.SetActive(false);
            Debug.Log("Level Clear deactivated!");
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