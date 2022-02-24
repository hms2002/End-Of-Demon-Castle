using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill_FastSword : Skill_ID
{
    Player player;
    GameObject FastSwordEffect;
    bool isSkillOn;
    GameObject tempObj;
    //#.스킬 아이콘
    GameObject buffICON;
    GameObject tempBuffICON;
    

    void Start()
    {
        player = Player.GetInstance();
        FastSwordEffect = Resources.Load<GameObject>("Prefabs/FastSword");
        coolTime = 30f;
        coolTimeSlider.maxValue = coolTime;
        //#.스킬 아이콘 및 초기화
        buffICON = Resources.Load<GameObject>("Prefabs/Buff/Buff_FastSword");
        SkillSelectManager.GetInstance().Init += BeforeDEL;
    }

    void Update()
    {
        if (skillCoolTimeStop == true)
            return;
        if (isSkillOn == false)
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
        player.atkCoolTime = 0.1f;

        tempObj = Instantiate(FastSwordEffect, player.transform);

        tempObj.transform.localPosition = new Vector2(0, 0);
        tempBuffICON = Instantiate(buffICON, BuffLayoutSetting.GetInstance().transform);
        BuffLayoutSetting.GetInstance().AddBuff();

        SoundManager.GetInstance().Play("Sound/PlayerSound/SkillSound/FastSword", 0.5f);

        Destroy(tempObj, 5);

        StartCoroutine("Off");

    }

    IEnumerator Off()
    {
        yield return new WaitForSeconds(5);
        if(isSkillOn == true)
        {
            player.atkCoolTime = 0.25f;
            isSkillOn = false;

            Destroy(tempBuffICON);
            Destroy(tempObj);
        }
    }

    //스킬 바꾸려고 할 때 강제 초기화
    public void BeforeDEL()
    {
        if (isSkillOn == true)
        {
            isSkillOn = false;
            player.speed = 8;
            player.atkCoolTime = 0.25f;
            Destroy(tempBuffICON);
            Destroy(tempObj);

            BuffLayoutSetting.GetInstance().SubBuff();
            DamageControler.GetInstance().DowngradeDamage();
        }
    }
}
