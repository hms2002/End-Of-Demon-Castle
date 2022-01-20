using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill_Venom : Skill_ID
{
    Player player;
    GameObject VenomBottle;

    public float cooltime = 5f;
    public float curtime = 0f;
    public float BottleSpeed = 10f;
    public Vector2 playerDir;
    void Start()
    {
        player = GetComponent<Player>();
        VenomBottle = Resources.Load<GameObject>("Prefabs/VenomBottle");
    }

    void Update()
    {
        curtime -= Time.deltaTime;      
    }

    public override void SkillOn()
    {
        //스킬 발동
        if (curtime < 0)
        {
            curtime = cooltime;
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 v2 = mousePos - (Vector2)transform.position;
            float angle = Mathf.Atan2(v2.y, v2.x) * 180 / Mathf.PI;
            GameObject venomBottle = Instantiate(VenomBottle, player.transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
            Rigidbody2D BottleRig = venomBottle.GetComponent<Rigidbody2D>();
            playerDir = player.transform.position;
            
            Vector2 dir = v2.normalized;
            BottleRig.AddForce(new Vector2(dir.x, dir.y) * BottleSpeed, ForceMode2D.Impulse);
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
