using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{

    public float groundspeed = 10;

    public float airspeed = 5;
    public float jumpForce = 350;

    public LayerMask theGround;
    public Transform bottom;
    bool grounded = false;
    private float xSpeed;

    Rigidbody2D _rigidbody;

    Animator _animator;

    int state = 0;
    // Start is called before the first frame update
    void Start()
    { 
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }


    void FixedUpdate() {
        if(grounded && state == 0)
        {
            xSpeed = Input.GetAxis("Horizontal") * groundspeed;
            _rigidbody.velocity = new Vector2(xSpeed, _rigidbody.velocity.y);
        }
        else if(!grounded && state == 1)
        {
            xSpeed = Input.GetAxis("Horizontal") * airspeed;
            _rigidbody.velocity = new Vector2(xSpeed, _rigidbody.velocity.y);
        }
        
    }
    // Update is called once per frame
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
}
