using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkFlame : MonoBehaviour
{

    Boss boss = null;
    Player player = null;
    BreakableObj breakableObj = null;

    float curTime = 0;

    //#.스킬 아이콘
    GameObject buffICON;
    GameObject tempBuffICON;

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

        if(player != null)
        {
            //#.스킬 아이콘 및 강제 초기화
            buffICON = Resources.Load<GameObject>("Prefabs/Buff/Buff_DarkFlame");
            tempBuffICON = Instantiate(buffICON, BuffLayoutSetting.GetInstance().transform);
            SkillSelectManager.GetInstance().Init += BeforeDEL;
            BuffLayoutSetting.GetInstance().AddBuff();
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
                boss.damaged(4 + DamageControler.GetInstance().GetDotDamage());
            }
        }
        else if (breakableObj != null)
        {
            if (curTime > 1)
            {
                curTime = 0;
                breakableObj.breakObj(4 + DamageControler.GetInstance().GetDotDamage());
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

    public void BeforeDEL()
    {
        Destroy(tempBuffICON);
        BuffLayoutSetting.GetInstance().SubBuff();

        Destroy(gameObject);
    }

    private void OnDestroy()
    {

        Destroy(tempBuffICON);
        BuffLayoutSetting.GetInstance().SubBuff();
        SkillSelectManager.GetInstance().Init -= BeforeDEL;
    }
}
