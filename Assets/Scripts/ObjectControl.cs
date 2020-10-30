using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectControl : Character {

    private void Start() {
        tr = this.transform;
        if (this.GetComponent<Animator>() != null)
            animator = this.GetComponent<Animator>();
    }

    private void FixedUpdate() {
        Move();
    }
}