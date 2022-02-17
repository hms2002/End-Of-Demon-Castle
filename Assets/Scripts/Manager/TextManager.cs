using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    //private SizeCtrl sizeCtrl;
    public bool isTextOn = false; //bool ���� ���� TextClose ���� ���� ����
    public Text text;
    public Text Endingtext;
    public float delay;//�ؽ�Ʈ ��� �ӵ�

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
        "���� �Ա��� ��翩...",
        " ",
        "�ٵ� �� �̷��� ���� �Գ�?",
        "���� �� �� �մµ�...",
        "���� ���� �ʿ䵵 ���ڴ�. ���� �� ���ļ� ������ ū�ϳ�.",//4
        " "//5
    };

    private string[] bossPhase2Scenario =
    {
        " ",
        "��ȣ... �� �༮ �ϳ� �����ϱ���...",
        "�׷�... �� ���� ��¦ ������ ����?",
        " "
    };

    private string[] bossPhase3Scenario =
    {
        " ",
        "ũ��... �̹��� �����̴پƾ�~!",
        " "
    };

    private string[] bossDeadScenario =
    {
        " ",
        "������ �� �����ڳ� �ù�����",
        " "
    };

    private string[] Ending =
    {
        "�׷��� ���� ������ �����ư�...",
        "����� �ٽ� ��ȭ�� ��ã�� �Ǿ���...",
        "������ ���� �𸥴�...",
        "�� �ٸ� ������ ���� ���̴�ĥ����...",
        " "
    };

    #endregion
    private void Start()
    {
        //sizeCtrl = transform.GetChild(0).gameObject.GetComponent<SizeCtrl>();
        text = transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>();//�ؽ�Ʈ ������Ʈ ��������
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
        if (isTextOn == false)//�ٸ� ��� �ؽ�Ʈ�� ������ ���ȿ� ��µ��� �ʰ� ����
        {

            transform.GetChild(0).gameObject.SetActive(true);
            text.text = "";
            for (; bossDoorScenario[scriptNum] != " ";)
            {
                yield return new WaitForSeconds(0.5f);
                for (int i = 0; i < bossDoorScenario[scriptNum].Length; i++)//�ش� ����� ���̸�ŭ �ݺ��ϸ� �ؽ�Ʈ�� ���� ����
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

        if (isTextOn == false)//�ٸ� ��� �ؽ�Ʈ�� ������ ���ȿ� ��µ��� �ʰ� ����
        {

            transform.GetChild(0).gameObject.SetActive(true);
            text.text = "";
            for (; bossPhase2Scenario[scriptNum] != " ";)
            {
                yield return new WaitForSeconds(0.5f);
                for (int i = 0; i < bossPhase2Scenario[scriptNum].Length; i++)//�ش� ����� ���̸�ŭ �ݺ��ϸ� �ؽ�Ʈ�� ���� ����
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

        if (isTextOn == false)//�ٸ� ��� �ؽ�Ʈ�� ������ ���ȿ� ��µ��� �ʰ� ����
        {

            transform.GetChild(0).gameObject.SetActive(true);
            text.text = "";
            for (; bossPhase3Scenario[scriptNum] != " ";)
            {
                yield return new WaitForSeconds(0.5f);
                for (int i = 0; i < bossPhase3Scenario[scriptNum].Length; i++)//�ش� ����� ���̸�ŭ �ݺ��ϸ� �ؽ�Ʈ�� ���� ����
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

        if (isTextOn == false)//�ٸ� ��� �ؽ�Ʈ�� ������ ���ȿ� ��µ��� �ʰ� ����
        {
            transform.GetChild(0).gameObject.SetActive(true);
            text.text = "";
            for (; bossDeadScenario[scriptNum] != " ";)
            {
                yield return new WaitForSeconds(0.5f);
                for (int i = 0; i < bossDeadScenario[scriptNum].Length; i++)//�ش� ����� ���̸�ŭ �ݺ��ϸ� �ؽ�Ʈ�� ���� ����
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
                Debug.Log("�ƴ��̰Կ����ݵ�");
                transform.parent.GetChild(6).GetChild(0).gameObject.SetActive(false);
                transform.parent.GetChild(6).GetChild(1).gameObject.SetActive(true);
            }

        }
    }
}