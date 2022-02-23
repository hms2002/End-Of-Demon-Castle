using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour
{
    public float rotateSpeed;
    GameObject VenomAOE;

    void Start()
    {
        rotateSpeed = 1f;
        VenomAOE  = Resources.Load<GameObject>("Prefabs/VenomAOE");
    }

    void Update()
    {
        PlayerSkill_Venom VenomLogic = FindObjectOfType<PlayerSkill_Venom>();
        transform.Rotate(0, 0, 1800 * Time.deltaTime);
        if(Vector2.Distance(VenomLogic.playerDir, transform.position) >= 8f)
        {
            Instantiate(VenomAOE, transform.position + new Vector3(0,0.5f), Quaternion.Euler(new Vector3(0, 0, 0)));
            StartCoroutine("delay");
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        int Layer = collision.gameObject.layer;
        if (collision.CompareTag("CanBroke"))
        {
            Instantiate(VenomAOE, transform.position, Quaternion.Euler(new Vector3(0,0,0)));
            StartCoroutine("delay");
            Destroy(gameObject);
        }
        if (Layer == 12)
        {
            Instantiate(VenomAOE, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            StartCoroutine("delay");
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Border")
        {   
            Instantiate(VenomAOE, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            StartCoroutine("delay");
            Destroy(gameObject);
        }
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(0.05f);
    }
}
