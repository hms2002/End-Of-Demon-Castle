using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill_Berserk : Skill_ID
{
    Player player;
    GameObject berserkEffect;

    float coolTime = 60;
    float curTime = 0;

    private void Start()
    {
        player = Player.GetInstance();
        berserkEffect = Resources.Load<GameObject>("Prefabs/Berserk_Pivot");
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
        player.speed = 10;
        player.atkCoolTime = 0.1f;
        player.damaged(20);

        GameObject tempObj = Instantiate(berserkEffect, player.transform);

        tempObj.transform.localPosition = new Vector2(0, 0);

        Destroy(tempObj, 20);

        DamageControler.GetInstance().UpgradeDamage();
    }

    IEnumerator OnBerserk()
    {
        yield return new WaitForSeconds(20);
        player.speed = 8;
        player.atkCoolTime = 0.25f;

        DamageControler.GetInstance().DowngradeDamage();
    }
}
