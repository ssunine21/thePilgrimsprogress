using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [ExecuteInEditMode]
    public Transform target;
    public Vector3 offset;
    public float followSpeed;

    private void FixedUpdate() {
        if (target) {
            transform.position = Vector3.Lerp(transform.position, target.position + offset, followSpeed * Time.deltaTime);
        }
    }
}