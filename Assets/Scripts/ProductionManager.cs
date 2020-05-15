using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum Production // your custom enumeration
{
    Character,
    Camera,
    Object
};

[System.Serializable]
public class ProductionList {
    public Vector2 pos;
    public float moveSpeed;
    public float delayTime;
    public Animation anim;
}

public class ProductionManager : MonoBehaviour
{
    [SerializeField]
    private Vector3 m_TargetPosition = new Vector3(1f, 0f, 2f);
    public Production production;
    public List<ProductionList> productionList = new List<ProductionList>();

    private Vector3 snap;

    ProductionManager() {
        productionList[0].pos = this.transform.position;
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, Vector2.one);

        for (int i = 0; i < productionList.Count; ++i) {
            if (i == 0) productionList[0].pos = this.transform.position;
            productionList[i].pos = Handles.PositionHandle(productionList[i].pos, Quaternion.identity);

            if (productionList.Count - 1 == i) {
                break;
            } else
                Handles.DrawLine(productionList[i].pos, productionList[i + 1].pos);
        }

    }

    public virtual void Update() {
        transform.LookAt(m_TargetPosition);
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