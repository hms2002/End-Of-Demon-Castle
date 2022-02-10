using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class SkillSlot : MonoBehaviour, IDragHandler, IBeginDragHandler, IDropHandler, IEndDragHandler
{
    public Skill_ID skill;
    public Image data;
    Sprite defaultImg;
    public DragAndDropCantainer dragAndDropContainer;
    public DragPlus beforeDrag;

    public bool canSelect;
    public bool isDragging = false;

    private void Start() {
        data = GetComponent<Image>();
        defaultImg = data.sprite;
    }


//슬롯을 드래그하려고 할 때 작동하는 부분
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(defaultImg == data.sprite)
            canSelect = false;
        else
            canSelect = true;

        if (data.sprite == null)
            return;
        
        if(canSelect == false)
            return;
        
        // Activate Container
        dragAndDropContainer.gameObject.SetActive(true);
        // Set Data 
        dragAndDropContainer.image.sprite = data.sprite;
        dragAndDropContainer.skill = skill;
        dragAndDropContainer.skillSlot = this;
        isDragging = true;
    }
    public void OnDrag(PointerEventData eventData)
    {
        if(canSelect == false)
            return;
        if (isDragging)
        {
            dragAndDropContainer.gameObject.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //
        if(canSelect == false)
            return;
        if(dragAndDropContainer.image.sprite == data.sprite)
        {
            beforeDrag.OnAir();
            beforeDrag = null;
            data.sprite = defaultImg;
            skill = null;
        }
        else
        {
            Debug.Log(dragAndDropContainer.image.sprite);
            data.sprite = dragAndDropContainer.image.sprite;
            skill = dragAndDropContainer.skill;
        }

        isDragging = false;
        // Reset Contatiner
        dragAndDropContainer.image.sprite = null;
        dragAndDropContainer.skill = null;
        dragAndDropContainer.skillSlot = null;
        dragAndDropContainer.gameObject.SetActive(false);
    }

//슬롯에 집어넣을 때 구간
    public void OnDrop(PointerEventData eventData)
    {
        if (dragAndDropContainer.image.sprite != null)
        {
            Sprite tempSprite = dragAndDropContainer.image.sprite;
            Skill_ID tempSkill = dragAndDropContainer.skill;

            if(dragAndDropContainer.skillSlot == null)
            {
                if(beforeDrag != null)
                    beforeDrag.OnAir();

                data.sprite = tempSprite;
                skill = tempSkill;

                beforeDrag = skill.gameObject.GetComponent<DragPlus>();
                dragAndDropContainer.image.sprite = null;
            }
            else if(dragAndDropContainer.skillSlot != null)
            {
                Debug.Log("컨테이너 : " + dragAndDropContainer.image.sprite);
                Debug.Log("데이터.스프라잍 : " + data.sprite);
                //등록된 이미지, 스킬 교환s
                dragAndDropContainer.image.sprite = data.sprite;
                dragAndDropContainer.skill = skill;
                DragPlus tempDrag = dragAndDropContainer.skillSlot.beforeDrag;

                //등록된 DragPlus 교환
                dragAndDropContainer.skillSlot.beforeDrag = beforeDrag;
                beforeDrag = tempDrag;

                data.sprite = tempSprite;
                skill = tempSkill;
            }
            SoundManager.GetInstance().Play("Sound/SystemSound/ChangeSkillSlot");
        }
        else
        {
        }
    }
}
