using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int moveSpeed;
    public Vector3 moveDir;

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
        float yAxis = moveDir.y;

        Vector3 characterDirection = tr.localScale;

        if (xAxis > 0f) {
            characterDirection.x = -1;
            //moveDir.x *= -1f;
        } else if (xAxis < 0) {
            characterDirection.x = 1;
        }
        tr.localScale = characterDirection;
        tr.Translate(moveDir * moveSpeed * Time.deltaTime);

        if (animator) animator.SetFloat("Walk", value: Mathf.Abs(xAxis) + Mathf.Abs(yAxis));
        else Debug.Log("noAnimation");
    }
}
