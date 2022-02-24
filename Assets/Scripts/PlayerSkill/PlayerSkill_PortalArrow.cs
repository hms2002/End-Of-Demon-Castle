using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill_PortalArrow : Skill_ID
{
    GameObject portalArrow;
    
    
    void Start()
    {
        coolTime = 20;
        portalArrow = Resources.Load<GameObject>("Prefabs/PortalArrow");
        coolTimeSlider.maxValue = coolTime;
    }

    private void Update()
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
        Debug.Log("쿨타임 : " + coolTime);
        Debug.Log("현재 : " + curTime);
        curTime = coolTime;
        SoundManager.GetInstance().Play("Sound/PlayerSound/SkillSound/OpenPortal", 0.5f,Define.Sound.Effect, 0.7f);

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 v2 = mousePos - (Vector2)transform.position;
        float angle = Mathf.Atan2(v2.y, v2.x) * 180 / Mathf.PI;

        if (angle < 0) angle += 360;
        if (angle <= 22.5f || angle > 337.5f) //오른쪽을 바라봅니다.0
        {
            GameObject atkEffect = Instantiate(portalArrow, transform.position + new Vector3(0.4f, 0, 0), Quaternion.Euler(0, 45, 0));
            atkEffect.GetComponent<Animator>().SetTrigger("Left");
            atkEffect.GetComponent<SpriteRenderer>().flipX = true;
            atkEffect.GetComponent<SkillAct_PortalArrow>().Shooting(0);
        }
        else if (22.5f <= angle && angle < 67.5f) //오른쪽위를 바라봅니다.45
        {
            GameObject atkEffect = Instantiate(portalArrow, transform.position + new Vector3(0.2f, 0.2f, 0), Quaternion.Euler(0, 45, 45));
            atkEffect.GetComponent<Animator>().SetTrigger("Left");
            atkEffect.GetComponent<SpriteRenderer>().flipX = true;
            atkEffect.GetComponent<SkillAct_PortalArrow>().Shooting(45);
        }
        else if (67.5f <= angle && angle < 112.5f) //위를 바라봅니다.90
        {
            GameObject atkEffect = Instantiate(portalArrow, transform.position + new Vector3(0, 0.4f, 0), Quaternion.Euler(68, 0, 0));
            atkEffect.GetComponent<Animator>().SetTrigger("Up");
            atkEffect.GetComponent<SkillAct_PortalArrow>().Shooting(90);
        }
        else if (112.5f <= angle && angle < 157.5f) //왼쪽위를 바라봅니다.135
        {
            GameObject atkEffect = Instantiate(portalArrow, transform.position + new Vector3(-0.2f, 0.2f, 0), Quaternion.Euler(0, 45, 135));
            atkEffect.GetComponent<Animator>().SetTrigger("Left");
            atkEffect.GetComponent<SpriteRenderer>().flipX = true;
            atkEffect.GetComponent<SkillAct_PortalArrow>().Shooting(135);
        }
        else if (157.5f <= angle && angle < 202.5f) //왼쪽을 바라봅니다.180
        {
            GameObject atkEffect = Instantiate(portalArrow, transform.position + new Vector3(-0.4f, 0, 0), Quaternion.Euler(0, 45, 0));
            atkEffect.GetComponent<Animator>().SetTrigger("Left");
            atkEffect.GetComponent<SkillAct_PortalArrow>().Shooting(180);
        }
        else if (202.5f <= angle && angle < 247.5f) //왼쪽아래를 바라봅니다.225
        {
            GameObject atkEffect = Instantiate(portalArrow, transform.position + new Vector3(-0.2f, -0.2f, 0), Quaternion.Euler(0, 45, 225));
            atkEffect.GetComponent<Animator>().SetTrigger("Left");
            atkEffect.GetComponent<SpriteRenderer>().flipX = true;
            atkEffect.GetComponent<SkillAct_PortalArrow>().Shooting(225);
        }
        else if (247.5f <= angle && angle < 292.5f) //아래를 바라봅니다.270
        {
            GameObject atkEffect = Instantiate(portalArrow, transform.position + new Vector3(0, -0.4f, 0), Quaternion.Euler(68, 0, 0));
            atkEffect.GetComponent<Animator>().SetTrigger("Up");
            atkEffect.GetComponent<SpriteRenderer>().flipY = true;
            atkEffect.GetComponent<SkillAct_PortalArrow>().Shooting(270);
        }
        else if (292.5f <= angle && angle < 337.5f) //오른쪽아래를 바라봅니다.315
        {
            GameObject atkEffect = Instantiate(portalArrow, transform.position + new Vector3(0.2f, -0.2f, 0), Quaternion.Euler(0, 45, 315));
            atkEffect.GetComponent<Animator>().SetTrigger("Left");
            atkEffect.GetComponent<SpriteRenderer>().flipX = true;
            atkEffect.GetComponent<SkillAct_PortalArrow>().Shooting(315);
        }
    }
}
