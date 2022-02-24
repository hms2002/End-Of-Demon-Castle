using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cross : MonoBehaviour
{
    Boss boss;
    BoxCollider2D collid;
    Animator anim;
    public int Damage = 50;
    BreakableObj breakableObj;
    bool isfire;
    Renderer Renderer;

    private void Awake()
    {
        collid = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        Renderer = GetComponent<Renderer>();
        collid.enabled = false;
    }

    private void Start()
    {
        StartCoroutine(ColliderOn());
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("zz");
        int layer = collision.gameObject.layer;
        if (layer == 12 && isfire)
        {
            if (boss == null)
                boss = collision.GetComponent<Boss>();

            boss.damaged(Damage + DamageControler.GetInstance().GetMonoDamage());
            collid.enabled = false;
            isfire = false;
        }
        else if (collision.CompareTag("CanBroke"))
        {
            Debug.Log("zz");
            breakableObj = collision.GetComponent<BreakableObj>();
            breakableObj.breakObj(Damage + DamageControler.GetInstance().GetMonoDamage());
            collid.enabled = false;
            isfire = false;
        }
    }

    public IEnumerator ColliderOn()
    {
        yield return new WaitForSeconds(0.1f);
        anim.SetTrigger("Ready");
        collid.enabled = true;
        isfire = true;
        int i = 100;
        while (i > 0)
        {
            i -= 1;
            float f = i / 20.0f;
            Color c = Renderer.material.color;
            c.a = f;
            Renderer.material.color = c;
            yield return new WaitForSeconds(0.001f);
        }
        Destroy(transform.root.gameObject);
    }
}
