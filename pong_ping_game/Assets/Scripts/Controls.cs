using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls
{
    //this class is simply for simplicity. wasnt even neccessary but ok
    private KeyCode upKey;
    private KeyCode downKey;
    public KeyCode GetUpKey() => upKey;
    public KeyCode GetDownKey() => downKey;
    //constructor to setup a new control scheme easily. Player constructor takes Controls as a parameter.
    public Controls(KeyCode up, KeyCode down)
    {
        upKey = up;
        downKey = down;
    }
    //detect if any key is being held.
    public bool IfKeyHeld(KeyCode key)
    {
        if (Input.GetKey(key)) return true;
        return false;
    }
}
