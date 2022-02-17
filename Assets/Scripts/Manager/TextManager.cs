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
    public float delay;//텍스트 출력 속도

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
        "근데 왜 이렇게 일찍 왔냐?",
        "아직 똥 못 쌌는데...",
        "내가 나설 필요도 없겠다. 지금 배 아파서 움직면 큰일나.",//4
        " "//5
    };

    private string[] bossPhase2Scenario =
    {
        " ",
        "오호... 네 녀석 꽤나 강력하구나...",
        "그럼... 이 몸이 살짝 움직여 볼까?",
        " "
    };

    private string[] bossPhase3Scenario =
    {
        " ",
        "크윽... 이번엔 진심이다아앗~!",
        " "
    };

    private string[] bossDeadScenario =
    {
        " ",
        "죽으면 똥 지리겠네 시벌ㅋㅋ",
        " "
    };

    private string[] Ending =
    {
        "그렇게 용사는 마왕을 물리쳤고...",
        "세계는 다시 평화를 되찾게 되었다...",
        "하지만 아직 모른다...",
        "또 다른 위협이 언제 들이닥칠지는...",
        " "
    };

    #endregion
    private void Start()
    {
        //sizeCtrl = transform.GetChild(0).gameObject.GetComponent<SizeCtrl>();
        text = transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>();//텍스트 오브젝트 가져오기
        Endingtext = transform.parent.GetChild(6).GetChild(0).GetComponent<Text>();
    }

    public void BossTextOn(int scriptNum)
    {
        StartCoroutine("IBossTextOn", scriptNum);
    }

    public void BossPhase2On(int scriptNum)
    {
        StartCoroutine("IBossPhase2On", scriptNum);
    }

    public void BossPhase3On(int scriptNum)
    {
        StartCoroutine("IBossPhase3On", scriptNum);
    }
    public void BossDead(int scriptNum)
    {
        StartCoroutine("IBossDead", scriptNum);
    }

    public void EndingOn(int scriptNum)
    {
        StartCoroutine("IEnding", scriptNum);
    }

    public void TextClose()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }
    

    IEnumerator IBossTextOn(int scriptNum)
    {
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
        else if(scriptNum == 5)
        {
            CameraControl.GetInstance().StartCoroutine("setCameraToPlayer");

            yield return new WaitForSeconds(1.5f);

            EnterTheBossRoom.GetInstance().FightStart();
        }
    }

    IEnumerator IBossPhase2On(int scriptNum)
    {
        Barrage[] barrage = FindObjectsOfType<Barrage>();

        for (int i = 0; i < barrage.Length; i++)
        {
            barrage[i].Delete();
        }

        if (isTextOn == false)//다른 대사 텍스트가 나오는 동안에 출력되지 않게 막기
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
        if (scriptNum == 3)
        {
            CameraControl.GetInstance().StartCoroutine("setCameraToPlayer");

            yield return new WaitForSeconds(1.5f);
            Boss.GetInstance().BossStop = true;
            Boss.GetInstance().PatternManager();
        }
    }

    IEnumerator IBossPhase3On(int scriptNum)
    {
        Barrage[] barrage = FindObjectsOfType<Barrage>();

        for (int i = 0; i < barrage.Length; i++)
        {
            barrage[i].Delete();
        }

        if (isTextOn == false)//다른 대사 텍스트가 나오는 동안에 출력되지 않게 막기
        {

            transform.GetChild(0).gameObject.SetActive(true);
            text.text = "";
            for (; bossPhase3Scenario[scriptNum] != " ";)
            {
                yield return new WaitForSeconds(0.5f);
                for (int i = 0; i < bossPhase3Scenario[scriptNum].Length; i++)//해당 대사의 길이만큼 반복하며 텍스트를 점점 띄우기
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
            yield return new WaitForSeconds(2f);

            BossPhase3On(1);
        }
        if (scriptNum == 2)
        {
            CameraControl.GetInstance().StartCoroutine("setCameraToPlayer");

            yield return new WaitForSeconds(1.5f);
            Boss.GetInstance().BossStop = true;
            Boss.GetInstance().PatternManager();
        }
    }

    IEnumerator IBossDead(int scriptNum)
    {
        Barrage[] barrage = FindObjectsOfType<Barrage>();

        for (int i = 0; i < barrage.Length; i++)
        {
            barrage[i].Delete();
        }

        if (isTextOn == false)//다른 대사 텍스트가 나오는 동안에 출력되지 않게 막기
        {
            transform.GetChild(0).gameObject.SetActive(true);
            text.text = "";
            for (; bossDeadScenario[scriptNum] != " ";)
            {
                yield return new WaitForSeconds(0.5f);
                for (int i = 0; i < bossDeadScenario[scriptNum].Length; i++)//해당 대사의 길이만큼 반복하며 텍스트를 점점 띄우기
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
        if (scriptNum == 2)
        {
            CameraControl.GetInstance().StartCoroutine("setCameraToPlayer");

            yield return new WaitForSeconds(1.5f);
            Boss.GetInstance().BossStop = true;
            GameManager.GetInstance().StartCoroutine("IEnding");
            Boss.GetInstance().gameObject.SetActive(false);
        }
    }
    IEnumerator IEnding(int scriptNum)
    {
        if (isTextOn == false)//다른 대사 텍스트가 나오는 동안에 출력되지 않게 막기
        {

            transform.parent.GetChild(6).GetChild(0).gameObject.SetActive(true);
            Endingtext.text = "";
            for (; Ending[scriptNum] != " ";)
            {
                yield return new WaitForSeconds(0.5f);
                for (int i = 0; i < Ending[scriptNum].Length; i++)//해당 대사의 길이만큼 반복하며 텍스트를 점점 띄우기
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
                Debug.Log("아니이게왜지금돼");
                transform.parent.GetChild(6).GetChild(0).gameObject.SetActive(false);
                transform.parent.GetChild(6).GetChild(1).gameObject.SetActive(true);
            }

        }
    }
}