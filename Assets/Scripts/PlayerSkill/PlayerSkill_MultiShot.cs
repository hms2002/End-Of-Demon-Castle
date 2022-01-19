using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill_MultiShot : Skill_ID
{
    ObjectManager objectManager;
    float arrowSpeed;

    float coolTime;
    float curTime;

    void Start()
    {
        objectManager = FindObjectOfType<ObjectManager>();
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
        //커서 각도
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 v2 = mousePos - (Vector2)transform.position;
        float angle = Mathf.Atan2(v2.y, v2.x) * 180 / Mathf.PI;
        
        for(int i = 0; i < 7; i++)
        {
            GameObject arrow = objectManager.MakeObj("Arrow");
            Rigidbody2D arrowRigid = arrow.GetComponent<Rigidbody2D>();

            arrow.transform.position = transform.position;

            Quaternion v3Rotation = Quaternion.AngleAxis(angle + (i * 7) - (7*3), Vector3.forward);

            arrow.transform.rotation = v3Rotation;

            Vector2 dir = (v3Rotation * -Vector2.left);

            Debug.Log(dir);
            arrowRigid.AddForce(new Vector2(dir.x, dir.y) * arrowSpeed, ForceMode2D.Impulse);
        }
    }
}
