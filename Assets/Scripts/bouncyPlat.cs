using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bouncyPlat : MonoBehaviour
{
    public float bounciness; // Determines jump height
    public bool equalAndOpposite;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (equalAndOpposite) {
            other.rigidbody.velocity = new Vector2(other.relativeVelocity.x, -other.relativeVelocity.y);
            return;
        }

        // other.rigidbody.AddForce(new Vector2(-other.relativeVelocity.x, other.relativeVelocity.y) * bounciness, ForceMode2D.Impulse);
        // set x value of new force vector to 0.0f, if you need the jump was straight up
        if (other.rigidbody) {
            other.rigidbody.AddForce(new Vector2(0, bounciness), ForceMode2D.Impulse);
        }
    }
}
