using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public enum Type // your custom enumeration
{
    Character,
    Camera,
    Object
};

public enum Production {
    Position,
    MoveSpeed,
    DelayTime,
    Script,
    Animation
}

[System.Serializable]
public class Objects {
    public GameObject gameObject;
    public ProductionArray[] productionArray;
}

[System.Serializable]
public class ProductionArray {
    public Vector3 pos;
    public float moveSpeed;
    public float delayTime;
    public Animation anim;
}

public class ProductionManager : MonoBehaviour
{

    public Type type;
    public Objects[] objects;
    public bool isStart = false;

    int i = 0;

    Vector2 finishPos;

    private Vector3 snap = Vector3.zero;

    private void Start() {
        finishPos = Vector2.zero;
    }


    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, Vector2.one);


        if (objects.Length > 0) {

            if (objects[0].productionArray.Length > 0) {

                for (int i = 0; i < objects.Length; ++i) {



                    for(int j = 0; j < objects[i].productionArray.Length; ++j) {
                        if (objects[i].productionArray.Length - 1 == j) {
                            objects[i].productionArray[j].pos = Handles.PositionHandle(objects[i].productionArray[j].pos, Quaternion.identity);
                            break;
                        } else
                            Handles.DrawLine(objects[i].productionArray[j].pos, objects[i].productionArray[j + 1].pos);

                        if (j == 0) {
                            objects[0].productionArray[0].pos = this.transform.position;
                            continue;
                        }
                        if (objects[i].productionArray[j].pos != Vector3.zero) {
                            objects[i].productionArray[j].pos = Handles.PositionHandle(objects[i].productionArray[j].pos, Quaternion.identity);
                        }

                    }

                    //if (objects[i].productionArray)
                    //    objects[i].productionArray = Handles.PositionHandle(objects[i].pos, Quaternion.identity);

                }
            }
        }
    }


    private void FixedUpdate() {
        if (isStart) {
            if (objects[0].productionArray.Length < i) return;

            if (Vector3.Distance(objects[0].gameObject.transform.position, objects[0].productionArray[i + 1].pos) >= 0.01f)
                objects[0].gameObject.transform.localPosition = Vector3.MoveTowards(objects[0].gameObject.transform.position, objects[0].productionArray[i + 1].pos, 8 * Time.deltaTime);
            else ++i;
        }
    }
}