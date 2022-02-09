using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDamage : MonoBehaviour
{
    Boss boss;
    PolygonCollider2D polygonCollider2D;

    int flameDamage = 10;
    float flameDamageTimer = 0;
    BreakableObj breakableObj;

    Vector3 originLocalPos;

    private void OnEnable()
    {
        originLocalPos = transform.localPosition;
        polygonCollider2D = GetComponent<PolygonCollider2D>();
       
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        int layer = collision.gameObject.layer;
        
        if (layer == 12)
        {
            if(boss == null)
                boss = collision.GetComponent<Boss>();

            boss.damaged(flameDamage + DamageControler.GetInstance().GetDotDamage());
        }
        else if (collision.CompareTag("CanBroke"))
        {
            breakableObj = collision.GetComponent<BreakableObj>();
            breakableObj.breakObj(flameDamage + DamageControler.GetInstance().GetDotDamage());
        }
    }

    private void FixedUpdate()
    {
        transform.parent.transform.localPosition = Vector3.zero;
        transform.localPosition = originLocalPos;
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
