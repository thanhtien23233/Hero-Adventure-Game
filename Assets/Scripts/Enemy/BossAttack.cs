using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour, IEnemy
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletMoveSpeed;
    [SerializeField] private int burstCount;
    [SerializeField] private float timeBetweenBurst;
    [SerializeField] private float restTime = 1f;

    private bool isShooting = false;

    public void Attack()
    {
        Debug.Log("Boss Attack");
        if (!isShooting)
        {
            StartCoroutine(ShootRoutine());
        }
    }

    private IEnumerator ShootRoutine()
    {
        isShooting = true;
        Debug.Log("Shooting");
        for (int i = 0; i < burstCount; i++)
        {
            Vector2 targetDirection = PlayerController.Instance.transform.position - transform.position;

            GameObject newbullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            newbullet.transform.right = targetDirection;

            if (newbullet.TryGetComponent(out Projectile projectile))
            {
                projectile.UpdateMoveSpeed(bulletMoveSpeed);

                yield return new WaitForSeconds(timeBetweenBurst);
            }
        }

        yield return new WaitForSeconds(restTime);
        isShooting = false;
    }
}
