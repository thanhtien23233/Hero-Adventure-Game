using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Tutorial : MonoBehaviour
{
    [SerializeField] GameObject movementTutorial;
    [SerializeField] GameObject attackTutorial;
    [SerializeField] GameObject colliderTutorial;
    [SerializeField] GameObject activeWeapon;


    private void Awake()
    {
        attackTutorial.SetActive(false);
        activeWeapon.SetActive(false);
        SetActiveAfterDelay();
    }
    public void SetActiveAfterDelay()
    {
        StartCoroutine(SetActiveAfterDelay(2f));
    }
    public IEnumerator SetActiveAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Time.timeScale = 1f;
        movementTutorial.SetActive(true);
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            movementTutorial.SetActive(false);
            attackTutorial.SetActive(true);
            colliderTutorial.SetActive(false);
            activeWeapon.SetActive(true);
        }
    }
}
