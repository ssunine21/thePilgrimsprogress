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
public class ProductionManager : MonoBehaviour {
    private const float gizmoDiameter = 0.7f;

    [SerializeField]
    public List<GameObject> npcList;
    public ProductionType[] productionTypeList;
    public GameObject checkerImg = null;

    public string questNumber = "";

    public bool isCheck = false;
    public bool isStarting = false;

    // private bool division = false;



    private void Start() {
        if (checkerImg != null) {
            checkerImg.SetActive(false);
            isCheck = true;
        }

        setProduction();
    }

    private void setProduction() {
        if (productionTypeList.Length > 0) {
            foreach (var prodiction in productionTypeList) {
                if (prodiction.productionKey.Equals(ProductionKey.gameObject)) {
                    npcList.Add(prodiction.gameObject);
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

        foreach (var production in productionTypeList) {
            if (production.productionKey.Equals(ProductionKey.gameObject)) {
                tempPos = new Vector2(production.gameObject.transform.position.x, production.gameObject.transform.position.y);
            } else if (production.productionKey.Equals(ProductionKey.position)) {
                Handles.DrawLine(tempPos, production.pos);
                tempPos = production.pos;
            }
        }
    }

    private void FixedUpdate() {
        if (this.isStarting) {
            ProductionStart();

            this.isStarting = false;
        }
        //if (isStart) {
        //    if (objects[0].productionValue.Length < i) return;

        //    if (Vector3.Distance(objects[0].gameObject.transform.position, objects[0].productionValue[i + 1].pos) >= 0.01f)
        //        objects[0].gameObject.transform.localPosition = Vector3.MoveTowards(objects[0].gameObject.transform.position, objects[0].productionValue[i + 1].pos, 8 * Time.deltaTime);
        //    else ++i;
        //}
    }

    public void ProductionStart() {
        StopAllCoroutines();
        StartCoroutine("ProductionControl");
    }

    public void startProduction() {
        if (isCheck) {
            if (checkerImg.activeSelf == true) isStarting = true;
        } else {
            isStarting = true;
        }
    }

    IEnumerator ProductionControl() {
        ObjectControl currObject = null;
        int initMoveSpeed = 0;

        foreach (var production in productionTypeList) {
            switch (production.productionKey) {
                case ProductionKey.gameObject:
                    currObject = production.gameObject.GetComponent<ObjectControl>();

                    if (production.gameObject.GetComponent<FollowCam>() != null)
                        production.gameObject.GetComponent<FollowCam>().enabled = false;
                    break;

                case ProductionKey.position:
                    bool isMove = true;

                    Vector2 tempDir = (production.pos - (Vector2)currObject.tr.position);
                    currObject.moveDir = tempDir.normalized;

                    while (isMove) {
                        if (Vector2.Distance(currObject.tr.position, production.pos) <= 0.1f) {
                            isMove = false;
                            currObject.moveDir = Vector3.zero;
                        }
                        yield return null;
                    }
                    break;

                case ProductionKey.scriptNum:
                    try {
                        GameObject npcObject = searchNpc(QuestManager.init.InsertQuest(production.scriptNum));

                        if (npcObject)
                            npcObject.GetComponent<ObjectControl>().playTalkAnimation();

                    } catch (System.NullReferenceException e) {
                        Debug.LogError(e.Message + "QuestManagert script is null, check Canvas Object");
                    } catch (KeyNotFoundException e) {
                        Debug.LogError(e.Message + "해당 퀘스트의 script number 불일치");
                    }
                    while (DialogueManager.init.productionTalking) {
                        yield return null;
                    }
                    break;

                case ProductionKey.moveSpeed:
                    initMoveSpeed = currObject.moveSpeed;
                    currObject.moveSpeed = production.moveSpeed;
                    break;

                case ProductionKey.delayTime:
                    yield return new WaitForSeconds(production.delayTime);
                    break;

                case ProductionKey.anim:
                    break;

                case ProductionKey.nextQuest:
                    QuestManager.init.nextQuest(production.nextQuestNumber);
                    if (PlayerControl.init != null)
                        PlayerControl.init.systemControl(false);
                    if (production.gameObject.GetComponent<FollowCam>() != null)
                        production.gameObject.GetComponent<FollowCam>().enabled = true;
                    break;
            }
        }
        if (initMoveSpeed != 0)
            currObject.moveSpeed = initMoveSpeed;
    }

    private GameObject searchNpc(string name) {
        foreach(var npc in npcList) {
            if (npc.name.Equals(name))
                return npc;
        }

        return null;
    }
}