using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    [SerializeField] private int damageAmount = 1;

    //private void Start()
    //{
    //    MonoBehaviour currenActiveWeapon = ActiveWeapon.CurrentActiveWeapon;
    //    damageAmount = (currenActiveWeapon as IWeapon).GetWeaponInfo().weaponDamage;
    //}

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<EnemyHealth>())
        {
            EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(damageAmount);
        }
    }

}
