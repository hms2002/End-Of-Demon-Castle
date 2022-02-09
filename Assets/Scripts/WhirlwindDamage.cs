using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhirlwindDamage : MonoBehaviour
{
    Boss boss;
    PolygonCollider2D collisionMask;

    int damage = 10;
    float whirlwindDamageTimer = 0.0f;
    BreakableObj breakableObj;

    private void OnEnable()
    {
        collisionMask = GetComponent<PolygonCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int layer = collision.gameObject.layer;

        if (layer == 12)
        {
            if (boss == null)
            {
                boss = collision.GetComponent<Boss>();
            }

            boss.damaged(damage + DamageControler.GetInstance().GetDotDamage());
        }
        else if (collision.CompareTag("CanBroke"))
        {
            Debug.Log("¿Ô³ª?");
            breakableObj = collision.GetComponent<BreakableObj>();
            breakableObj.breakObj(damage + DamageControler.GetInstance().GetDotDamage());
        }
    }

    private void FixedUpdate()
    {
        whirlwindDamageTimer += Time.deltaTime;

        if (whirlwindDamageTimer >= 0.2f)
        {
            if (collisionMask.enabled)
            {
                collisionMask.enabled = false;
            }
            else
            {
                collisionMask.enabled = true;
            }

            whirlwindDamageTimer = 0;
        }
    }
}
