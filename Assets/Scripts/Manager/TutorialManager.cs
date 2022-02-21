using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    private static TutorialManager tutorialManager;
    
    public bool start = true;
    public bool isFirstMissionClear = false;
    public bool isSecondMissionClear1 = false;
    public bool isSecondMissionClear2 = false;
    public bool isMe = false;
    EnterTheBossRoom bossRoom;
    public static TutorialManager GetInstance()
    {
        if(tutorialManager == null)
        {
            tutorialManager = FindObjectOfType<TutorialManager>();
        }
        return tutorialManager;
    }

    private void Awake()
    {
        GetInstance();
    }
    private void Start()
    {
        bossRoom = FindObjectOfType<EnterTheBossRoom>();

        GetInstance().StartCoroutine(StartTutorial());
        if (tutorialManager != null)
        {
            if (tutorialManager != this)
            {
                Destroy(gameObject);
            }
            else
            {
                isMe = true;
            }
        }
        DontDestroyOnLoad(gameObject); 
    }


    void SceneSetting(Scene scene, LoadSceneMode mode)
    {
    }
    public void StertTutorial2()
    {
        StartCoroutine(IStartTutorial2());
    }

    IEnumerator StartTutorial()
    {
        SoundManager.GetInstance().Play("Sound/BGM/BGM_FrontOf_BossRoomDoor", 0.5f, Define.Sound.Bgm, 1f);
        yield return new WaitForSeconds(0.01f);
        if (GetInstance().start)
        {
            //게임 처음 시작할 때 첵스트 출력하기
            GetInstance().start = false;

            bossRoom.GetComponent<CapsuleCollider2D>().enabled = false;

            Player.GetInstance().playerConfine();
            Player.GetInstance().playerConfine("Skill");

            TextManager.GetInstance().TutorialTextOn(0);
        }
        else if (GetInstance().isFirstMissionClear == false)
        {
            //죽고 다시 시작할 때 오른쪽 위에 텍스트 뛰우기

            bossRoom.GetComponent<CapsuleCollider2D>().enabled = false;

            TextManager.GetInstance().TutorialTextOn(6);
        }
        else if(GetInstance().isSecondMissionClear1 == false)
        {
            //죽고 다시 시작할 때 오른쪽 위에 텍스트 뛰우기

            bossRoom.GetComponent<CapsuleCollider2D>().enabled = false;

            TextManager.GetInstance().TutorialTextOn(9);
        }
        else if (GetInstance().isSecondMissionClear2 == false)
        {
            //죽고 다시 시작할 때 오른쪽 위에 텍스트 뛰우기
            GetInstance().isSecondMissionClear1 = true;

            bossRoom.GetComponent<CapsuleCollider2D>().enabled = false;

            TextManager.GetInstance().TutorialTextOn(6);
        }
        else
        {
            bossRoom.GetComponent<CapsuleCollider2D>().enabled = true;
            yield break;
        }
        Debug.Log("d : " + GetInstance().isFirstMissionClear);

    }

    IEnumerator IStartTutorial2()
    {
        yield return new WaitForSeconds(0.01f);
        Player.GetInstance().playerConfine();
        Player.GetInstance().playerConfine("Skill");
        TextManager.GetInstance().TutorialTextOn(7);
    }
}
