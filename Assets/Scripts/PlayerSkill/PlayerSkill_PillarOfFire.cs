using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill_PillarOfFire : Skill_ID
{
    public static readonly WaitForSeconds m_waitForSecondTimeDelta = new WaitForSeconds(0.01f); // ĳ??

    SetCursor setCursor;
    GameObject pillarFire;
    GameObject darkFlame;

    float coolTime = 10f;
    float curTime = 0;

    private void Start()
    {
        setCursor = Camera.main.gameObject.GetComponent<SetCursor>();
        pillarFire = Resources.Load<GameObject>("Prefabs/PillarOfFire");
        darkFlame = Resources.Load<GameObject>("Prefabs/DarkFlame");

        coolTimeSlider.maxValue = coolTime;
    }

    public override void SkillOn()
    {
        if (curTime > 0)
            return;
        curTime = coolTime;
        StartCoroutine("Setting");
    }

    private void Update()
    {
        curTime -= Time.deltaTime;
        coolTimeSlider.value = curTime;
    }

    IEnumerator Setting()
    {
        setCursor.ChangeCursor("magicCircle");
        
        while(true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SoundManager.GetInstance().Play("Sound/PlayerSound/SkillSound/PillarOfFire", 0.8f);
                GameObject darkTemp = Instantiate(darkFlame, Player.GetInstance().transform);
                darkFlame.transform.localPosition = new Vector3(0, 0, 0);
                break;
            }

            yield return m_waitForSecondTimeDelta;

        }

        setCursor.ChangeCursor("base");
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Instantiate(pillarFire, (Vector3)mousePos, Quaternion.Euler(0, 0, 0));
    }
}
