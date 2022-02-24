using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill_Thunder : Skill_ID
{
    GameObject thunder;
    GameObject tempEffect;
    

    bool isSkillOn = false;

    private void Start()
    {
        thunder = Resources.Load<GameObject>("Prefabs/Thunder");

        coolTime = 20;
        curTime = 0;

        coolTimeSlider.maxValue = coolTime;
    }

    private void Update()
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
        curTime = coolTime;
        isSkillOn = true;
        tempEffect = Instantiate(thunder, transform);
        tempEffect.transform.localPosition = new Vector3(0, 0, 0);

        StartCoroutine(Off());
    }

    IEnumerator Off()
    {
        yield return new WaitForSeconds(10);
        isSkillOn = false;
    }
}
