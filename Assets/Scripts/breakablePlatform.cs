using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakablePlatform : MonoBehaviour
{
    public GameObject leftPiece;
    public GameObject rightPiece;
    public float destroyAfter = 3f;

    public float velocityThreshold = 50;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude > velocityThreshold) {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;

            leftPiece.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            leftPiece.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 1), ForceMode2D.Impulse);
            rightPiece.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            rightPiece.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 1), ForceMode2D.Impulse);

            Object.Destroy(gameObject, destroyAfter);
        }
    }
}
