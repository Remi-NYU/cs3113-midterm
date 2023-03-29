using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fanAreaBehavior : MonoBehaviour
{


    public GameObject parent;
    public float blowForceMagnitude;

    public string blowDirection = "up";

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
            float playerX = collision.transform.position.x;
            float platformX = parent.transform.position.x;

            float blowForce;
            switch (blowDirection) {
                case "up":
                    blowForce = ( 1 - (playerY - platformY) / myHeight ) * blowForceMagnitude;
                    blowForce = Mathf.Max(0, blowForce);
                    break;
                case "down":
                    blowForce = ( 1 - (platformY - playerY) / myHeight ) * blowForceMagnitude;
                    blowForce = Mathf.Min(0, blowForce);
                    break;
                case "left":
                    blowForce = ( 1 - (platformX - playerX) / myHeight ) * blowForceMagnitude;
                    blowForce = Mathf.Min(0, blowForce);
                    break;
                case "right":
                    blowForce = ( 1 - (playerX - platformX) / myHeight ) * blowForceMagnitude;
                    blowForce = Mathf.Max(0, blowForce);
                    break;
                default:
                    blowForce = ( 1 - (playerY - platformY) / myHeight ) * blowForceMagnitude;
                    blowForce = Mathf.Max(0, blowForce);
                    break;
            }

            blowForce = Mathf.Max(0, blowForce);

            switch (blowDirection) {
                case "up":
                    rb.AddForce(new Vector2(0, blowForce), ForceMode2D.Impulse);
                    break;
                case "down":
                    rb.AddForce(new Vector2(0, blowForce), ForceMode2D.Impulse);
                    break;
                case "left":
                    rb.AddForce(new Vector2(blowForce, 0), ForceMode2D.Impulse);
                    break;
                case "right":
                    rb.AddForce(new Vector2(blowForce, 0), ForceMode2D.Impulse);
                    break;
                default:
                    rb.AddForce(new Vector2(0, blowForce), ForceMode2D.Impulse);
                    break;
            }           

        }
    }
}
