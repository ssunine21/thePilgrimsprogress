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

    public Production production;
    public List<ProductionList> productionList = new List<ProductionList>();

    ProductionManager() {
        productionList[0].pos = this.transform.position;
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, Vector2.one);

        for (int i = 0; i < productionList.Count; ++i) {
            if (i == 0) productionList[0].pos = this.transform.position;

            if (productionList.Count - 1 == i) {
                productionList[i].pos = Handles.PositionHandle(productionList[i].pos, Quaternion.identity);
                break;
            } else
                Handles.DrawLine(productionList[i].pos, productionList[i + 1].pos);
        }

    }
}