using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{

    // Function for Start Game button
    public void MainMenu()
    {
        // Load scene 1 (replace "Scene1" with your actual scene name)
        SceneManager.LoadScene("MainMenu");
    }

    // Function for How to Play button
    public void QuitGame()
    {
        Application.Quit();
    }

    // Function for closing the How to Play canvas
    public void Restart()
    {
        // Get the current active scene
        Scene currentScene = SceneManager.GetActiveScene();

        // Reload the current scene
        SceneManager.LoadScene(currentScene.name);
    }
}
