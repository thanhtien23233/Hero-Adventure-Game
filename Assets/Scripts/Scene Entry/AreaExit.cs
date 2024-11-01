using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    private void OnTriggerEnter2D(Collider2D other) {
        
        SceneManager.LoadScene(sceneToLoad);
        Debug.Log("DataToKeep" + DataToKeep.CurrentHealth);
    }   
}
