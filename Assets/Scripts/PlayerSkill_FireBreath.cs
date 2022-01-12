using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill_FireBreath : MonoBehaviour
{
    Player player;
    PlayerSkill skillManager;
    public GameObject fireBreath;
    Animator fireAnimator;

    float flameTimer = 0;
    public float maxSkillTime = 5f;
    bool isSkillOn;

    void Awake()
    {
        player = GetComponent<Player>();
        skillManager = GetComponent<PlayerSkill>();
        skillManager.q += FireBreath;

        isSkillOn = false;
    }

    public void FireBreath()
    {
        if (isSkillOn)
        {
            return;
        }
        isSkillOn = true;

        //불 각도
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 v2 = mousePos - (Vector2)transform.position;
        float angle = Mathf.Atan2(v2.y, v2.x) * 180 / Mathf.PI;

        GameObject fireEffect = Instantiate(fireBreath, transform.position, Quaternion.AngleAxis(angle, Vector3.forward));

        fireAnimator = fireEffect.transform.GetChild(0).GetComponent<Animator>();
        //스킬 발동
        StartCoroutine("SpitFire", fireEffect);
        player.playerConfine();
    }

    IEnumerator SpitFire(GameObject fireEffect)
    {
        Debug.Log("Spit");
        while (isSkillOn)
        {
            flameTimer += Time.deltaTime;
            //Debug.Log("타이머 : " + flameTimer);

            if (Input.GetKeyUp(KeyCode.Q) || flameTimer > maxSkillTime)
            {
                break;
            }
            yield return null;
        }

        fireAnimator.SetTrigger("fireOff");
        Destroy(fireEffect, 0.2f);
        yield return new WaitForSeconds(0.5f);
        player.playerFree();
        isSkillOn = false;
        flameTimer = 0.0f;
    }
}
