using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffLayoutSetting : MonoBehaviour
{
    private static BuffLayoutSetting buffLayoutSetting;
    public static BuffLayoutSetting GetInstance()
    {
        if(buffLayoutSetting == null)
        {
            buffLayoutSetting = FindObjectOfType<BuffLayoutSetting>();
        }
        return buffLayoutSetting;
    }

    RectTransform rect;
    int buffCnt;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        buffCnt = 0;
    }

    //#.SubBuff : 버프 이미지 개수 피드백
    public void AddBuff()
    {
        buffCnt++;
        Setting();
    }

    //#.SubBuff : 버프 이미지 개수 피드백
    public void SubBuff()
    {
        buffCnt--;
        Setting();
    }

    //#.SubBuff : 버프 이미지 위치 조절
    public void Setting()
    {
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, buffCnt * 30 + (buffCnt - 1) * 20);
        rect.anchoredPosition = new Vector2(-755 + (buffCnt - 1) * 25, 413.5861f);
    }
}
