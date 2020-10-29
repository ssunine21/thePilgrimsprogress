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
    scriptNum,
    division,
    nextQuest
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
    public string nextQuestNumber;
}

public enum PROPERTIES {
    BLOCK = 0,
    NPC = 1
}

[RequireComponent(typeof(Collider2D))]
public class ProductionManager : MonoBehaviour
{
    private const float gizmoDiameter = 0.7f;

    [SerializeField]
    public List<GameObject> npcList;
    public string questNumber = "";

    public ProductionType[] productionType;
    public bool isCheck = false;
    public bool isStarting = false;

    public GameObject checkerImg = null;
    private bool division = false;

    private List<ObjectControl> tempObjects = new List<ObjectControl>();
    

    private void Start() {
        if (checkerImg != null) {
            checkerImg.SetActive(false);
            isCheck = true;
        }
    }

    private void setProduction() {
        ObjectControl tempObject = null;

        if (productionType.Length > 0) {
            foreach (var prodiction in productionType) {
                if (prodiction.productionKey.Equals(ProductionKey.division)) division = true;

                if (prodiction.productionKey.Equals(ProductionKey.gameObject)) {
                    if (!division) {
                        prodiction.gameObject.GetComponent<ObjectControl>().preObject = tempObject;
                    }

                    tempObject = prodiction.gameObject.GetComponent<ObjectControl>();
                    tempObject.productionTypeList = new List<ProductionType>();
                    tempObject.productionTypeList.Add(prodiction);
                    tempObjects.Add(tempObject);
                    division = false;

                } else {
                    tempObject.productionTypeList.Add(prodiction);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.CompareTag("Player")) {
            if (isCheck)
                checkerImg.SetActive(true);
            else {
                this.isStarting = true;
                PlayerControl.init.systemControl(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.transform.CompareTag("Player")) {
            if (isCheck) checkerImg.SetActive(false);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, gizmoDiameter);
    }

    void OnDrawGizmosSelected() {
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
        if(this.isStarting) {

            setProduction();

            foreach(var gameObject in tempObjects) {
                gameObject.ProductionStart();
            }
            this.isStarting = false;
        }
        //if (isStart) {
        //    if (objects[0].productionValue.Length < i) return;

        //    if (Vector3.Distance(objects[0].gameObject.transform.position, objects[0].productionValue[i + 1].pos) >= 0.01f)
        //        objects[0].gameObject.transform.localPosition = Vector3.MoveTowards(objects[0].gameObject.transform.position, objects[0].productionValue[i + 1].pos, 8 * Time.deltaTime);
        //    else ++i;
        //}
    }

    public void startProduction() {
        if (isCheck) {
            if (checkerImg.activeSelf == true) isStarting = true;
        } else {
            isStarting = true;
        }
    }
}