using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    public PlayerControls PlayerControls { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        PlayerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        PlayerControls.Enable();
    }

    private void OnDisable()
    {
        PlayerControls.Disable();
    }
}
