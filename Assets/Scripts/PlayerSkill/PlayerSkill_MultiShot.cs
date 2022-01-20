﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill_MultiShot : Skill_ID
{
    ObjectManager objectManager;
    GameObject skillEffect;
    GameObject bow;
    Player player;

    float arrowSpeed;
    float coolTime;
    float curTime;

    void Start()
    {
        player = GetComponent<Player>();
        objectManager = FindObjectOfType<ObjectManager>();
        skillEffect =  Resources.Load<GameObject>("Prefabs/MultiShotPivot");
        bow = Resources.Load<GameObject>("Prefabs/MultiShotBow_1");
        arrowSpeed = 15f;
        coolTime = 10;
        curTime = 0;
    }

    private void Update() {
        curTime -= Time.deltaTime;
    }

    public override void SkillOn()
    {
        if(curTime > 0)
            return;

        curTime = coolTime;
        StartCoroutine("HasDelay");
        

    }

    IEnumerator HasDelay()
    {
        player.playerConfine();

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 v2 = mousePos - (Vector2)transform.position;
        float angle = Mathf.Atan2(v2.y, v2.x) * 180 / Mathf.PI;

        GameObject effect = Instantiate(skillEffect);
        GameObject bowTemp = Instantiate(bow);

        effect.transform.position = transform.position;
        bowTemp.transform.position = transform.position;

        Quaternion v3Rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Quaternion v3RotationBow = Quaternion.AngleAxis(angle - 135f, Vector3.forward);

        effect.transform.rotation = v3Rotation;
        bowTemp.transform.rotation = v3RotationBow;
        yield return new WaitForSeconds(0.3f);

        Destroy(effect);
        Destroy(bowTemp);
        player.playerFree();

        MultiShot(angle);
    }

    void MultiShot(float angle)
    {   
        for(int i = 0; i < 7; i++)
        {
            GameObject arrow = objectManager.MakeObj("Arrow");

            Rigidbody2D arrowRigid = arrow.GetComponent<Rigidbody2D>();

            arrow.transform.position = transform.position;

            Quaternion v3Rotation = Quaternion.AngleAxis(angle + (i * 7) - (7*3), Vector3.forward);

            arrow.transform.rotation = v3Rotation;

            Vector2 dir = (v3Rotation * -Vector2.left);

            arrowRigid.AddForce(new Vector2(dir.x, dir.y) * arrowSpeed, ForceMode2D.Impulse);
        }
    }
}
