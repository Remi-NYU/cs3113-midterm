using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abilities : MonoBehaviour
{
    public player player;

    public bool glidingEnabled;
    public bool fastFallEnabled;

    public float glidingAirSpeed = 8f;
    public float glidingGravityScale = 0.15f;
    public float fastFallGravityScale = 10f;

    // called after a KeyDown event for moving left/right while in air.
    public void handleGlideStart() 
    {
        // check if player has gliding ability.
        if (!glidingEnabled) {
            Debug.Log("You cannot glide yet...");
            return;
        }

        // start airgliding.
        //      1. change airspeed
        //      2. and gravity scale.
        player.setAirSpeed(glidingAirSpeed);
        player.setGravityScale(glidingGravityScale);
        player.isGliding = true;
    }

    // called after a KeyUp event for left/right movement and in air or when player is grounded.
    public void handleGlideEnd() 
    {
        player.resetAirSpeed();
        player.resetGravityScale();
        player.isGliding = false;
    }

    public void handleFastFallStart()
    {
        if (!fastFallEnabled) {
            Debug.Log("You cannot fast fall yet...");
            return;
        }
        player.setGravityScale(fastFallGravityScale);
        player.isFastFalling = true;
    }

    public void handleFastFallEnd()
    {
        player.resetGravityScale();
        player.isFastFalling = false;
    }
}
