using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int moveSpeed;

    protected Vector3 moveDir;
    protected Transform tr;
    protected Animator animator;

    public void Move() {

        float xAxis = moveDir.x;
        float yAxis = moveDir.y;

        Vector3 characterDirection = tr.localScale;

        if (xAxis > 0f) {
            characterDirection.x = -1;
        } else if (xAxis < 0) {
            characterDirection.x = 1;
        }
        tr.localScale = characterDirection;
        tr.Translate(moveDir * moveSpeed * Time.deltaTime);

        if (animator) animator.SetFloat("Walk", value: Mathf.Abs(xAxis) + Mathf.Abs(yAxis));
        else Debug.Log("noAnimation");
    }
}
