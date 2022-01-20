using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill_SniperArrow : Skill_ID
{
    Player player;
    GameObject SniperArrow;
    GameObject SniperCharge;
    float arrowSpeed;

    float coolTime;
    float curTime;

    void Start()
    {
        player = GetComponent<Player>();
        SniperArrow = Resources.Load<GameObject>("Prefabs/SniperArrow");
        SniperCharge = Resources.Load<GameObject>("Prefabs/SniperCharge");
        arrowSpeed = 15f;
        coolTime = 10;
        curTime = 0;
    }

    private void Update()
    {
        curTime -= Time.deltaTime;
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
        player.playerConfine();
        yield return new WaitForSeconds(0.5f);
        Destroy(Charge);
        GameObject arrow = Instantiate(SniperArrow, transform.position, Quaternion.AngleAxis(angle + 90, Vector3.forward));
        Rigidbody2D arrowRigid = arrow.GetComponent<Rigidbody2D>();

        arrow.transform.position = transform.position;
        Vector2 dir = v2.normalized;
        
        player.playerFree();
        arrowRigid.AddForce(new Vector2(dir.x, dir.y) * arrowSpeed, ForceMode2D.Impulse);
    }
}
