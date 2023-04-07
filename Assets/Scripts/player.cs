using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{

    // global constant variables.
    private const int MARBLE = 0;
    private const int SPRING = 1;
    private const int GLIDER = 2;
    private const int HEAVY = 3;


    // adjustable variables.
    public float defaultAirSpeed = 5;
    public float defaultGravityScale = 1;
    public float defaultGroundSpeed = 10;
    public float jumpForce = 350;
    public bool automaticallySwitchToPrevGroundState = true;
    public bool useDownForFastFall = true;
    public bool allowSpringGlide = false;
    public float deathYVal = -1000;

    public LayerMask theGround;
    public Transform bottom;

    public GameObject image;

    public GameObject transform_particles;
    Rigidbody2D _rigidbody;
    abilities _abilities;
    Animator _animator;



    ControlWrapper controls;

    // non-adjustable variables.
    int state = 0;
    public bool isGliding = false;
    public bool isFastFalling = false;
    private int prevGroundState = MARBLE;
    public float groundspeed;
    private float airspeed;
    bool grounded = false;




    void Start()
    { 
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _abilities = GetComponent<abilities>();
        

        airspeed = defaultAirSpeed;
        groundspeed = defaultGroundSpeed;

        controls = new ControlWrapper();
        image.GetComponent<Image>().color = new Color(0, 1.0f, 0, 0.5f);
    }



    void FixedUpdate() {
        float hrInput = Input.GetAxis("Horizontal");
        bool vertInput = controls.JumpButton();

        switch(state) {

            case MARBLE:
                if (!grounded) break;
                if (hrInput == 0)
                {
                    _animator.SetBool("moving", false);
                    break;
                } 
                _rigidbody.velocity = new Vector2(hrInput * groundspeed, _rigidbody.velocity.y);
                _animator.SetBool("moving", true);
                break;


            case SPRING:
                if (grounded) 
                {
                    _animator.SetBool("grounded", true);
                    if(vertInput)
                    {
                        _rigidbody.AddForce(new Vector2(0, jumpForce));
                        _animator.SetBool("grounded", false);
                    }
                }
                if (!grounded && hrInput != 0 && allowSpringGlide) {
                    _rigidbody.velocity = new Vector2(hrInput * airspeed, _rigidbody.velocity.y);
                }
                break;


            case GLIDER:
                if (grounded) {
                    if (automaticallySwitchToPrevGroundState) handleStateSwitch(prevGroundState);
                    _animator.SetBool("grounded", true);
                    break;
                } else { // airborne...
                    _animator.SetBool("grounded", false);
                    if (_rigidbody.velocity.y <= 0f) {
                        // all conditions are met! start gliding phase.
                        _abilities.handleGlideStart();
                    } else {    // player should not be able to glide when elevating.
                        if (isGliding) _abilities.handleGlideEnd();
                    }
                }
                if (hrInput != 0) _rigidbody.velocity = new Vector2(hrInput * airspeed, _rigidbody.velocity.y);
                break;


            case HEAVY:
                if (grounded) {
                    if (automaticallySwitchToPrevGroundState) {
                        handleStateSwitch(prevGroundState);
                        break;
                    }
                } else {
                    if (!isFastFalling) _abilities.handleFastFallStart();
                }
                break;
            

            default:
                break;
        }
    }



    private void handleStateSwitch(int newState) {
        if (state == newState) return;
        if (state == MARBLE || state == SPRING) {
            prevGroundState = state;
        }
        if (state == GLIDER) _abilities.handleGlideEnd();
        if (state == HEAVY) _abilities.handleFastFallEnd();
        _animator.SetInteger("State", newState);
        Vector3 partpos = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
        Instantiate(transform_particles, partpos, Quaternion.identity);
        state = newState;
        switch(state) 
        {
            case MARBLE:
                image.GetComponent<Image>().color = new Color(0, 1.0f, 0, 1.0f);
                break;
            case SPRING:
                image.GetComponent<Image>().color = new Color(0.8f, 0, 0, 1.0f);
                break;
            case GLIDER:
                image.GetComponent<Image>().color = new Color(0, 0, 1.0f, 1.0f);
                break;
            case HEAVY:
                image.GetComponent<Image>().color = new Color(0.9f, 0.45f, 0, 1.0f);
                break;
            default:
                break;
        }
    }

    private void handleDeath()
    {
        // lock player in place.
        _rigidbody.bodyType = RigidbodyType2D.Static;

        // play death animation.
        _animator.SetBool("dead", true);

        // handle scene change.
        StartCoroutine(WaitForSceneReload());
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator WaitForSceneReload()
    {

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }


    void Update()
    {
        if (gameObject.transform.position.y <= deathYVal) handleDeath();


        grounded = Physics2D.OverlapCircle(bottom.position, 0.2f, theGround) && (_rigidbody.velocity.y == 0f);
        if (grounded) {
            if (isFastFalling) _abilities.handleFastFallEnd();
            if (isGliding) _abilities.handleGlideEnd();
        }
        if (controls.Mode_Move()) handleStateSwitch(MARBLE);
        if (controls.Mode_Jump()) handleStateSwitch(SPRING);
        if (controls.Mode_Glide()) handleStateSwitch(GLIDER);
        if (controls.Mode_Fall()) handleStateSwitch(HEAVY);

        // if the option is toggled, support down key (S on keyboard) for fast fall.
        if (useDownForFastFall && Input.GetAxis("Vertical") < 0f) handleStateSwitch(HEAVY);
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
