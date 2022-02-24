using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragPlus : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public bool canSelect;
    Image data;
    public DragAndDropCantainer dragAndDropContainer;
    Skill_ID skill;
    public bool isDragging = false;
    GameObject Info;
    GameObject InfoScreen;

    private void Start() {
        skill = gameObject.GetComponent<Skill_ID>();
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
        SetCursor.GetInstance().ChangeCursor(4);
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
        SetCursor.GetInstance().ChangeCursor(3);
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (dragAndDropContainer.skill == null)
        {
            int job = skill.ID / 10;
            int skills = skill.ID % 10;
            switch (job)
            {
                case 1:
                    switch (skills)
                    {
                        case 1:
                            Info = Resources.Load<GameObject>("Prefabs/UI/SkillData 1");
                            break;
                        case 2:
                            Info = Resources.Load<GameObject>("Prefabs/UI/SkillData 2");
                            break;
                        case 3:
                            Info = Resources.Load<GameObject>("Prefabs/UI/SkillData 3");
                            break;
                    }
                    break;
                case 2:
                    switch (skills)
                    {
                        case 1:
                            Info = Resources.Load<GameObject>("Prefabs/UI/SkillData 4");
                            break;
                        case 2:
                            Info = Resources.Load<GameObject>("Prefabs/UI/SkillData 5");
                            break;
                        case 3:
                            Info = Resources.Load<GameObject>("Prefabs/UI/SkillData 6");
                            break;
                    }
                    break;
                case 3:
                    switch (skills)
                    {
                        case 1:
                            Info = Resources.Load<GameObject>("Prefabs/UI/SkillData 7");
                            break;
                        case 2:
                            Info = Resources.Load<GameObject>("Prefabs/UI/SkillData 8");
                            break;
                        case 3:
                            Info = Resources.Load<GameObject>("Prefabs/UI/SkillData 9");
                            break;
                    }
                    break;
                case 4:
                    switch (skills)
                    {
                        case 1:
                            Info = Resources.Load<GameObject>("Prefabs/UI/SkillData 10");
                            break;
                        case 2:
                            Info = Resources.Load<GameObject>("Prefabs/UI/SkillData 11");
                            break;
                        case 3:
                            Info = Resources.Load<GameObject>("Prefabs/UI/SkillData 12");
                            break;
                    }
                    break;
                case 5:
                    switch (skills)
                    {
                        case 1:
                            Info = Resources.Load<GameObject>("Prefabs/UI/SkillData 13");
                            break;
                        case 2:
                            Info = Resources.Load<GameObject>("Prefabs/UI/SkillData 14");
                            break;
                        case 3:
                            Info = Resources.Load<GameObject>("Prefabs/UI/SkillData 15");
                            break;
                    }
                    break;
                case 6:
                    switch (skills)
                    {
                        case 1:
                            Info = Resources.Load<GameObject>("Prefabs/UI/SkillData 16");
                            break;
                        case 2:
                            Info = Resources.Load<GameObject>("Prefabs/UI/SkillData 17");
                            break;
                        case 3:
                            Info = Resources.Load<GameObject>("Prefabs/UI/SkillData 18");
                            break;
                    }
                    break;
                default:
                   
                    break;
            }
            InfoScreen = Instantiate(Info, gameObject.transform);
            InfoScreen.transform.SetParent(transform.parent.parent.parent.parent.GetChild(6));
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Destroy(InfoScreen);
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