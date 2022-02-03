using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    //private SizeCtrl sizeCtrl;
    public bool isTextOn = false; //bool ���� ���� TextClose ���� ���� ����
    public Text text;
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

    #endregion
    private void Start()
    {
        //sizeCtrl = transform.GetChild(0).gameObject.GetComponent<SizeCtrl>();
        text = transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>();//�ؽ�Ʈ ������Ʈ ��������
    }

    public void BossTextOn(int scriptNum)
    {
        StartCoroutine("IBossTextOn", scriptNum);
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
}