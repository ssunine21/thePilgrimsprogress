using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : Character
{
    public Joystick joystick;

    private void Start() {
        animator = GetComponent<Animator>();

        moveDir = Vector3.zero;
        tr = this.transform;
    }

    private void Update() {
        HandleInput();
    }

    private void FixedUpdate() {
        Move();
    }

    public void HandleInput() {
        float h = joystick.GetHorizontal();
        float v = joystick.GetVertical();

        moveDir = new Vector3(h, v, 0).normalized;
    }
}
