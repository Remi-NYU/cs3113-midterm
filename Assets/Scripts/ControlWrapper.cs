using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlWrapper : MonoBehaviour
{
    // Start is called before the first frame update
    public bool Mode_Move()
    {
        bool inputter = false;
        if(Input.GetKey("1"))
        {
            inputter = true;
            //Debug.Log("Keyboard!");
        }
        if(Input.GetKey("joystick button 4"))
        {
            inputter = true;
            //Debug.Log("GamePad!");
        }
        return inputter;
    }

    public bool Mode_Jump()
    {
        bool inputter = false;
        if(Input.GetKey("2"))
        {
            inputter = true;
            //Debug.Log("Keyboard!");
        }
        if(Input.GetKey("joystick button 5"))
        {
            inputter = true;
            //Debug.Log("GamePad!");
        }
        return inputter;
    }

    public bool Mode_Glide()
    {
        bool inputter = false;
        if(Input.GetKey("3"))
        {
            inputter = true;
            Debug.Log("Keyboard 3!");
        }
        if(Input.GetAxis("Left Trigger") > 0)
        {
            inputter = true;
            Debug.Log("GamePad Left Trigger!");
        }
        return inputter;
    }

    public bool Mode_Fall()
    {
        bool inputter = false;
        if(Input.GetKey("4"))
        {
            inputter = true;
            Debug.Log("Keyboard 4!");
        }
        if(Input.GetAxis("Right Trigger") > 0)
        {
            inputter = true;
            Debug.Log("GamePad Right Trigger!");
        }
        return inputter;
    }
}
