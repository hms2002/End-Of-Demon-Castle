using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowAssult : MonoBehaviour
{
    float damage;
    Boss boss;
    BreakableObj breakableObj;

    private void Awake()
    {
        boss = FindObjectOfType<Boss>();
        damage = 10f;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("DD");
        int layer = collision.gameObject.layer;

        if (layer == 12)
        {
            if (boss == null)
                boss = collision.GetComponent<Boss>();

            boss.damaged(damage);
        }
        else if (collision.CompareTag("CanBroke"))
        {
            breakableObj = collision.GetComponent<BreakableObj>();
            breakableObj.breakObj();
        }
    }
}
