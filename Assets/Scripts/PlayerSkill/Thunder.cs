using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : MonoBehaviour
{
    Boss boss;
    BreakableObj breakableObj;

    BoxCollider2D boxCollider2D;

    float damage;

    float cur;

    private void Start()
    {
        
        damage = 3;
        cur = 0.5f;

        boxCollider2D = GetComponent<BoxCollider2D>();

        StartCoroutine(Damage());

        Destroy(gameObject, 10);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int layer = collision.gameObject.layer;

        if (layer == 12)
        {
            if (boss == null)
                boss = collision.GetComponent<Boss>();

            boss.damaged(damage + DamageControler.GetInstance().GetDotDamage());
        }
        else if (collision.CompareTag("CanBroke"))
        {
            breakableObj = collision.GetComponent<BreakableObj>();
            breakableObj.breakObj(damage + DamageControler.GetInstance().GetDotDamage());
        }
    }

    private void Update()
    {
        //cur -= Time.deltaTime;
        transform.localPosition = Vector3.zero;
        //if (cur < 0)
        //{
        //    cur = 0.5f;
        //    boxCollider2D.enabled = true;
        //}
        //else
        //{
        //    boxCollider2D.enabled = false;
        //}
    }

    IEnumerator Damage()
    {
        while(true)
        {
            Debug.Log("d");
            boxCollider2D.enabled = true;
            yield return new WaitForSeconds(0.1f);
            boxCollider2D.enabled = false;
            yield return new WaitForSeconds(0.45f);
        }
    }
}
