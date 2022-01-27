using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill_ShadowAssault : Skill_ID
{
    Player player;
    PlayerSkill playerSkill;
    GameObject shadowAssult;

    int layerMask;

    void Start()
    {
        player = GetComponent<Player>();
        playerSkill = GetComponent<PlayerSkill>();
        layerMask = 1 << LayerMask.NameToLayer("Wall");
        shadowAssult = Resources.Load<GameObject>("Prefabs/ShadowAssultPivot");
    }

    public override void SkillOn()
    {
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

        RaycastHit2D hit = Physics2D.Raycast(transform.position, v2, 7, layerMask);

        if(hit)
        {
            Debug.Log("AHHHHA");
            v2 = hit.point;
            destination = v2 * 0.9f;
            scale = Vector3.Magnitude(         (v2 - (Vector2)transform.position) * 0.9f    );
        }
        else
        {
            destination = startPos + v2.normalized * 7f;
            scale = 7;
        }

        Transform assultEffect = Instantiate(shadowAssult, startPos, Quaternion.AngleAxis(angle, Vector3.forward)/*transform*/).transform;
        assultEffect.localScale = new Vector3(scale, 1, 1);
        
        for (float i = 0; i <= 100; i+= 5)
        {
            //assultEffect.localScale = new Vector3(Vector3.Magnitude(v2)/(101f-i), 1, 1);
            player.transform.position = Vector2.Lerp(startPos, destination, i / 100.0f);
            yield return new WaitForSeconds(0.001f);
        }

        //yield return new WaitForSeconds(0.2f);
        //Transform assultEffect = Instantiate(shadowAssult, startPos, Quaternion.AngleAxis(angle, Vector3.forward), transform).transform;

        //for (float i = 0; i <= 100; i += 5)
        //{
        //    assultEffect.localScale = new Vector3(Vector3.Magnitude(v2)/(101f-i), 1, 1);
        //}

        Destroy(assultEffect.gameObject, 0.3f);

        playerSkill.canSkill = true;
        player.playerFree("Attack");
        player.playerFree("Move");
    }
}
