using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDamage : MonoBehaviour
{
    Boss boss;
    PolygonCollider2D polygonCollider2D;

    public int flameDamage = 20;
    float flameDamageTimer = 0;

    private void Start()
    {
        polygonCollider2D = GetComponent<PolygonCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        int layer = collision.gameObject.layer;

        if (layer == 12)
        {
            Debug.Log("데미지 테스트");
            boss = collision.GetComponent<Boss>();
            boss.damaged(flameDamage);
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
