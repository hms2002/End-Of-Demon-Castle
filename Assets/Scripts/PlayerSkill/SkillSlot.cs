using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class SkillSlot : MonoBehaviour, IDropHandler
{
    public Skill_ID skill;
    public Image data;
    public DragAndDropCantainer dragAndDropContainer;
    public DragPlus beforeDrag;

    private void Start() {
        data = GetComponent<Image>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (dragAndDropContainer.image.sprite != null)
        {
            data.sprite = dragAndDropContainer.image.sprite;
            skill = dragAndDropContainer.skill;

            if(beforeDrag != null)
                beforeDrag.OnAir();

            beforeDrag = skill.gameObject.GetComponent<DragPlus>();


            dragAndDropContainer.image.sprite = null;
        }
        else
        {
        }
    }
}
