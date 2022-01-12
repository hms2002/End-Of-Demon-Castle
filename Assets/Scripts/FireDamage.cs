using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDamage : MonoBehaviour
{
    Boss boss;
    BreakableObj breakableObj;
    PolygonCollider2D polygonCollider2D;

    public int flameDamage = 20;
    float flameDamageTimer = 0;

    private void OnEnable() {
        polygonCollider2D = GetComponent<PolygonCollider2D>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        int layer = collision.gameObject.layer;

        if (layer == 12) //보스 공격
        {
            boss = collision.GetComponent<Boss>();
            boss.damaged(flameDamage);
        }
        else if (collision.CompareTag("CanBroke"))
        {
            breakableObj = collision.GetComponent<BreakableObj>();
            breakableObj.breakObj();
        }
    }

    private void Update()
    {
        flameDamageTimer += Time.deltaTime;
        if (flameDamageTimer > 0.1f)
        {
            if (polygonCollider2D.enabled)
            {
                polygonCollider2D.enabled = false;
            }
            else
            {
                polygonCollider2D.enabled = true;
            }
            flameDamageTimer = 0;
        }
    }
}
