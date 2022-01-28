using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonToRed : MonoBehaviour
{
    public GameObject startButton;
    public GameObject optionButton;
    public GameObject exitButton;
    public Text start;
    public Text option;
    public Text exit;

    //버튼 색상 변환
    public void MousePointEnter(int buttonType)
    {
        Color textColor;

        switch (buttonType)
        {
            case 0://start
                textColor = start.color;
                textColor.r += 1;
                start.color = textColor;

                break;

            case 1://option
                textColor = option.color;
                textColor.r += 1;
                option.color = textColor;

                break;

            case 2://exit
                textColor = exit.color;
                textColor.r += 1;
                exit.color = textColor;

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
                textColor.r -= 1;
                start.color = textColor;

                break;

            case 1://option
                textColor = option.color;
                textColor.r -= 1;
                option.color = textColor;

                break;

            case 2://exit
                textColor = exit.color;
                textColor.r -= 1;
                exit.color = textColor;

                break;

            default:
                break;
        }
    }
}
