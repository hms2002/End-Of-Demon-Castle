using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    //private SizeCtrl sizeCtrl;
    public bool isTextOn = false; //bool 값에 따라 TextClose 실행 여부 결정
    public Text text;
    public Text Endingtext;
    public Text textName;
    public Text textQuest;
    public Image redBox;
    public Image redBox2;
    public Button backButton;
    RectTransform rect;
    Vector3 downPos;
    Vector3 upPos;
    public float delay;//텍스트 출력 속도

    bool isBoxUp = false;

    public static TextManager textManager;

    public static TextManager GetInstance()
    {
        if (textManager == null)
        {
            textManager = FindObjectOfType<TextManager>();
        }

        return textManager;
    }


    #region ScenarioField
    private string[] bossDoorScenario = {
        "드디어 왔구나 용사여...",
        " ",
        "설마 혼자 온것인가?",
        "참으로 대단하군. 그 실력 인정해주마.",
        " ",
        "그럼 어디 한번 놀아볼까?",//4
        " "//6
    };

    private string[] bossPhase2Scenario =
    {
        " ",
        "오호... 네 녀석 꽤나 강력하구나...",
        " ",
        "그럼... 이 몸이 살짝 움직여볼까?",
        " "
    };
    private string[] bossPhase3Scenario =
    {
        " ",
        "크윽...! 꽤나 강력하군.",
        "좋다. 이번엔 진심을 발휘해주지",
        " "
    };

    private string[] bossDeadScenario =
    {
        " ",
        "이럴... 수가_!",
        "하지만... 이번이 끝은 아닐거다...!",
        " "
    };
    private string[] tutorialScenario=
    {
        "드디어 최상부... 이 앞이 마왕의 방이군.",
        "마지막이라고 해서 방심하지 말자. 이럴 때 일수록 기초가 중요한 법이야.",
        "조작은 W(↑), A(←), S(↓), D(↑), 좌클릭으로 공격을 할 수 있지.",
        "이동키와 스페이스바를 누르면 대쉬도 가능하지.",
        "대쉬를 사용하면 적의 공격이나 구덩이를 넘을 수 있어",
        "마침 앞에 구덩이가 있네. 대쉬를 연습해보자.",
        " ",//6
        "좋아. 손쉽게 구덩이를 전부 넘었군.",
        "자, 이젠 스킬에 대해서 다시 짚어보자.",
        " ",//9
        "위의 스킬 아이콘에 마우스 커서를 가져다 대면 스킬의 정보를 알 수 있어.",
        " ",//텍스트 위 아래 옮길 때 구분하기 위한 공백 텍스트 11
        "그리고 스킬 아이콘을 누르고 있으면 스킬이 잡히고,\n밑의 스킬창에 가져다 두면 사용할 수 있어.",
        "설정한 기존 스킬을 다른 스킬로 변경하려면,\n변경할 스킬의 아이콘을 기존 스킬 아이콘 위치에 올려두거나\n기존 스킬의 아이콘을 스킬 선택창 아무 곳에나 두면 돼.",
        "스킬은 총 5개까지 설정 가능하고, 스킬을 사용할 수 있는 조작키는\n마우스 우클릭, Q, E, R, 왼쪽 Shift야.",
        " ",//텍스트 위 아래 옮길 때 구분하기 위한 공백 텍스트 15
        "참고로 마왕의 방에 입장하게 되면, 마왕의 방 안에선\n저주로 인해 설정한 스킬들을 바꿀 수 없으니 신중하게 스킬을 선택하고 들어가자.",
        " ",//17
        "자, 이제 복습은 이쯤 하면 됐을거야.",
        "마왕을 꼭 물리치고 세상의 평화를 되찾는거야... 가자!",
        " "
    };

    private string[] tutorialQuestScenario =
    {
        "Tutorial 1: 구덩이를 전부 넘자!",
        "Tutorial 2: 스킬에 대해서 – 스킬 선택창 클릭!",
        "Tutorial 2: 스킬에 대해서 – 스킬 선택하기!"
    };

    private string[] Ending =
    {
        "그렇게 용사는 마왕을 물리쳤고...",
        "세계는 다시 평화를 되찾게 되었다...",
        "하지만 아직 모른다...",
        "또 다른 위협이 언제 다시 들이닥칠지는...",
        " "
    };
    #endregion


    private void Start()
    {
        //sizeCtrl = transform.GetChild(0).gameObject.GetComponent<SizeCtrl>();
        text = transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>();//텍스트 오브젝트 가져오기
        Endingtext = transform.parent.GetChild(6).GetChild(0).GetComponent<Text>();
        upPos = new Vector3(200, 900, 0);
        downPos = new Vector3(200, 270, 0);
        rect = transform.GetChild(0).GetComponent<RectTransform>();
    }

    public void BossTextOn(int scriptNum)
    {
        textName.text = "마왕";
        StartCoroutine("IBossTextOn", scriptNum);
    }

    public void BossPhase2On(int scriptNum)
    {
        textName.text = "마왕";
        StartCoroutine("IBossPhase2On", scriptNum);
    }

    public void BossPhase3On(int scriptNum)
    {
        textName.text = "마왕";
        StartCoroutine("IBossPhase3On", scriptNum);
    }
    public void BossDead(int scriptNum)
    {
        textName.text = "마왕";
        StartCoroutine("IBossDead", scriptNum);
    }

    public void EndingOn(int scriptNum)
    {
        StartCoroutine("IEnding", scriptNum);
    }
    public void TutorialTextOn(int scriptNum)
    {
        textName.text = "용사";
        StartCoroutine("ITutorialTextOn", scriptNum);
    }

    public void TextClose()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void TextPosUp()
    {
        rect.offsetMin = new Vector2(100, 900);
        rect.offsetMax = new Vector2(-100, 800);
        rect.sizeDelta = new Vector2(-200, 350);
    }

    public void TextPosDown()
    {
        rect.offsetMin = new Vector2(100, 270);
        rect.offsetMax = new Vector2(-100, 160);
        rect.sizeDelta = new Vector2(-200, 350);
    }

    IEnumerator IBossTextOn(int scriptNum)
    {
        Player.GetInstance().playerConfine();
        Player.GetInstance().playerConfine("Skill");

        if (isTextOn == false)//다른 대사 텍스트가 나오는 동안에 출력되지 않게 막기
        {
            transform.GetChild(0).gameObject.SetActive(true);
            text.text = "";
            for (; bossDoorScenario[scriptNum] != " ";)
            {
                yield return new WaitForSeconds(0.5f);
                for (int i = 0; i < bossDoorScenario[scriptNum].Length; i++)//해당 대사의 길이만큼 반복하며 텍스트를 점점 띄우기
                {
                    if (Input.GetKey(KeyCode.Space))
                        break;

                    //sizeCtrl.Fix(i + 1);
                    //Debug.Log(bossDoorScenario[scriptNum].Length + "  " + i);
                    text.text = bossDoorScenario[scriptNum].Substring(0, i + 1);
                    yield return new WaitForSeconds(delay);
                }

                text.text = bossDoorScenario[scriptNum];

                yield return new WaitForSeconds(0.5f);
                while (true)
                {
                    if (Input.GetKey(KeyCode.Space))
                    {
                        Debug.Log(1);
                        break;
                    }
                        
                    yield return new WaitForSeconds(Time.deltaTime);
                }
                scriptNum++;
            }
            transform.GetChild(0).gameObject.SetActive(false);

        }

        if (scriptNum == 1)
        {
            FadeManager.GetInstance().FadeOut(GameManager.GetInstance().fadeImage_loading);

            yield return new WaitForSeconds(1.3f);

            CameraControl.GetInstance().StartCoroutine("setCameraToBoss");

            yield return new WaitForSeconds(2f);

            BossTextOn(2);
        }
        if(scriptNum == 4)
        {
            GameObject.Find("Map").transform.GetChild(6).gameObject.SetActive(true);

            yield return new WaitForSeconds(1.5f);

            BossTextOn(5);
        }
        if(scriptNum == 6)
        {
            SoundManager.GetInstance().Play("Sound/BGM/BGM_ingameBoss", 0.3f, Define.Sound.Bgm, 0.5f);
            CameraControl.GetInstance().StartCoroutine("setCameraToPlayer");

            SkillSelectManager.GetInstance().CoolTimeInit();
            Player.GetInstance().player_hp = 1000;
            yield return new WaitForSeconds(1.5f);

            EnterTheBossRoom.GetInstance().Invoke("FightStart", 2f);
            Player.GetInstance().playerFree();
            Player.GetInstance().playerFree("Skill");
        }
    }

    IEnumerator IBossPhase2On(int scriptNum)
    {
        Player.GetInstance().playerConfine();
        Player.GetInstance().playerConfine("Skill");
        Barrage[] barrage = FindObjectsOfType<Barrage>();
        Laser[] laser = FindObjectsOfType<Laser>();

        for (int i = 0; i < barrage.Length; i++)
        {
            barrage[i].Delete();
        }

        for (int i = 0; i < laser.Length; i++)
        {
            laser[i].gameObject.SetActive(false);
        }

        if (isTextOn == false)//�ٸ� ���� �ؽ�Ʈ�� ������ ���ȿ� ���µ��� �ʰ� ����
        {

            transform.GetChild(0).gameObject.SetActive(true);
            text.text = "";
            for (; bossPhase2Scenario[scriptNum] != " ";)
            {
                yield return new WaitForSeconds(0.5f);
                for (int i = 0; i < bossPhase2Scenario[scriptNum].Length; i++)//해당 대사의 길이만큼 반복하며 텍스트를 점점 띄우기
                {
                    if (Input.GetKey(KeyCode.Space))
                        break;

                    //sizeCtrl.Fix(i + 1);
                    //Debug.Log(bossDoorScenario[scriptNum].Length + "  " + i);
                    text.text = bossPhase2Scenario[scriptNum].Substring(0, i + 1);
                    yield return new WaitForSeconds(delay);
                }

                text.text = bossPhase2Scenario[scriptNum];

                yield return new WaitForSeconds(0.5f);
                while (true)
                {
                    if (Input.GetKey(KeyCode.Space))
                    {
                        Debug.Log(1);
                        break;
                    }

                    yield return new WaitForSeconds(Time.deltaTime);
                }
                scriptNum++;
            }
            transform.GetChild(0).gameObject.SetActive(false);

        }

        if (scriptNum == 0)
        {
            FadeManager.GetInstance().FadeOut(GameManager.GetInstance().fadeImage_loading);

            yield return new WaitForSeconds(1.3f);

            CameraControl.GetInstance().StartCoroutine("setCameraToBoss");
            yield return new WaitForSeconds(2f);

            BossPhase2On(1);
        }
        if (scriptNum == 2)
        {
            for(int i = 0; i < 32; i++)
            {
                GameObject.Find("Map").transform.GetChild(6).GetChild(i).GetComponent<Animator>().SetTrigger("SkulDestroy");
            }
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < 32; i++)
            {
                GameObject.Find("Map").transform.GetChild(6).GetChild(i).gameObject.SetActive(false);
            }
            Boss.GetInstance().anim.SetTrigger("Phase2");
            while(Boss.GetInstance().BossVanished)
            {
                yield return new WaitForSeconds(0.01f);
            }
            Boss.GetInstance().transform.position = new Vector3(0, 26, 0);
            CameraControl.GetInstance().StartCoroutine("setCameraToBoss");
            yield return new WaitForSeconds(2f);
            GameObject.Find("UI").transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            BossPhase2On(3);
        }
        if (scriptNum == 4)
        {
            CameraControl.GetInstance().StartCoroutine("setCameraToPlayer");

            Player.GetInstance().playerFree();
            Player.GetInstance().playerFree("Skill");
            SkillSelectManager.GetInstance().CoolTimeInit();

            yield return new WaitForSeconds(1.5f);
            Boss.GetInstance().BossStop = true;
            Boss.GetInstance().PatternManager();
        }
    }

    IEnumerator IBossPhase3On(int scriptNum)
    {
        Player.GetInstance().playerConfine();
        Player.GetInstance().playerConfine("Skill");
        Barrage[] barrage = FindObjectsOfType<Barrage>();
        Laser[] laser = FindObjectsOfType<Laser>();
        BigBarrage[] bigBarrage = FindObjectsOfType<BigBarrage>();


        for (int i = 0; i < barrage.Length; i++)
        {
            barrage[i].Delete();
        }

        for (int i = 0; i < laser.Length; i++)
        {
            laser[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < bigBarrage.Length; i++)
        {
            bigBarrage[i].gameObject.SetActive(false);
        }

        if (isTextOn == false)//�ٸ� ���� �ؽ�Ʈ�� ������ ���ȿ� ���µ��� �ʰ� ����
        {

            transform.GetChild(0).gameObject.SetActive(true);
            text.text = "";
            for (; bossPhase3Scenario[scriptNum] != " ";)
            {
                yield return new WaitForSeconds(0.5f);
                for (int i = 0; i < bossPhase3Scenario[scriptNum].Length; i++)//�ش� ������ ���̸�ŭ �ݺ��ϸ� �ؽ�Ʈ�� ���� ������
                {
                    if (Input.GetKey(KeyCode.Space))
                        break;

                    //sizeCtrl.Fix(i + 1);
                    //Debug.Log(bossDoorScenario[scriptNum].Length + "  " + i);
                    text.text = bossPhase3Scenario[scriptNum].Substring(0, i + 1);
                    yield return new WaitForSeconds(delay);
                }

                text.text = bossPhase3Scenario[scriptNum];

                yield return new WaitForSeconds(0.5f);
                while (true)
                {
                    if (Input.GetKey(KeyCode.Space))
                    {
                        Debug.Log(1);
                        break;
                    }

                    yield return new WaitForSeconds(Time.deltaTime);
                }
                scriptNum++;
            }
            transform.GetChild(0).gameObject.SetActive(false);

        }

        if (scriptNum == 0)
        {
            FadeManager.GetInstance().FadeOut(GameManager.GetInstance().fadeImage_loading);

            yield return new WaitForSeconds(1.3f);

            CameraControl.GetInstance().StartCoroutine("setCameraToBoss");
            yield return new WaitForSeconds(1f);
            Boss.GetInstance().anim.SetTrigger("Phase3");
            yield return new WaitForSeconds(2f);

            BossPhase3On(1);
        }
        if (scriptNum == 3)
        {
            CameraControl.GetInstance().StartCoroutine("setCameraToPlayer");

            SkillSelectManager.GetInstance().CoolTimeInit();
            yield return new WaitForSeconds(1.5f);
            Boss.GetInstance().BossStop = true;
            Boss.GetInstance().PatternManager();

            Player.GetInstance().playerFree();
            Player.GetInstance().playerFree("Skill");
        }
    }

    IEnumerator IBossDead(int scriptNum)
    {
        Player.GetInstance().playerConfine();
        Player.GetInstance().playerConfine("Skill");
        SkillSelectManager.GetInstance().OpenSkillSetting();

        SetCursor.GetInstance().ChangeCursor(1);

        Barrage[] barrage = FindObjectsOfType<Barrage>();
        Laser[] laser = FindObjectsOfType<Laser>();
        AOE[] aoe = FindObjectsOfType<AOE>();

        for (int i = 0; i < barrage.Length; i++)
        {
            barrage[i].Delete();
        }

        for (int i = 0; i < laser.Length; i++)
        {
            laser[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < aoe.Length; i++)
        {
            aoe[i].gameObject.SetActive(false);
        }

        SoundManager.GetInstance().Play("Sound/BGM/BGM_Ending", 0.3f, Define.Sound.Bgm, 1f);

        if (isTextOn == false)//�ٸ� ���� �ؽ�Ʈ�� ������ ���ȿ� ���µ��� �ʰ� ����
        {
            transform.GetChild(0).gameObject.SetActive(true);
            text.text = "";
            for (; bossDeadScenario[scriptNum] != " ";)
            {
                yield return new WaitForSeconds(0.5f);
                for (int i = 0; i < bossDeadScenario[scriptNum].Length; i++)//�ش� ������ ���̸�ŭ �ݺ��ϸ� �ؽ�Ʈ�� ���� ������
                {
                    if (Input.GetKey(KeyCode.Space))
                        break;

                    //sizeCtrl.Fix(i + 1);
                    //Debug.Log(bossDoorScenario[scriptNum].Length + "  " + i);
                    text.text = bossDeadScenario[scriptNum].Substring(0, i + 1);
                    yield return new WaitForSeconds(delay);
                }

                text.text = bossDeadScenario[scriptNum];
                yield return new WaitForSeconds(0.5f);
                while (true)
                {
                    if (Input.GetKey(KeyCode.Space))
                    {
                        Debug.Log(1);
                        break;
                    }

                    yield return new WaitForSeconds(Time.deltaTime);
                }
                scriptNum++;
            }
            transform.GetChild(0).gameObject.SetActive(false);

        }
        if (scriptNum == 0)
        {
            FadeManager.GetInstance().FadeOut(GameManager.GetInstance().fadeImage_loading);

            yield return new WaitForSeconds(1.3f);

            CameraControl.GetInstance().StartCoroutine("setCameraToBoss");
            yield return new WaitForSeconds(2f);

            BossDead(1);
        }
        if (scriptNum == 3)
        {
            CameraControl.GetInstance().StartCoroutine("setCameraToPlayer");

            yield return new WaitForSeconds(1.5f);
            Boss.GetInstance().BossStop = true;
            GameManager.GetInstance().StartCoroutine("IEnding");
            Boss.GetInstance().gameObject.SetActive(false);
            }
        }

    IEnumerator ITutorialTextOn(int scriptNum)
    {
        if (scriptNum < 11)
        {
            textQuest.text = "";
            Debug.Log("scriptNum" + scriptNum);
        }


        if (scriptNum == 0)
        {
            transform.parent.GetChild(1).gameObject.SetActive(false);
        }
        //특수한 텍스트 넘버는 위쪽에 출력하도록 설정
        if (scriptNum == 10)
        {
            if (TutorialManager.GetInstance().isSecondMissionClear1 == false)
            {
                TutorialManager.GetInstance().isSecondMissionClear1 = true;
                backButton.gameObject.SetActive(false);
            }
            else
            {
                yield break;
            }
        }

        if (isTextOn == false)//다른 대사 텍스트가 나오는 동안에 출력되지 않게 막기
        {

            transform.GetChild(0).gameObject.SetActive(true);
            text.text = "";
            for (; tutorialScenario[scriptNum] != " ";)
            {
                yield return new WaitForSeconds(0.5f);
                for (int i = 0; i < tutorialScenario[scriptNum].Length; i++)//해당 대사의 길이만큼 반복하며 텍스트를 점점 띄우기
                {
                    if (Input.GetKey(KeyCode.Space))
                        break;

                    //sizeCtrl.Fix(i + 1);
                    //Debug.Log(bossDoorScenario[scriptNum].Length + "  " + i);
                    text.text = tutorialScenario[scriptNum].Substring(0, i + 1);
                    yield return new WaitForSeconds(delay);
                }

                text.text = tutorialScenario[scriptNum];

                yield return new WaitForSeconds(0.5f);
                while (true)
                {
                    if (Input.GetKey(KeyCode.Space))
                    {
                        Debug.Log(1);
                        break;
                    }

                    yield return new WaitForSeconds(Time.deltaTime);
                }
                scriptNum++;
            }
            transform.GetChild(0).gameObject.SetActive(false);

        }
        if (scriptNum == 6)
        {
            Player.GetInstance().playerFree();
            Player.GetInstance().playerFree("Skill");

            //오른쪽 위 퀘스트 텍스트 띄우기
            textQuest.text = " ";

            for (int i = 0; i < tutorialQuestScenario[0].Length; i++)//해당 대사의 길이만큼 반복하며 텍스트를 점점 띄우기
            {
                //sizeCtrl.Fix(i + 1);
                //Debug.Log(bossDoorScenario[scriptNum].Length + "  " + i);
                textQuest.text = tutorialQuestScenario[0].Substring(0, i + 1);
                yield return new WaitForSeconds(delay);
            }

            textQuest.text = tutorialQuestScenario[0];
        }
        else if (scriptNum == 9)
        {
            Player.GetInstance().playerFree();
            Player.GetInstance().playerFree("Skill");

            //오른쪽 위 퀘스트 텍스트 띄우기
            textQuest.text = " ";

            for (int i = 0; i < tutorialQuestScenario[1].Length; i++)//해당 대사의 길이만큼 반복하며 텍스트를 점점 띄우기
            {
                //sizeCtrl.Fix(i + 1);
                //Debug.Log(bossDoorScenario[scriptNum].Length + "  " + i);
                textQuest.text = tutorialQuestScenario[1].Substring(0, i + 1);
                yield return new WaitForSeconds(delay);
            }

            textQuest.text = tutorialQuestScenario[1];
            transform.parent.GetChild(1).gameObject.SetActive(true);


            Color tempColor = redBox.color;
            tempColor.a = 0;
            for (int i = 0; i < 100; i++)
            {
                tempColor.a += 0.01f;
                redBox.color = tempColor;
                yield return new WaitForSeconds(0.01f);
            }
        }
        else if (scriptNum == 11)
        {
            textQuest.text = " ";

            for (int i = 0; i < tutorialQuestScenario[2].Length; i++)//해당 대사의 길이만큼 반복하며 텍스트를 점점 띄우기
            {
                //sizeCtrl.Fix(i + 1);
                //Debug.Log(bossDoorScenario[scriptNum].Length + "  " + i);
                textQuest.text = tutorialQuestScenario[2].Substring(0, i + 1);
                yield return new WaitForSeconds(delay);
            }

            textQuest.text = tutorialQuestScenario[2];

            //레드박스2 띄우기
            Color tempColor = redBox2.color;
            tempColor.a = 0;
            for (int i = 0; i < 100; i++)
            {
                tempColor.a += 0.01f;
                redBox2.color = tempColor;
                yield return new WaitForSeconds(0.01f);
            }

            TextPosUp();
            isBoxUp = true;
            TutorialTextOn(12);
        }
        else if (scriptNum == 15)
        {
            redBox2.gameObject.SetActive(false);

            TextPosDown();
            isBoxUp = false;
            TutorialTextOn(16);
        }
        else if (scriptNum == 17)
        {
            //보스룸 입장 가능
            FindObjectOfType<EnterTheBossRoom>().GetComponent<CapsuleCollider2D>().enabled = true;
            textQuest.text = "";
            TutorialManager.GetInstance().isSecondMissionClear2 = true;
            backButton.gameObject.SetActive(true);
        }
    }

    IEnumerator IEnding(int scriptNum)
    {
        if (isTextOn == false)//�ٸ� ��� �ؽ�Ʈ�� ������ ���ȿ� ��µ��� �ʰ� ����
        {

            transform.parent.GetChild(6).GetChild(0).gameObject.SetActive(true);
            Endingtext.text = "";
            for (; Ending[scriptNum] != " ";)
            {
                yield return new WaitForSeconds(0.5f);
                for (int i = 0; i < Ending[scriptNum].Length; i++)//�ش� ����� ���̸�ŭ �ݺ��ϸ� �ؽ�Ʈ�� ���� ����
                {
                    if (Input.GetKey(KeyCode.Space))
                        break;

                    //sizeCtrl.Fix(i + 1);
                    //Debug.Log(bossDoorScenario[scriptNum].Length + "  " + i);
                    Endingtext.text = Ending[scriptNum].Substring(0, i + 1);
                    yield return new WaitForSeconds(delay);
                }

                Endingtext.text = Ending[scriptNum];

                yield return new WaitForSeconds(0.5f);
                while (true)
                {
                    if (Input.GetKey(KeyCode.Space))
                    {
                        Debug.Log(1);
                        break;
                    }

                    yield return new WaitForSeconds(Time.deltaTime);
                }
                scriptNum++;
            }

            if (scriptNum == 4)
            {
                transform.parent.GetChild(6).GetChild(0).gameObject.SetActive(false);
                transform.parent.GetChild(6).GetChild(1).gameObject.SetActive(true);
            }

        }
    }
}