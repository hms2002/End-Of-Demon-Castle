using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCursor : MonoBehaviour
{
    [SerializeField] Texture2D[] cursorImg;
    public static SetCursor setCursor;
    public static SetCursor GetInstance()
    {
        if (setCursor == null)
        {
            setCursor = FindObjectOfType<SetCursor>();
        }
        return setCursor;
    }
    void Start()
    {
        Cursor.SetCursor(cursorImg[0], Vector2.zero, CursorMode.ForceSoftware);
    }

    //base - 기본 커서
    //backSteb - 백스탭 커서
    public void ChangeCursor(string cursorName)
    {
        switch(cursorName)
        {
            case "base":
                Cursor.SetCursor(cursorImg[0], new Vector2(cursorImg[0].width / 2, cursorImg[1].height / 2), CursorMode.ForceSoftware);
            break;
            case "backSteb":
                Cursor.SetCursor(cursorImg[1], new Vector2(cursorImg[1].width/2, cursorImg[1].height/2), CursorMode.ForceSoftware);
            break;
            case "magicCircle":
                Cursor.SetCursor(cursorImg[2], Vector2.zero, CursorMode.ForceSoftware);
            break;
            case "skillSetting":
                Cursor.SetCursor(cursorImg[2], Vector2.zero, CursorMode.ForceSoftware);
                break;
            case "onDrag":
                Cursor.SetCursor(cursorImg[2], Vector2.zero, CursorMode.ForceSoftware);
                break;
        }

    }

    public void ChangeCursor(int cursorIdx)
    {
        Cursor.SetCursor(cursorImg[cursorIdx], Vector2.zero, CursorMode.ForceSoftware);
    }
}
