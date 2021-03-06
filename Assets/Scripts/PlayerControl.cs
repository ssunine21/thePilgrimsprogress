﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    private static PlayerControl _init;
    public static PlayerControl init {
        get {
            if (_init == null)
                _init = new PlayerControl();
            return _init;
        }
        set { if (_init == null)
                _init = value;
        }
    }
    private bool _isSystemControl = false;
    public bool isSystemControl {
        get => _isSystemControl;
        set {
            _isSystemControl = value;
        }
    }

    private ObjectControl objectControl;

    private float h;
    private float v;

    private void Awake() {
        if (init == null)
            init = this;

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start() {
        objectControl = this.GetComponent<ObjectControl>();
    }

    private void Update() {
        if(!isSystemControl)
            HandleInput();
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

        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        objectControl.moveDir = new Vector2(h, v).normalized;
    }

    private void GetKeyDown() {
        if (Input.GetKeyDown(KeyCode.Space)) {

        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.CompareTag("NPC")) {
            if (Input.GetKeyDown(KeyCode.Space) && collision.GetComponent<ProductionManager>().isCheck) {
                collision.GetComponent<ProductionManager>().startProduction();
            }
        }
    }

    public void systemControl(bool isControl) {
        if (isControl) {
            isSystemControl = true;
            objectControl.moveDir = Vector2.zero;
        } else {
            isSystemControl = false;
        }
    }
}
