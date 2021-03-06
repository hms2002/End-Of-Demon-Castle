using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill_Cross_Attack : Skill_ID
{
    Player player;
    GameObject Cross;
    public float cooltime = 1f;
    public float curtime = 0f;
    bool isInitSlider = false;

    void Awake()
    {
        Cross = Resources.Load<GameObject>("Prefabs/Cross_Attack_Pivot");
        player = Player.GetInstance();
    }

    void Update()
    { 
        curtime -= Time.deltaTime;
        if (coolTimeSlider != null)
        {
            if (coolTimeSlider)
            {
                if (!isInitSlider)
                {
                    coolTimeSlider.maxValue = cooltime;
                    isInitSlider = true;
                }
               
                coolTimeSlider.value = curtime;
            }
        }
    }

    public override void SkillOn()
    {
        //??ų ?ߵ?
        if (curtime < 0)
        {
            curtime = cooltime;
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 v2 = mousePos - (Vector2)transform.position;
            Vector2 temp = v2.normalized * 3;
            float angle = Mathf.Atan2(v2.y, v2.x) * 180 / Mathf.PI;
            Instantiate(Cross, (Vector3)temp + transform.position, transform.rotation);
            SoundManager.GetInstance().Play("Sound/PlayerSound/SkillSound/CrossAttack", 0.1f);
            player.playerConfine();
            StartCoroutine("delay");
        }
    }

    public IEnumerator delay()
    {
        yield return new WaitForSeconds(0.2f);
        player.playerFree();
    }
}
