using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    public Transform player;
    public Transform moveSpot;
    public Transform moveSpot2;
    public float moveLength;

    int MAX_DISTANCE;

    Vector2 dir;

    // Start is called before the first frame update
    void Start(){
        MAX_DISTANCE = 20;
    }

    // Update is called once per frame
    void Update(){
        
        dir = new Vector2(Input.mousePosition.x/1600 - player.position.x, Input.mousePosition.y/900 - player.position.y).normalized;

        //moveSpot.position = new Vector3(dir.x * moveLength, dir.y * moveLength, -10);
        moveSpot.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10);
        moveSpot2.position = new Vector3(dir.x, dir.y, -10);
        float length = Vector3.Distance(transform.position, moveSpot.position);
        
        float fracDistance = length / MAX_DISTANCE;
        Debug.Log("[Length] : " + length);
        Debug.Log("[FracDistance] : " + fracDistance);
        transform.localPosition = Vector3.Lerp(moveSpot.position, transform.position, 1);
    }
}
