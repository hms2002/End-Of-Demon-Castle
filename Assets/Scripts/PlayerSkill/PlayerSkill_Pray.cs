using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill_Pray : Skill_ID
{
    GameObject pray;

    float skillDuration;
    float maxSkillDuration;
    bool isSkillOn;

    private void Awake()
    {
        pray = Resources.Load<GameObject>("Prefabs/Pray"); //스프라이트 생기면 추가하셈
    }

    public override void SkillOn()
    {
        if (isSkillOn)
        {
            return;
        }
        isSkillOn = true;

        ActivePray();
    }

    void ActivePray()
    {
        GameObject prayEffect = Instantiate(pray);

        Destroy(prayEffect);
    }

}
