using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls
{
    private KeyCode upKey;
    private KeyCode downKey;
    public KeyCode GetUpKey() => upKey;
    public KeyCode GetDownKey() => downKey;
    public Controls(KeyCode up, KeyCode down)
    {
        upKey = up;
        downKey = down;
    }
    public bool IfKeyHeld(KeyCode key)
    {
        if (Input.GetKey(key)) return true;
        return false;
    }
}
