using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlat : MonoBehaviour
{
    public bool activated;
    public float x_speed;
    public float y_speed;

    public bool backAndForth;
    public float changeDirInterval;

    private float timeSinceChangeDir = 0f;
    void FixedUpdate()
    {
        if (!activated) return;

        GetComponent<Rigidbody2D>().velocity = new Vector2(x_speed, y_speed);

        if (backAndForth) {
            timeSinceChangeDir += Time.deltaTime;
            if (timeSinceChangeDir >= changeDirInterval) {
                x_speed = -x_speed;
                y_speed = -y_speed;
                timeSinceChangeDir = 0f;
            }
        }
    }
}
