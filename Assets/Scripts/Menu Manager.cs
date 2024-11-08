using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject howToPlayCanvas; // Reference to the How to Play canvas

    // Function for Start Game button
    public void StartGame()
    {
        // Load scene 1 (replace "Scene1" with your actual scene name)
        Time.timeScale = 1f;
        SceneManager.LoadScene("Tutorial");
    }

    // Function for How to Play button
    public void ShowHowToPlay()
    {
        // Set the How to Play canvas active
        howToPlayCanvas.SetActive(true);
    }

    // Function for closing the How to Play canvas
    public void CloseHowToPlay()
    {
        // Set the How to Play canvas inactive
        howToPlayCanvas.SetActive(false);
    }

    // Function for Quit button
    public void QuitGame()
    {
        // Quit the application
        Application.Quit();

        // For debugging purposes in the Unity Editor

    }
}