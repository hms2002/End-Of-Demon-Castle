using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonToRed : MonoBehaviour
{
    public GameObject startButton;
    public GameObject exitButton;
    public Text start;
    public Text exit;
    public GameObject startTextShadow;
    public GameObject exitTextShadow;

    //버튼 색상 변환
    public void MousePointEnter(int buttonType)
    {
        Color textColor;

        switch (buttonType)
        {
            case 0://start
                textColor = start.color;
                textColor.g -= 1.0f;
                textColor.b -= 1.0f;
                start.color = textColor;
                startTextShadow.SetActive(true);

                break;

            case 1://exit
                textColor = exit.color;
                textColor.g -= 1.0f;
                textColor.b -= 1.0f;
                exit.color = textColor;
                exitTextShadow.SetActive(true);

                break;

            default:
                break;
        }
    }
    public void MousePointExit(int buttonType)
    {
        Color textColor;

        switch (buttonType)
        {
            case 0://start
                textColor = start.color;
                textColor.g += 1.0f;
                textColor.b += 1.0f;
                start.color = textColor;
                startTextShadow.SetActive(false);

                break;

            case 1://exit
                textColor = exit.color;
                textColor.g += 1.0f;
                textColor.b += 1.0f;
                exit.color = textColor;
                exitTextShadow.SetActive(false);

                break;

            default:
                break;
        }
    }
}
