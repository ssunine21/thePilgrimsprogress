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
    public Vector2 pos;
    public float moveSpeed;
    public float delayTime;
    public Animation anim;
}

public class ProductionManager : MonoBehaviour
{

    public Type type;
    public Objects[] objects;

    private Vector3 snap = Vector3.zero;


    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, Vector2.one);


        //if (objects.Length > 0) {
        //    for (int i = 0; i < objects.Length; ++i) {
        //        if (i == 0) objects[0].pos = this.transform.position;
        //        objects[i].pos = Handles.PositionHandle(objects[i].pos, Quaternion.identity);

        //        if (objects.Length - 1 == i) {
        //            break;
        //        } else
        //            Handles.DrawLine(objects[i].pos, objects[i + 1].pos);
        //    }
        //}

    }

    Vector2 PositionHandle(Vector2 transform) {

        var position = transform;
        var size = 1;

        Handles.color = Handles.xAxisColor;
        position = Handles.Slider(position, Vector3.right, size, Handles.ArrowHandleCap, snap.x);

        //Y축
        Handles.color = Handles.yAxisColor;
        position = Handles.Slider(position, Vector3.up, size, Handles.ArrowHandleCap, snap.y);

        return position;
    }
}