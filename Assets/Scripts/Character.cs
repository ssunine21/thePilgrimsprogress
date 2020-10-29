using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    private const int IsometricRangePerYUnit = 100;
    private readonly int walkAnimationId = Animator.StringToHash("Walk");
    private readonly int talkAnimationId = Animator.StringToHash("Talk");

    public int moveSpeed;

    protected Vector3 moveDir;

    protected Transform tr;
    protected Animator animator;

    protected void Move() {

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

        if (animator)
            animator.SetFloat(walkAnimationId, value: Mathf.Abs(xAxis) + Mathf.Abs(yAxis));
    }

    protected void playTalkAnimation() {
        if (animator)
            animator.SetTrigger(talkAnimationId);
    }

}
