using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour 
{
    public static SceneManagement Instance { get; private set; }
    public string SceneTransitionName { get; private set; }
    [SerializeField] private GameObject enemyParent;
    [SerializeField] private GameObject exitObject;
    [SerializeField] private GameObject levelCanvas;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject levelClear;
    [SerializeField] private GameObject notification;
    [SerializeField] private GameObject mapObject;
    [SerializeField] private GameObject mapBorder;
    [SerializeField] private GameObject activeWeapon;
    [SerializeField] private GameObject? attackTutorial;
    [SerializeField] private GameObject? gameOver;

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
       
        if (attackTutorial != null)
        {
            attackTutorial.SetActive(false);
        }
        exitObject.SetActive(false);
        pauseMenu.SetActive(false);
        levelClear.SetActive(false);
        notification.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Pressed Escape");
            TogglePause();
        }
        if (enemyParent.transform.childCount == 0 && !exitActivated)
        {
            levelClear.SetActive(true);
            activeWeapon.SetActive(true);
            Scene currentScene = SceneManager.GetActiveScene();
            if (currentScene.name == "Tutorial")
            {
                attackTutorial.SetActive(false);
                levelClear.SetActive(true);
            }
            if (currentScene.name == "MapFinal")
            {
                Time.timeScale = 0;
                activeWeapon.SetActive(false);
                levelClear.SetActive(true);
            }
            else
            {
                ActivateLevelClearWithDelay();
                ActivateExit();
            }
        }
    }
    private void TogglePause()
    {
        isPaused = !isPaused;

        if (pauseMenu != null)
        {
            pauseMenu.SetActive(isPaused);
            activeWeapon.SetActive(!isPaused);
        }
        levelCanvas.SetActive(false);
        levelClear.SetActive(false);
        notification.SetActive(false);
        gameOver.SetActive(false);
        Time.timeScale = isPaused ? 0f : 1f; 
    }

    public IEnumerator ShowNotification()
    {
        notification.SetActive(true);
        yield return new WaitForSeconds(2);
        notification.SetActive(false);
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
            mapObject.SetActive(false);
            mapBorder.SetActive(false);
            exitObject.SetActive(true);
            exitActivated = true;
            Debug.Log("Exit activated!");
        }
    }
}