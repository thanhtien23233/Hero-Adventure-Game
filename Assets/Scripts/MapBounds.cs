using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class MapBound: MonoBehaviour
{
    [SerializeField] GameObject notification;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(SceneManagement.Instance.ShowNotification());
        }
    }

}
