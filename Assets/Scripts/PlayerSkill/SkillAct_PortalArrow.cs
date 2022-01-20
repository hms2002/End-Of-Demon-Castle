using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAct_PortalArrow : MonoBehaviour
{
    ObjectManager objectManager;
    int arrowSpeed;

    private void Awake() {
        objectManager = FindObjectOfType<ObjectManager>();
        arrowSpeed = 15;
    }
    public void Shooting(float angle)
    {
        StartCoroutine("IShooting", angle);
        Destroy(gameObject, 5);
    }

    IEnumerator IShooting(float angle)
    {
        while(true)
        {
            GameObject arrow = objectManager.MakeObj("Arrow");

            Rigidbody2D arrowRigid = arrow.GetComponent<Rigidbody2D>();

            arrow.transform.position = transform.position + new Vector3(Random.Range(-0.4f, 0.4f), Random.Range(-0.4f, 0.4f));

            Quaternion v3Rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            arrow.transform.rotation = v3Rotation;
            
            Vector2 dir = (v3Rotation * -Vector2.left);

            arrowRigid.AddForce(new Vector2(dir.x, dir.y) * arrowSpeed, ForceMode2D.Impulse);

            yield return new WaitForSeconds(0.3f);
        }
    }
}
