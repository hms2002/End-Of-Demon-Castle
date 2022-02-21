using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill_Berserk : Skill_ID
{
    Player player;
    GameObject berserkEffect;

    float coolTime = 60;
    float curTime = 0;

    //#.��ų ������
    GameObject buffICON;
    GameObject tempBuffICON;

    bool isSkillOn = false;
    private GameObject tempObj;

    private void Start()
    {
        player = Player.GetInstance();
        berserkEffect = Resources.Load<GameObject>("Prefabs/Berserk_Pivot");

        coolTimeSlider.maxValue = coolTime;

        //#.��ų ������ �� �ʱ�ȭ
        buffICON = Resources.Load<GameObject>("Prefabs/Buff/Buff_Bersrk");
        SkillSelectManager.GetInstance().Init += BeforeDEL;
    }

    private void Update()
    {
        if(isSkillOn == false)
        {
            curTime -= Time.deltaTime;
            coolTimeSlider.value = curTime;
        }
    }

    public override void SkillOn()
    {
        if (curTime > 0)
            return;
        isSkillOn = true;
        curTime = coolTime;
        player.speed = 10;
        player.atkCoolTime = 0.15f;
        player.damaged(20);

        tempObj = Instantiate(berserkEffect, player.transform);

        tempObj.transform.localPosition = new Vector2(0, 0);

        Destroy(tempObj, 20);

        DamageControler.GetInstance().UpgradeDamage();

        StartCoroutine(OnBerserk());
    }

    IEnumerator OnBerserk()
    {
        tempBuffICON = Instantiate(buffICON, BuffLayoutSetting.GetInstance().transform);
        BuffLayoutSetting.GetInstance().AddBuff();

        yield return new WaitForSeconds(20);
        
        player.speed = 8;
        player.atkCoolTime = 0.25f;

        Destroy(tempBuffICON);
        BuffLayoutSetting.GetInstance().SubBuff();

        DamageControler.GetInstance().DowngradeDamage();



        isSkillOn = false;
    }


    //��ų �ٲٷ��� �� �� ���� �ʱ�ȭ
    public void BeforeDEL()
    {
        if(isSkillOn == true)
        {
            player.speed = 8;
            player.atkCoolTime = 0.25f;
            Destroy(tempBuffICON);
            Destroy(tempObj);

            BuffLayoutSetting.GetInstance().SubBuff();
            DamageControler.GetInstance().DowngradeDamage();
        }
    }
}
