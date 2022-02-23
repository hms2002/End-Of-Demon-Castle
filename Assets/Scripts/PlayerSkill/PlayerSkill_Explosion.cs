using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkill_Explosion : Skill_ID
{
    Player player;
    GameObject Explosion;
    Text text;

    public float cooltime = 0.5f;
    public float curtime = 0f;
    public float Chargetime = 2f;
    public const int ChargeMax = 5;
    public int ChargeNum = 5;
    bool isSkillOn;
    bool isOnce = true;

    void Awake()
    {
        player = GetComponent<Player>();
        Explosion = Resources.Load<GameObject>("Prefabs/ExplosionPivot");
        isSkillOn = false;
    }

    void Update()
    {
        curtime -= Time.deltaTime;
        if (coolTimeSlider != null)
        {
            if (isOnce)
            {
                isOnce = false;
                coolTimeSlider.maxValue = cooltime;
                text = coolTimeSlider.transform.parent.GetChild(1).GetComponent<Text>();
                text.text = ChargeNum.ToString();
            }
            coolTimeSlider.value = curtime;
            
        }
            
        if (ChargeNum == ChargeMax)
        {
            StopCoroutine("Charge");
        }
    }
    public override void SkillOn()
    {
        //스킬 발동
        if (curtime < 0 && ChargeNum > 0)
        {
            if (isSkillOn)
            {
                return;
            }
            isSkillOn = true;
            curtime = cooltime;
            //불 각도
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 v2 = mousePos - (Vector2)transform.position;
            float angle = Mathf.Atan2(v2.y, v2.x) * 180 / Mathf.PI;
            GameObject ExplosionEffect = Instantiate(Explosion, transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
            SoundManager.GetInstance().Play("Sound/PlayerSound/SkillSound/Explosion", 0.3f);
            StartCoroutine("SpitFire", ExplosionEffect);
            if(ChargeNum == ChargeMax)
            {
                StartCoroutine("Charge");
            }
            ChargeNum -= 1;
            text.text = ChargeNum.ToString();
            
            player.playerConfine();
        }
    }

    IEnumerator SpitFire(GameObject ExplosionEffect)
    {
        Destroy(ExplosionEffect, 0.2f);
        yield return new WaitForSeconds(0.2f);
        player.playerFree();
        isSkillOn = false;
    }

    IEnumerator Charge()
    {
        yield return new WaitForSeconds(Chargetime);


        if (ChargeNum < ChargeMax)
        {
            ChargeNum += 1;
            text.text = ChargeNum.ToString();
            Debug.Log(ChargeNum);
            StartCoroutine("Charge");
        }
    }
}
