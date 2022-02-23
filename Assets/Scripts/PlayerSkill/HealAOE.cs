using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAOE : MonoBehaviour
{
    GameObject healSkill;
    GameObject tempHeal;

    PolygonCollider2D polygon;

    public Player player;

    float delayTime = 1f;
    float timer = 0;

    float amountOfRecovery = 10f;

    private void Awake()
    { 
        healSkill = Resources.Load<GameObject>("Prefabs/Heal");
        polygon = GetComponent<PolygonCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (timer <= 0)
            {

                SoundManager.GetInstance().Play("Sound/PlayerSound/SkillSound/HealSound", 0.3f);

                tempHeal = Instantiate(healSkill);
                tempHeal.transform.position = player.transform.position;
                player.player_hp += amountOfRecovery;
                Destroy(tempHeal, 0.5f);

                timer = delayTime;
            }
        }
    }

    void Update()
    {
        /////Debug.Log(timer);
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            polygon.enabled = true;
        }
        else
        {
            polygon.enabled = false;
        }
    }
}
