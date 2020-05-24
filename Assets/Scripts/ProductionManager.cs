using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public enum ProductionKey {
    GameObject,
    pos,
    moveSpeed,
    delayTime,
    anim
}
[System.Serializable]
public class ProductionType {
    public ProductionKey productionKey;
    public GameObject gameObject;
    public Vector3 pos;
    public float moveSpeed;
    public float delayTime;
    public Animation anim;
}
[System.Serializable]
public class ProductionArray {
    public ProductionKey key;
    public ProductionType value;
}


public class ProductionManager : MonoBehaviour
{
    public ProductionType[] productionType;
    public bool isStart = false;

    //private int i = 0;
    //private Vector2 finishPos;
    //private Vector3 snap = Vector3.zero;

    private void Start() {
        //finishPos = Vector2.zero;
    }


    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, Vector2.one);


        //if (objects.Length > 0) {

        //    if (objects[0].productionValue.Length > 0) {

        //        for (int i = 0; i < objects.Length; ++i) {



        //            for(int j = 0; j < objects[i].productionValue.Length; ++j) {
        //                if (objects[i].productionValue.Length - 1 == j) {
        //                    objects[i].productionValue[j].pos = Handles.PositionHandle(objects[i].productionValue[j].pos, Quaternion.identity);
        //                    break;
        //                } else
        //                    Handles.DrawLine(objects[i].productionValue[j].pos, objects[i].productionValue[j + 1].pos);

        //                if (j == 0) {
        //                    objects[0].productionValue[0].pos = this.transform.position;
        //                    continue;
        //                }
        //                if (objects[i].productionValue[j].pos != Vector3.zero) {
        //                    objects[i].productionValue[j].pos = Handles.PositionHandle(objects[i].productionValue[j].pos, Quaternion.identity);
        //                }

        //            }

        //            //if (objects[i].productionArray)
        //            //    objects[i].productionArray = Handles.PositionHandle(objects[i].pos, Quaternion.identity);

        //        }
        //    }
        //}
    }


    private void FixedUpdate() {
        //if (isStart) {
        //    if (objects[0].productionValue.Length < i) return;

        //    if (Vector3.Distance(objects[0].gameObject.transform.position, objects[0].productionValue[i + 1].pos) >= 0.01f)
        //        objects[0].gameObject.transform.localPosition = Vector3.MoveTowards(objects[0].gameObject.transform.position, objects[0].productionValue[i + 1].pos, 8 * Time.deltaTime);
        //    else ++i;
        //}
    }
}