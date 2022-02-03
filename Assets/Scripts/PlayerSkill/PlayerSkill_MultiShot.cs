using System.Collections;
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
        bow = Resources.Load<GameObject>("Prefabs/MultiShotBow_1");
        arrowSpeed = 15f;
        coolTime = 0.5f;
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
        
        GameObject bowTemp = Instantiate(bow);
        
        bowTemp.transform.position = transform.position;

        Quaternion v3Rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Quaternion v3RotationBow = Quaternion.AngleAxis(angle - 135f, Vector3.forward);
        
        bowTemp.transform.rotation = v3RotationBow;

        SoundManager.GetInstance().Play("Sound/PlayerSound/SkillSound/MultiShotChargeSound",15);

        yield return new WaitForSeconds(0.3f);
        
        Destroy(bowTemp);
        player.playerFree();

        MultiShot(angle);
    }

    void MultiShot(float angle)
    {
        
        SoundManager.GetInstance().Play("Sound/PlayerSound/SkillSound/MultiShotSound",0.5f);
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
