using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill_ShadowAssault : Skill_ID
{
    Player player;
    Rigidbody2D rigid;
    PlayerSkill playerSkill;
    GameObject shadowAssult;
    GameObject bul;

    int layerMask;
    int layerMask2;
    bool canLerp;
    

    void Start()
    {
        player = GetComponent<Player>();
        rigid = GetComponent<Rigidbody2D>();
        playerSkill = GetComponent<PlayerSkill>();
        layerMask = (1 << LayerMask.NameToLayer("Wall")) | (1 << LayerMask.NameToLayer("SkullHand"));
        shadowAssult = Resources.Load<GameObject>("Prefabs/ShadowAssultPivot");
        bul = Resources.Load<GameObject>("Prefabs/torch_side_0");

        coolTime = 7;
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
        if(curTime > 0)
        {
            return;
        }

        SoundManager.GetInstance().Play("Sound/PlayerSound/SkillSound/ShadowAssult", 0.6f);

        curTime = coolTime;
        player.playerConfine("Attack");
        player.playerConfine("Move");

        playerSkill.canSkill = false;
        StartCoroutine("ShadowAssult");
    }

    IEnumerator ShadowAssult()
    {
        Vector2 destination;
        float scale;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 v2 = mousePos - (Vector2)transform.position;
        Vector2 startPos = player.transform.position;
        float angle = Mathf.Atan2(v2.y, v2.x) * 180 / Mathf.PI;
        float len = 0.0f;
        RaycastHit2D hit = Physics2D.Raycast(transform.position - new Vector3(0,0.3f,0), v2, 7, layerMask);

        if(hit)
        {
            v2 = hit.point - (Vector2)transform.position;
            Vector2 vTemp = hit.point;

            len = Vector3.Magnitude(v2);


            Debug.Log(len);
            destination = Vector2.zero;

            if(len > 1)
            {
                destination = (startPos + v2.normalized * len) - v2.normalized * 0.5f;
                canLerp = true;
            }
            else
            {
                canLerp = false;

            }
            //if (len > 1.5f)
            //{
            //    destination = startPos + v2.normalized * len;
            //    canLerp = true;
            //}
            //else if(len > 0.8f)
            //{
            //    rigid.AddForce(startPos + v2.normalized * len * 0.5f);
            //    canLerp = false;
            //}   
            //else
            //{
            //    rigid.AddForce(startPos + v2.normalized * len * 0.5f);
            //    canLerp = false;
            //}   
            scale = Vector3.Magnitude((vTemp - (Vector2)transform.position) * 0.9f);
        }
        else
        {
            destination = startPos + v2.normalized * 7f;
            scale = 7;
            canLerp = true;
        }

        Transform assultEffect = Instantiate(shadowAssult, startPos, Quaternion.AngleAxis(angle, Vector3.forward)/*transform*/).transform;
        assultEffect.localScale = new Vector3(scale, 1, 1);
        
        if(canLerp)
        {
            for (float i = 0; i <= 100; i += 500 * Time.deltaTime)
            {
                if(i == 50 && i == 70 && i == 90)
                {
                    RaycastHit2D hitt = Physics2D.Raycast(transform.position, v2, 0.1f, layerMask);
                    if(hitt)
                        break;
                }
                player.transform.position = Vector2.Lerp(startPos, destination, i / 100.0f);
                yield return new WaitForSeconds(0.001f);
            }
        }

        //yield return new WaitForSeconds(0.2f);
        //Transform assultEffect = Instantiate(shadowAssult, startPos, Quaternion.AngleAxis(angle, Vector3.forward), transform).transform;

        //for (float i = 0; i <= 100; i += 5)
        //{
        //    assultEffect.localScale = new Vector3(Vector3.Magnitude(v2)/(101f-i), 1, 1);
        //}

        Destroy(assultEffect.gameObject, 0.3f);

        if (hit)
        {
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(hit.point, new Vector2(0.3f, 0.3f), 0);
            foreach (Collider2D collider in collider2Ds)
            {
                if (collider.CompareTag("Player"))
                {
                    BreakableObj obj = collider.GetComponent<BreakableObj>();
                    player.transform.position = Vector2.Lerp(startPos, destination, 30 / 100.0f);
                }
            }
        }

        playerSkill.canSkill = true;
        player.playerFree("Attack");
        player.playerFree("Move");
    }

}
