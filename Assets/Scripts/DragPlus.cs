using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class DragPlus : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public bool canSelect;
    Image data;
    public DragAndDropCantainer dragAndDropContainer;
    Skill_ID skill;
    public bool isDragging = false;

    private void Start() {
        skill = GetComponent<Skill_ID>();
        data = GetComponent<Image>();
        canSelect = true;
    }

	// 드래그 오브젝트에서 발생
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (data.sprite == null)
            return;
        
        if(canSelect == false)
            return;
        
        // Activate Container
        dragAndDropContainer.gameObject.SetActive(true);
        // Set Data 
        dragAndDropContainer.image.sprite = data.sprite;
        dragAndDropContainer.skill = skill;
        isDragging = true;
    }
	// 드래그 오브젝트에서 발생
    public void OnDrag(PointerEventData eventData)
    {
        //
        if(canSelect == false)
            return;
        if (isDragging)
        {
            dragAndDropContainer.gameObject.transform.position = eventData.position;
        }
    }
	// 드래그 오브젝트에서 발생
    public void OnEndDrag(PointerEventData eventData)
    {
        if (dragAndDropContainer.image.sprite == null)
        {
            // set data from dropped object  
            data.color = Color.gray;
            canSelect = false;
            // if(dragAndDropContainer.beforeSkill != null)
            //     dragAndDropContainer.beforeSkill.OnAir();

            // dragAndDropContainer.beforeSkill = this;
        }
        
        isDragging = false;
        // Reset Contatiner
        dragAndDropContainer.image.sprite = null;
        dragAndDropContainer.skill = null;
        dragAndDropContainer.gameObject.SetActive(false);
    }

    public void OnAir()
    {
        data.color = Color.white;
        canSelect = true;
    }
    
    // Vector3 OriginPos;
    // RectTransform rectTransform;
    // private void Start() {
    //     rectTransform = GetComponent<RectTransform>();
    //     OriginPos = rectTransform.localPosition;
    // }
    // public void OnBeginDrag(PointerEventData eventData)
    // {
    //     Debug.Log("Start");
        
    //     //throw new System.NotImplementedException();
    // }

    // public void OnDrag(PointerEventData eventData)
    // {
    //     Debug.Log("Draging");
    //     transform.position =  eventData.position;
    //     //throw new System.NotImplementedException();
    // }

    // public void OnEndDrag(PointerEventData eventData)
    // {
    //     Debug.Log("End");
    //     rectTransform.localPosition = OriginPos;
    //     //throw new System.NotImplementedException();
    // }
}
