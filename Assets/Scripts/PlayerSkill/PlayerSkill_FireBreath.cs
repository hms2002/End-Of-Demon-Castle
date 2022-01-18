using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//다형성을 위해 skill_ID 상속!
public class PlayerSkill_FireBreath : Skill_ID
{
    Player player;
    PlayerSkill skillManager;
    GameObject fireBreath;
    Animator fireAnimator;

    float flameTimer = 0;
    public float maxSkillTime = 5f;
    bool isSkillOn;

    void Awake()
    {
        player = GetComponent<Player>();
        skillManager = GetComponent<PlayerSkill>();
        fireBreath = Resources.Load<GameObject>("Prefabs/FlamePibot");

        maxSkillTime = 5f;
        isSkillOn = false;
    }

    public override void SkillOn()
    {
        if (isSkillOn)
        {
            return;
        }
        isSkillOn = true;

        //불 각도
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 v2 = mousePos - (Vector2)transform.position;
        float angle = Mathf.Atan2(v2.y, v2.x) * 180 / Mathf.PI;

        GameObject fireEffect = Instantiate(fireBreath, transform.position, Quaternion.AngleAxis(angle, Vector3.forward));

        fireAnimator = fireEffect.transform.GetChild(0).GetComponent<Animator>();
        fireEffect.transform.SetParent(transform);
        
        //스킬 발동
        StartCoroutine("SpitFire", fireEffect);
        player.playerConfine();
    }

    IEnumerator SpitFire(GameObject fireEffect)
    {
        while (isSkillOn)
        {
            flameTimer += Time.deltaTime;
            //Debug.Log("타이머 : " + flameTimer);

            //KeyCode.Q => skillKey로 변경함
            if (Input.GetKeyUp(skillKey) || flameTimer > maxSkillTime)
            {
                break;
            }
            yield return null;
        }

        fireAnimator.SetTrigger("fireOff");
        Destroy(fireEffect, 0.2f);
        yield return new WaitForSeconds(0.5f);
        player.playerFree();
        isSkillOn = false;
        flameTimer = 0.0f;
    }
}
