using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DragPlus : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    Vector3 OriginPos;
    private void Start() {
        OriginPos = transform.position;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Start");
        
        //throw new System.NotImplementedException();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Draging");
        transform.position = eventData.position;
        //throw new System.NotImplementedException();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End");
        transform.position = OriginPos;
        //throw new System.NotImplementedException();
    }
    
}
