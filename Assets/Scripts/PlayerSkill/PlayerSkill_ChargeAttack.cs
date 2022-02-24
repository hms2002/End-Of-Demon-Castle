using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill_ChargeAttack : Skill_ID
{
    GameObject chargeEffect;
    GameObject AtkEffect;
    GameObject chargeTemp;
    GameObject atkTemp;

    Player player;

    int cnt = 0;
    float chargeTime;
    bool OnCharge = false;

    private void Update()
    {

        Debug.Log("Y");
        if (OnCharge)
        {
            chargeTime += Time.deltaTime;

            if (chargeTime > 1)
            {
                chargeTime = 0;
                cnt += 1;
            }
        }
        else
        {
            curTime -= Time.deltaTime;
            coolTimeSlider.value = curTime;
            Debug.Log("J");
        }
        if (chargeTemp != null)
            chargeTemp.transform.localPosition = Vector3.zero;
    }

    private void Start()
    {
        coolTime = 10;
        curTime = 0;
        coolTimeSlider.maxValue = coolTime;

        player = GetComponent<Player>();
        chargeEffect = Resources.Load<GameObject>("Prefabs/Charge_Pivot");
        AtkEffect = Resources.Load<GameObject>("Prefabs/ChargeAttackPivot");
    }

    public override void SkillOn()
    {
        if (curTime > 0)
            return;
        Debug.Log("H");
        curTime = coolTime;
        cnt = 0;
        chargeTime = 0;
        OnCharge = true;
        player.playerConfine("Attack");
        player.playerConfine("Skill");

        StartCoroutine(Charge());
    }



    IEnumerator Charge()
    {
        chargeTemp = Instantiate(chargeEffect, transform);
        Animator anim = chargeTemp.transform.GetChild(0).GetComponent<Animator>();

        while (true)
        {
            if(Input.GetKeyUp(skillKey))
            {
                break;
            }
            yield return null;
        }


        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 v2 = mousePos - (Vector2)transform.position;
        float angle = Mathf.Atan2(v2.y, v2.x) * 180 / Mathf.PI;
        if (angle < 0) angle += 360;




        player.playerConfine("Move");

        atkTemp = Instantiate(AtkEffect, transform);
        atkTemp.transform.localPosition = Vector3.zero;
        atkTemp.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        SoundManager.GetInstance().Play("Sound/PlayerSound/SkillSound/ChargeSlash");

        if (cnt == 0)
        {
            atkTemp.transform.localScale = new Vector3(1, 1, 0);
            DamageControler.GetInstance().SetChargeDamage(0);
        }
        else if(cnt == 1)
        {
            atkTemp.transform.localScale = new Vector3(2, 2, 0);
            DamageControler.GetInstance().SetChargeDamage(1);
        }
        else
        {
            atkTemp.transform.localScale = new Vector3(3, 3, 0);
            DamageControler.GetInstance().SetChargeDamage(2);
        }

        Destroy(atkTemp,0.25f);
        Destroy(chargeTemp);

        OnCharge = false;
        player.playerFree("Move");
        player.playerFree("Attack");
        player.playerFree("Skill");
    }
}
