using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarOfFire : MonoBehaviour
{
    Boss boss;
    BreakableObj breakableObj;
    BoxCollider2D boxCollider2D;

    GameObject darkFlame;

    float flameDamage;
    float flameDamageTimer = 0;
    bool isDarkFlameOn = false;
    bool isSetMigicCircleDone = false;

    private void OnEnable()
    {
        darkFlame = Resources.Load<GameObject>("Prefabs/DarkFlame");

        boxCollider2D = GetComponent<BoxCollider2D>();
        transform.GetChild(0).gameObject.SetActive(false);

        flameDamage = 30.0f;
        flameDamageTimer = 0;

        StartCoroutine("After4");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int layer = collision.gameObject.layer;

        if(isDarkFlameOn)
        {
            if (layer == 12)
            {
                if (boss == null)
                    boss = collision.GetComponent<Boss>();

                GameObject darkTemp = Instantiate(darkFlame, boss.transform);
                darkFlame.transform.localPosition = new Vector3(0, 0, 0);
                boss.damaged(flameDamage + DamageControler.GetInstance().GetMonoDamage());
            }
            else if (collision.CompareTag("CanBroke"))
            {
                breakableObj = collision.GetComponent<BreakableObj>();
                breakableObj.breakObj();
                Debug.Log("DD");
                GameObject darkTemp = Instantiate(darkFlame, breakableObj.transform);
                darkFlame.transform.localPosition = new Vector3(0, 0, 0);
                breakableObj.breakObj(flameDamage + DamageControler.GetInstance().GetMonoDamage());
            }
            return;
        }

        if (layer == 12)
        {
            if (boss == null)
                boss = collision.GetComponent<Boss>();

            boss.damaged(flameDamage + DamageControler.GetInstance().GetMonoDamage());
        }
        else if (collision.CompareTag("CanBroke"))
        {
            breakableObj = collision.GetComponent<BreakableObj>();
            breakableObj.breakObj(flameDamage + DamageControler.GetInstance().GetMonoDamage());
        }
    }

    private void FixedUpdate()
    {
        if (!isSetMigicCircleDone)
            return;

        flameDamageTimer += Time.deltaTime;

        if (flameDamageTimer >= 1)
        {
            if (boxCollider2D.enabled)
            {
                boxCollider2D.enabled = false;
            }
            else
            {
                boxCollider2D.enabled = true;
            }
            flameDamageTimer = 0;
        }
    }

    IEnumerator After4()
    {
        yield return new WaitForSeconds(0.5f);
        isSetMigicCircleDone = true;
        transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(4);
        boxCollider2D.enabled = false;
        isDarkFlameOn = true;
        yield return new WaitForSeconds(0.01f);
        boxCollider2D.enabled = true;
        yield return new WaitForSeconds(0.02f);
        boxCollider2D.enabled = false;
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
