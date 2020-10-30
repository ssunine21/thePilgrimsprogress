using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    private const int IsometricRangePerYUnit = 100;
    private readonly int walkAnimId = Animator.StringToHash("Walk");
    private readonly int talkAnimId = Animator.StringToHash("Talk");

    public int moveSpeed;

    protected Vector3 _moveDir;
    public Vector3 moveDir {
        get => _moveDir;
        set => _moveDir = value;
    }

    protected Transform _tr;
    public Transform tr {
        get => _tr;
        set => _tr = value;
    }

    protected Animator animator;

    protected void Move() {

        float xAxis = _moveDir.x;
        float yAxis = _moveDir.y;

        Vector3 characterDirection = tr.localScale;

        if (xAxis > 0f) {
            characterDirection.x = -1;
        } else if (xAxis < 0) {
            characterDirection.x = 1;
        }
        tr.localScale = characterDirection;
        tr.Translate(_moveDir * moveSpeed * Time.deltaTime);

        if (animator)
            animator.SetFloat(walkAnimId, value: Mathf.Abs(xAxis) + Mathf.Abs(yAxis));
    }

    protected void playTalkAnimation() {
        if (animator)
            animator.SetTrigger(talkAnimId);
    }

}
