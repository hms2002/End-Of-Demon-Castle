using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill_PillarOfFire : Skill_ID
{
    public static readonly WaitForSeconds m_waitForSecondTimeDelta = new WaitForSeconds(0.005f); // Ä³½Ì

    SetCursor setCursor;
    GameObject pillarFire;
    GameObject darkFlame;

    GameObject magicCircle;

    GameObject tempCircle;

    float coolTime = 30.0f;
    float curTime = 0;

    bool OnKey = false;

    private void Start()
    {
        setCursor = Camera.main.gameObject.GetComponent<SetCursor>();
        pillarFire = Resources.Load<GameObject>("Prefabs/PillarOfFire");
        darkFlame = Resources.Load<GameObject>("Prefabs/DarkFlame");
        magicCircle = Resources.Load<GameObject>("Prefabs/SkillEffect_Pillar_Of_Fire_magicCircle");

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
        if(OnKey == false)
        {
            curTime -= Time.deltaTime;
            coolTimeSlider.value = curTime;
        }
    }

    IEnumerator Setting()
    {
        setCursor.ChangeCursor(1);
        tempCircle = Instantiate(magicCircle, transform.position, transform.rotation);
        OnKey = true;
        while(true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("H:");
                OnKey = false;
                SoundManager.GetInstance().Play("Sound/PlayerSound/SkillSound/PillarOfFire", 0.8f);
                GameObject darkTemp = Instantiate(darkFlame, Player.GetInstance().transform);
                darkFlame.transform.localPosition = new Vector3(0, 0, 0);
                break;
            }
            tempCircle.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0,0,10);
            yield return m_waitForSecondTimeDelta;

        }

        setCursor.ChangeCursor("base");
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Destroy(tempCircle);

        Instantiate(pillarFire, (Vector3)mousePos, Quaternion.Euler(0, 0, 0));
    }
}
