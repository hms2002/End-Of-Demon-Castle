using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill_FastSword : Skill_ID
{
    Player player;
    GameObject FastSwordEffect;

    float coolTime = 30f;
    float curTime = 0;

    void Start()
    {
        player = Player.GetInstance();
        FastSwordEffect = Resources.Load<GameObject>("Prefabs/FastSword");
        coolTimeSlider.maxValue = coolTime;
    }

    void Update()
    {
        curTime -= Time.deltaTime;
        coolTimeSlider.value = curTime;
    }

    public override void SkillOn()
    {
        if (curTime > 0)
            return;
        curTime = coolTime;
        player.atkCoolTime = 0.1f;

        GameObject tempObj = Instantiate(FastSwordEffect, player.transform);

        tempObj.transform.localPosition = new Vector2(0, 0);

        SoundManager.GetInstance().Play("Sound/PlayerSound/SkillSound/FastSword", 0.5f);

        Destroy(tempObj, 15);

        StartCoroutine("Off");

    }

    IEnumerator Off()
    {
        yield return new WaitForSeconds(15);
        player.atkCoolTime = 0.25f;
    }
}
