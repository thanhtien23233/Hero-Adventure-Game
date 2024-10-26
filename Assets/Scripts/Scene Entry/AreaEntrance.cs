using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour
{
    [SerializeField] private string transitionName;

    private void Start() {
        if (transitionName == SceneManagement.Instance.SceneTransitionName)
        {
            Debug.Log("Enter New Level");
            PlayerController.Instance.transform.position = this.transform.position;
            //CameraMove.Instance.SetPlayerCameraFollow();
            //CameraMove.Instance.AssignCameraBounds();
            //SceneManagement.Instance.ActivateLevelCanvasWithDelay();
        }
    }
}
