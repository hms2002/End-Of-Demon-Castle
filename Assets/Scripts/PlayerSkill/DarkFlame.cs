using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkFlame : MonoBehaviour
{
    Boss boss = null;
    Player player = null;
    BreakableObj breakableObj = null;

    float curTime = 0;
    int count;

    //#.��ų ������
    GameObject buffICON;
    GameObject tempBuffICON;

    private void OnEnable()
    {
        count = 1;
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
            //#.��ų ������ �� ���� �ʱ�ȭ
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
                boss.damaged(8 + DamageControler.GetInstance().GetDotDamage());
            }
        }
        else if (breakableObj != null)
        {
            if (curTime > 1)
            {
                curTime = 0;
                breakableObj.breakObj(8 + DamageControler.GetInstance().GetDotDamage());
            }
        }
        else if (player != null)
        {
            if (curTime > 1)
            {
                if (count > 4)
                {
                    BeforeDEL();
                    return;
                }
                curTime = 0;
                player.damaged(5);
                count++;
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
