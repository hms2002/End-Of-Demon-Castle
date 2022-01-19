using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCursor : MonoBehaviour
{
    [SerializeField] Texture2D[] cursorImg;
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
                Cursor.SetCursor(cursorImg[0], Vector2.zero, CursorMode.ForceSoftware);
            break;
            case "backSteb":
                Cursor.SetCursor(cursorImg[1], Vector2.zero, CursorMode.ForceSoftware);
            break;
        }

    }
}
