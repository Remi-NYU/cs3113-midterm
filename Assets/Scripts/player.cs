using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{

    public float groundspeed;

    public const float defaultAirSpeed = 5;
    public float defaultGravityScale = 1;
    public float defaultGroundSpeed = 10;
    public float jumpForce = 350;

    public LayerMask theGround;
    public Transform bottom;
    bool grounded = false;
    private float xSpeed;


    private float airspeed = 5;

    Rigidbody2D _rigidbody;
    abilities _abilities;

    Animator _animator;

    int state = 0;
    // Start is called before the first frame update
    void Start()
    { 
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _abilities = GetComponent<abilities>();

        airspeed = defaultAirSpeed;
        groundspeed = defaultGroundSpeed;
    }

    private bool isGliding = false;
    private bool isFastFalling = false;
    void FixedUpdate() {

        if(grounded && state == 0)
        {
            xSpeed = Input.GetAxis("Horizontal") * groundspeed;
            _rigidbody.velocity = new Vector2(xSpeed, _rigidbody.velocity.y);
        }
        else if (!grounded && state == 1)
        {
            // if falling, handle possible gliding...
            if (_rigidbody.velocity.y < 0f) {
                if (Input.GetAxis("Horizontal") != 0f && !isGliding) {
                    _abilities.handleGlideStart();
                    isGliding = true;
                } else if (Input.GetAxis("Horizontal") == 0f && isGliding) {
                    _abilities.handleGlideEnd();
                    isGliding = false;
                } 
            }

            xSpeed = Input.GetAxis("Horizontal") * airspeed;
            _rigidbody.velocity = new Vector2(xSpeed, _rigidbody.velocity.y);
        }
        // end gliding when grounded (reset airspeed and gravity)
        else if (grounded && isGliding) 
        {
            if (state == 1) { // if square, stop all x movement.
                _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
            }
            _abilities.handleGlideEnd();
            isGliding = false;
        }


        // start fast fall when in air and pressing down key 
        if (!grounded && Input.GetAxis("Vertical") < 0f) {
            _abilities.handleFastFallStart();
            isFastFalling = true;
        }
        // end fast fall when grounded.
        if (isFastFalling && grounded) {
            _abilities.handleFastFallEnd();
            isFastFalling = false;
        }
    }

    void Update()
    {

        grounded = Physics2D.OverlapCircle(bottom.position, 0.1f, theGround);

        if(Input.GetKeyDown(KeyCode.Space) && grounded && state == 1)
        {
            _rigidbody.AddForce(new Vector2(0, jumpForce));
        }
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            if(state == 0)
            {
                _animator.SetInteger("State", 1);
                state = 1;
            }
            else if(state == 1)
            {
                _animator.SetInteger("State", 0);
                state = 0;
            }
        }
    }



    public void setAirSpeed(float newAirSpeed) {
        airspeed = newAirSpeed;
    }
    public void resetAirSpeed() {
        airspeed = defaultAirSpeed;
    }
    public void setGravityScale(float newGravityScale) {
        _rigidbody.gravityScale = newGravityScale;
    }
    public void resetGravityScale() {
        _rigidbody.gravityScale = defaultGravityScale;
    }
    public bool getGroundedState() {
        return grounded;
    }
}
