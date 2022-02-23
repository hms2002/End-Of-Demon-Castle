using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill_HealAOE : Skill_ID
{
    GameObject HealAOEPrefabs;
    GameObject tempHealAOE;

    Player player;


    float skillCoolTime = 30f;
    float curTime = 0;
    float healAOE_DurationTime;

    private void Start(){
        HealAOEPrefabs = Resources.Load<GameObject>("Prefabs/Heal_Aoe");//0.5초 당 10 회복

        player = GetComponent<Player>();

        healAOE_DurationTime = 6;

        coolTimeSlider.maxValue = skillCoolTime;
    }

    private void FixedUpdate()
    {
        curTime -= Time.deltaTime;
        coolTimeSlider.value = curTime;
    }

    public override void SkillOn()
    {
        if (curTime > 0)
            return;
        curTime = skillCoolTime;
        SoundManager.GetInstance().Play("Sound/PlayerSound/SkillSound/HealAOE", 0.4f);
        tempHealAOE = Instantiate(HealAOEPrefabs);
        tempHealAOE.GetComponent<HealAOE>().player = player;
        tempHealAOE.transform.position = player.transform.position;
        Destroy(tempHealAOE, healAOE_DurationTime);
    }
}
