using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill_BackSteb : Skill_ID
{
    //커서 바꾸기
    SetCursor setCursor;
    //
    Player player;

    //백스탭 이펙트
    GameObject backSteb;
    //쿨타임
    float coolTime;
    float curTime;

    //다른 스킬 동시에 못쓰도록 플레이어 스킬 가져옴
    PlayerSkill playerSkill;
    //rayCast2D
    float MaxDistance;
    Vector3 mousePos;
    Camera cameraMain;
    bool isFirst;

    void Start()
    {
        isFirst = true;
        MaxDistance = 15f;
        cameraMain = Camera.main;

        player = GetComponent<Player>();
        playerSkill = GetComponent<PlayerSkill>();
         
        setCursor = Camera.main.gameObject.GetComponent<SetCursor>();

        backSteb = Resources.Load<GameObject>("Prefabs/BackSteb");
        
        coolTime = 10f;
        curTime = 0;

        coolTimeSlider.maxValue = coolTime;
    }

    private void Update() {
        curTime -= Time.deltaTime;
        coolTimeSlider.value = curTime;
    }

    public override void SkillOn()
    {
        if(curTime > 0)
            return;

        curTime = coolTime;
        playerSkill.canSkill = false;
        setCursor.ChangeCursor("backSteb");
        player.playerConfine("Attack");

        StartCoroutine("BackSteb");
    }

    IEnumerator BackSteb()
    {
        while(true)
        {
            if(Input.GetMouseButtonDown(0))
            {
                mousePos = cameraMain.ScreenToWorldPoint(Input.mousePosition);

                RaycastHit2D hit = Physics2D.Raycast(mousePos, transform.forward, MaxDistance);
                
                if(hit)
                {
                    if(hit.transform.CompareTag("CanBroke") || hit.transform.CompareTag("Boss"))
                    {
                        float distance = Mathf.Abs(Vector2.Distance(transform.position, hit.transform.position));
                        if(distance <= 7)
                        {
                            GameObject effect = Instantiate(backSteb, transform.position, transform.rotation);
                            AudioSource effectAudio = effect.GetComponent<AudioSource>();
                            Destroy(effect, 0.4f);

                            SoundManager.GetInstance().Play(effectAudio, "Sound/PlayerSound/SkillSound/BackStepSound");

                            transform.position = hit.transform.CompareTag("Boss") ? hit.transform.position + (hit.transform.up * 1) : hit.transform.position + (hit.transform.up * 0.5f);
                            break;
                        }
                    }
                }
            }

            if(Input.GetKeyDown(skillKey))
            {
                if(isFirst)
                {
                    yield return new WaitForSeconds(0.1f);
                    isFirst = false;
                    continue;
                }
                break;
            }
                

            yield return new WaitForSeconds(Time.deltaTime);
        }

        player.playerFree("Attack");
        player.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        setCursor.ChangeCursor("base");
        isFirst = true;
        playerSkill.canSkill = true;
    }
}
