using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill_Venom : Skill_ID
{
    Player player;
    GameObject VenomBottle;
    
    public float BottleSpeed = 10f;
    public Vector2 playerDir;

    void Start()
    {
        coolTime = 15f;
        player = GetComponent<Player>();
        VenomBottle = Resources.Load<GameObject>("Prefabs/VenomBottle");
        coolTimeSlider.maxValue = coolTime;
    }

    void Update()
    {
        if (skillCoolTimeStop == true)
            return;
        curTime -= Time.deltaTime;
        coolTimeSlider.value = curTime;
    }

    public override void SkillOn()
    {
        //스킬 발동
        if (curTime < 0)
        {
            curTime = coolTime;
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 v2 = mousePos - (Vector2)transform.position;
            float angle = Mathf.Atan2(v2.y, v2.x) * 180 / Mathf.PI;
            GameObject venomBottle = Instantiate(VenomBottle, player.transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
            Rigidbody2D BottleRig = venomBottle.GetComponent<Rigidbody2D>();
            playerDir = player.transform.position;
            
            Vector2 dir = v2.normalized;
            SoundManager.GetInstance().Play("Sound/PlayerSound/SkillSound/PoisonBottleWhoosh", 3f);
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
