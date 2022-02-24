using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public static CameraControl cameraControl;
    public static CameraControl GetInstance()
    {
        if (cameraControl == null)
        {
            cameraControl = FindObjectOfType<CameraControl>();
        }

        return cameraControl;

    }

    bool camFollowPlayer = true;
    public Transform player;
    
    //#cameraDistance설명.0에 가까울 수록 멀리 볼 수 있음
    //#cameraDistance설명.1에 가까울 수록 플레이어 시야 고정
    [Range(0.0f, 1.0f)]
    public float cameraDistance;

    void Update(){
        //#1.카메라가 플레이어 따라다님 + 커서 방향으로 위치 약간 쏠림 
        setCameraMoving();
    }

    void setCameraMoving()
    {
        if (!camFollowPlayer)
            return;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = Vector3.Lerp(new Vector3(mousePos.x, mousePos.y, -10), player.position + new Vector3(0,0,-10), cameraDistance);
    }

    IEnumerator setCameraToBoss()
    {
        camFollowPlayer = false;
        for (float i = 0; i < 100; i++)
        {
            transform.position = Vector3.Lerp(transform.position, Boss.GetInstance().transform.position + new Vector3(0, 0, -10), i*Time.deltaTime);
            yield return new WaitForSeconds(0.02f);
        }   
    }

    IEnumerator setCameraToPlayer()
    {
        for (float i = 0; i < 100; i++)
        {
            transform.position = Vector3.Lerp(transform.position, Player.GetInstance().transform.position + new Vector3(0, 0, -10), i * Time.deltaTime);
            yield return new WaitForSeconds(0.02f);
        }
        camFollowPlayer = true;
    }
}
