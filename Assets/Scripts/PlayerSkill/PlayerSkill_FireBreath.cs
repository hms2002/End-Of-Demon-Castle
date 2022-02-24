using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//다형성을 위해 skill_ID 상속!
public class PlayerSkill_FireBreath : Skill_ID
{
    Player player;
    GameObject fireBreath;
    Animator fireAnimator;
    AudioSource soundPlayer;
    public AudioClip sound1;
    
    bool isInitSlider = false;


    float skillDuration = 0.0f;
    public float maxSkillDuration = 5.0f;
    bool isSkillOn;

    void Awake()
    {
        coolTime = 7.0f;
        player = GetComponent<Player>();
        fireBreath = Resources.Load<GameObject>("Prefabs/FlamePibot");
        sound1 = Resources.Load<AudioClip>("Sound/PlayerSound/SkillSound/FireBreath");

        isSkillOn = false;
    }

    private void Update()
    {
        if (skillCoolTimeStop == true)
            return;
        if (isSkillOn == false)
        {
            curTime -= Time.deltaTime;
            if (coolTimeSlider)
            {
                if (!isInitSlider)
                {
                    coolTimeSlider.maxValue = coolTime;
                    isInitSlider = true;
                }
                coolTimeSlider.value = curTime;
            }
        }
    }

    public override void SkillOn()
    {
        if (curTime > 0)
            return;
        if (isSkillOn)
        {
            return;
        }
        isSkillOn = true;
        curTime = coolTime;

        //불 각도
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 v2 = mousePos - (Vector2)transform.position;
        float angle = Mathf.Atan2(v2.y, v2.x) * 180 / Mathf.PI;
        GameObject fireEffect = Instantiate(fireBreath, transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
        fireAnimator = fireEffect.transform.GetChild(0).GetComponent<Animator>();
        fireEffect.transform.SetParent(transform);

        //불 사운드
        soundPlayer = fireEffect.GetComponent<AudioSource>();
        soundPlayer.clip = sound1;

        //스킬 발동
        StartCoroutine("SpitFire", fireEffect);
        player.speed = 4.5f;
        player.playerConfine("Skill");
    }

    IEnumerator SpitFire(GameObject fireEffect)
    {
        soundPlayer.volume = 0.8f;
        soundPlayer.Play();
        
        while (isSkillOn)
        {
            skillDuration += Time.deltaTime;

            if (Input.GetKeyUp(skillKey) || skillDuration > maxSkillDuration)
            {
                break;
            }
            yield return null;
        }
        StartCoroutine("VolumeDown");

        fireAnimator.SetTrigger("fireOff");
        Destroy(fireEffect, 0.35f);
        isSkillOn = false;
        yield return new WaitForSeconds(0.5f);
        player.playerFree();
        player.speed = 8.0f;
        skillDuration = 0.0f;
    }

    IEnumerator VolumeDown()
    {
        for (int i = 0; i < 100; i++)
        {
            soundPlayer.volume -= 0.05f;

            yield return new WaitForSeconds(0.01f);

            if(soundPlayer == null)
            {
                yield break;
            }
            if (soundPlayer.volume == 0)
            {
                break;
            }
        }
    }
}
