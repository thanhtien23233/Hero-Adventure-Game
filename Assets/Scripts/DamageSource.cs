using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    //private int damageAmount;

    //private void Start()
    //{
    //    MonoBehaviour currenActiveWeapon = ActiveWeapon.CurrentActiveWeapon;
    //    damageAmount = (currenActiveWeapon as IWeapon).GetWeaponInfo().weaponDamage;
    //}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<EnemyAI>())
        {
            Debug.Log("AAA");
        }
        //EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        //enemyHealth?.TakeDamage(damageAmount);
    }

}
