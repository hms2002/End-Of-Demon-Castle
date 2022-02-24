using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill_HealAOE : Skill_ID
{
    GameObject HealAOEPrefabs;
    GameObject tempHealAOE;

    Player player;

    
    float healAOE_DurationTime;

    private void Start(){
        coolTime = 30f;
        HealAOEPrefabs = Resources.Load<GameObject>("Prefabs/Heal_Aoe");//0.5초 당 10 회복

        player = GetComponent<Player>();

        healAOE_DurationTime = 6;

        coolTimeSlider.maxValue = coolTime;
    }

    private void FixedUpdate()
    {
        if (skillCoolTimeStop == true)
            return;
        curTime -= Time.deltaTime;
        coolTimeSlider.value = curTime;
    }

    public override void SkillOn()
    {
        if (curTime > 0)
            return;
        curTime = coolTime;
        SoundManager.GetInstance().Play("Sound/PlayerSound/SkillSound/HealAOE", 0.4f);
        tempHealAOE = Instantiate(HealAOEPrefabs);
        tempHealAOE.GetComponent<HealAOE>().player = player;
        tempHealAOE.transform.position = player.transform.position;
        Destroy(tempHealAOE, healAOE_DurationTime);
    }
}
