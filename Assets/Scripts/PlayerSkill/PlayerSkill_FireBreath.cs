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

    float coolTime = 7.0f;
    float howLongUsedSkill = 0.0f;
    float flameDuration = 0.0f;
    public float maxSkillDuration = 5.0f;
    bool isSkillOn;

    void Awake()
    {
        player = GetComponent<Player>();
        fireBreath = Resources.Load<GameObject>("Prefabs/FlamePibot");
        sound1 = Resources.Load<AudioClip>("Sound/PlayerSound/SkillSound/FireBreath");

        maxSkillDuration = 5.0f;
        isSkillOn = false;
    }

    void Update()
    {
        howLongUsedSkill -= Time.deltaTime;
    }

    public override void SkillOn()
    {
        if (isSkillOn == true || howLongUsedSkill > 0.0f)
        {
            return;
        }
        isSkillOn = true;
        howLongUsedSkill = coolTime;

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
        player.playerConfine();
    }

    IEnumerator SpitFire(GameObject fireEffect)
    {
        soundPlayer.volume = 0.8f;
        soundPlayer.Play();
        
        while (isSkillOn)
        {
            flameDuration += Time.deltaTime;

            if (Input.GetKeyUp(skillKey) || flameDuration > maxSkillDuration)
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
        flameDuration = 0.0f;
    }

    IEnumerator VolumeDown()
    {
        for (int i = 0; i < 100; i++)
        {
            soundPlayer.volume -= 0.05f;

            yield return new WaitForSeconds(0.01f);

            if (soundPlayer.volume == 0)
            {
                break;
            }
        }
    }
}
