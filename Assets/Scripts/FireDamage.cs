﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDamage : MonoBehaviour
{
    Boss boss;
    PolygonCollider2D polygonCollider2D;

    public int flameDamage = 20;
    float flameDamageTimer = 0;
    BreakableObj breakableObj;

    private void OnEnable() {
        polygonCollider2D = GetComponent<PolygonCollider2D>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        int layer = collision.gameObject.layer;

        if (layer == 12)
        {
            if(boss == null)
                boss = collision.GetComponent<Boss>();

            boss.damaged(flameDamage);
            Debug.Log("데미지 인게이지");
        }
        else if (collision.CompareTag("CanBroke"))
        {
            breakableObj = collision.GetComponent<BreakableObj>();
            breakableObj.breakObj();
        }
    }

    private void FixedUpdate()
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
