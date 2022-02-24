using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill_SniperArrow : Skill_ID
{
    Player player;
    GameObject SniperArrow;
    GameObject SniperCharge;
    GameObject SniperChargeShot;
    GameObject bow;
    float arrowSpeed;
    

    void Start()
    {
        player = GetComponent<Player>();
        SniperArrow = Resources.Load<GameObject>("Prefabs/SniperArrow");
        SniperCharge = Resources.Load<GameObject>("Prefabs/SniperChargePivot");
        SniperChargeShot = Resources.Load<GameObject>("Prefabs/SniperChargeShotPivot");
        bow = Resources.Load<GameObject>("Prefabs/MultiShotBow_1");
        arrowSpeed = 25f;
        coolTime = 10;
        curTime = 0;

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
        curTime = coolTime;
        //커서 각도
        
        StartCoroutine("SniperShot");
    }

    private IEnumerator SniperShot()
    {   
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 v2 = mousePos - (Vector2)transform.position;
        float angle = Mathf.Atan2(v2.y, v2.x) * 180 / Mathf.PI;
        GameObject Charge = Instantiate(SniperCharge, player.transform.position, transform.rotation);
        SoundManager.GetInstance().Play("Sound/PlayerSound/SkillSound/SniperCharge", 0.1f);
        GameObject bowTemp = Instantiate(bow);
        bowTemp.transform.position = transform.position;
        Quaternion v3RotationBow = Quaternion.AngleAxis(angle - 135f, Vector3.forward);
        bowTemp.transform.rotation = v3RotationBow;
        SoundManager.GetInstance().Play("Sound/PlayerSound/SkillSound/MultiShotChargeSound", 15);
        player.playerConfine();
        yield return new WaitForSeconds(0.5f);
        Destroy(Charge);
        SoundManager.GetInstance().Play("Sound/PlayerSound/SkillSound/MultiShotSound",0.5f);
        GameObject arrow = Instantiate(SniperArrow, transform.position, Quaternion.AngleAxis(angle - 90, Vector3.forward));
        GameObject ChargeShot = Instantiate(SniperChargeShot, transform.position, Quaternion.AngleAxis(angle - 90, Vector3.forward));
        Destroy(ChargeShot, 0.104f);
        Rigidbody2D arrowRigid = arrow.GetComponent<Rigidbody2D>();

        arrow.transform.position = transform.position;
        Vector2 dir = v2.normalized;
        
        player.playerFree();
        Destroy(bowTemp);
        arrowRigid.AddForce(new Vector2(dir.x, dir.y) * arrowSpeed, ForceMode2D.Impulse);
    }
}
