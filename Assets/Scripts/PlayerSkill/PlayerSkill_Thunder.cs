using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill_Thunder : Skill_ID
{
    GameObject thunder;

    float coolTime;
    float curTime;

    private void Start()
    {
        thunder = Resources.Load<GameObject>("Prefabs/Thunder");

        coolTime = 20;
        curTime = 0;

        coolTimeSlider.maxValue = coolTime;
    }

    private void Update()
    {
        curTime -= Time.deltaTime;
        coolTimeSlider.value = curTime;
    }

    public override void SkillOn()
    {
        if (curTime > 0)
            return;
        curTime = coolTime;

        GameObject effect = Instantiate(thunder, transform);
        effect.transform.localPosition = new Vector3(0, 0, 0);
    }
}
