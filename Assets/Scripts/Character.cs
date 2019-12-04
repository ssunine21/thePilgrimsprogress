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



        //Vector3 eulerAngles = transform.localEulerAngles;

        //if (xAxis < 0f) {
        //    eulerAngles.y = 180f;
       // } else if (xAxis > 0f) {
      //      eulerAngles.y = 0f;
      //  }

      //  transform.localRotation = Quaternion.Euler(eulerAngles);

       // animator.SetFloat("Forward", Mathf.Abs(xAxis));

        float xAxis = moveDir.x;
        Vector3 eulerAngles = tr.localEulerAngles;

        if (xAxis > 0f) {
            eulerAngles.y = 180;
            moveDir.x *= -1f;
        } else if (xAxis < 0f) eulerAngles.y = 0f;

        tr.localRotation = Quaternion.Euler(eulerAngles);
        tr.Translate(moveDir * moveSpeed * Time.deltaTime);

        animator.SetFloat("Walk", value: Mathf.Abs(xAxis));
    }
}
