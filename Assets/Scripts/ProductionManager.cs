using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public enum ProductionKey {
    gameObject,
    position,
    moveSpeed,
    delayTime,
    anim,
    scriptNum
}
[System.Serializable]
public class ProductionType {

    public ProductionKey productionKey;
    public GameObject gameObject;
    public Vector2 pos;
    public string scriptNum;
    public int moveSpeed;
    public float delayTime;
    public Animation anim;
}

public class ProductionManager : MonoBehaviour
{
    public ProductionType[] productionType;
    public bool isStart = false;

    private List<ObjectControl> tempObjects = new List<ObjectControl>();


    //private int i = 0;
    //private Vector2 finishPos;
    //private Vector3 snap = Vector3.zero;

    private void Start() {
        ObjectControl tempObject = null;

        if(productionType.Length > 0) {
            foreach(var prodiction in productionType) {
                if (prodiction.productionKey.Equals(ProductionKey.gameObject)) {
                    tempObject = prodiction.gameObject.GetComponent<ObjectControl>();
                    tempObject.productionTypeList = new List<ProductionType>();
                    tempObjects.Add(tempObject);
                } else {
                    tempObject.productionTypeList.Add(prodiction);
                    tempObject.setDebug();
                }
            }
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, Vector2.one);
        Vector2 tempPos = Vector2.zero;

        foreach (var production in productionType) {
            if (production.productionKey.Equals(ProductionKey.gameObject)) {
                tempPos = new Vector2(production.gameObject.transform.position.x, production.gameObject.transform.position.y);
            } else if (production.productionKey.Equals(ProductionKey.position)) {
                Handles.DrawLine(tempPos, production.pos);
                tempPos = production.pos;
            }
        }
    }


    private void FixedUpdate() {
        if(isStart) {
            foreach(var gameObject in tempObjects) {
                gameObject.ProductionStart();
            }
            isStart = false;
        }
        //if (isStart) {
        //    if (objects[0].productionValue.Length < i) return;

        //    if (Vector3.Distance(objects[0].gameObject.transform.position, objects[0].productionValue[i + 1].pos) >= 0.01f)
        //        objects[0].gameObject.transform.localPosition = Vector3.MoveTowards(objects[0].gameObject.transform.position, objects[0].productionValue[i + 1].pos, 8 * Time.deltaTime);
        //    else ++i;
        //}
    }
}