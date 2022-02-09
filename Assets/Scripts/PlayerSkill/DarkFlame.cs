using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkFlame : MonoBehaviour
{

    Boss boss = null;
    Player player = null;
    BreakableObj breakableObj = null;

    float curTime = 0;

    private void OnEnable()
    {
        transform.parent.CompareTag("CanBroke");
        transform.parent.CompareTag("Boss");
        transform.parent.CompareTag("Player");

        GameObject hitObj = transform.parent.gameObject;

        if (transform.parent.CompareTag("Boss"))
        {
            boss = hitObj.GetComponent<Boss>();
        }
        else if (transform.parent.CompareTag("CanBroke"))
        {
            breakableObj = hitObj.GetComponent<BreakableObj>();
        }
        else if(transform.parent.CompareTag("Player"))
        {
            player = hitObj.GetComponent<Player>();
        }
        Destroy(gameObject, 10);
    }

    private void Update()
    {
        curTime += Time.deltaTime;

        if(boss != null)
        {
            if(curTime > 1)
            {
                curTime = 0;
                boss.damaged(4);
            }
        }
        else if (breakableObj != null)
        {
            if (curTime > 1)
            {
                curTime = 0;
                breakableObj.breakObj(4);
            }
        }
        else if (player != null)
        {
            if (curTime > 2)
            {
                curTime = 0;
                player.damaged(1);
            }
        }
    }
}
