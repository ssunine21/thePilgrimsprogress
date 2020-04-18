using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : Character
{
    public static PlayerControl init;

    private void Awake() {
        if(init == null) {
            init = this;
        }
    }

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
        //float h = joystick.GetHorizontal();
        //float v = joystick.GetVertical();
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        moveDir = new Vector2(h, v).normalized;
    }
}
