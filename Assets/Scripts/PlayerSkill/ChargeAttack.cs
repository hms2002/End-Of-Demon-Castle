using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeAttack : MonoBehaviour
{
    DamageControler damageControler;
    Boss boss;
    BreakableObj breakableObj;

    private void Start()
    {
        damageControler = DamageControler.GetInstance();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int layer = collision.gameObject.layer;

        if (layer == 12)
        {
            if (boss == null)
                boss = collision.GetComponent<Boss>();

            boss.damaged(damageControler.GetChargeDamage() + damageControler.GetDotDamage());
        }
        else if (collision.CompareTag("CanBroke"))
        {
            breakableObj = collision.GetComponent<BreakableObj>();
            breakableObj.breakObj(damageControler.GetChargeDamage() + damageControler.GetDotDamage());
        }
    }
}
