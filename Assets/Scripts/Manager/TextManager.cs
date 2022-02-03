using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    //private SizeCtrl sizeCtrl;
    public bool isTextOn = false; //bool 값에 따라 TextClose 실행 여부 결정
    public Text text;
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

    #endregion
    private void Start()
    {
        //sizeCtrl = transform.GetChild(0).gameObject.GetComponent<SizeCtrl>();
        text = transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>();//텍스트 오브젝트 가져오기
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
}