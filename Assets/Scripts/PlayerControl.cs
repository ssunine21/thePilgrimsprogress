using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : Character
{
    public static PlayerControl init;

    private float h;
    private float v;

    private bool _isSystemControl;
    public bool isSystemControl {
        get => _isSystemControl;
        set {
            _isSystemControl = value;
        }
    }

    private void Awake() {
        if(init == null) {
            init = this;
        }
    }

    private void Start() {
        animator = GetComponent<Animator>();

        moveDir = Vector3.zero;
        tr = this.transform;
        isSystemControl = false;
    }

    private void Update() {
        HandleInput();
    }

    private void FixedUpdate() {
        Move();
    }

    public void setDirect(Vector2 direct) {
        this.h = direct.x;
        this.v = direct.y;
    }

    public Vector2 getDirect() {
        return new Vector2(this.h, this.v);
    }

    public void HandleInput() {
        //float h = joystick.GetHorizontal();
        //float v = joystick.GetVertical();

        if (!isSystemControl) {
            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");
        }

        moveDir = new Vector2(h, v).normalized;
    }

}
