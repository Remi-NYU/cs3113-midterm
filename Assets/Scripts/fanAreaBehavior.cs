using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fanAreaBehavior : MonoBehaviour
{


    public GameObject parent;
    public float blowForceMagnitude;

    private float myHeight;
    private void Start()
    {
        myHeight = transform.localScale.y;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
        if (rb)
        {
            float playerY = collision.transform.position.y;
            float platformY = parent.transform.position.y;
            float blowForce = ( 1 - (playerY - platformY) / myHeight ) * blowForceMagnitude;
            blowForce = Mathf.Max(0, blowForce);
            rb.AddForce(new Vector2(0, blowForce), ForceMode2D.Impulse);
        }
    }
}
