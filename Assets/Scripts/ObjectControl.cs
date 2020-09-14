using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectControl : Character {

    private bool isProductionStop = false;
    private bool isMove = false;
    private int initMoveSpeed = 0;

    [HideInInspector]
    public List<ProductionType> productionTypeList;
    [HideInInspector]
    public ObjectControl preObject;
    [HideInInspector]
    public bool isDialogue = false;

    public Vector2 _moveDir {
        get => moveDir;
        set {
            moveDir = value;
        }
    }

    private void Start() {
        tr = this.transform;
        if (this.GetComponent<Animator>() != null) animator = this.GetComponent<Animator>();
        initMoveSpeed = moveSpeed;
    }

    public void ProductionStart() {
        StopAllCoroutines();
        StartCoroutine("ProductionControl");
    }

    private void FixedUpdate() {
        Move();
    }

    IEnumerator ProductionControl() {

        if (preObject != null) {
            while (!preObject.isProductionStop) {
                yield return null;
            }

            preObject.isProductionStop = false;
        }

        

        foreach(var production in productionTypeList) {
            switch (production.productionKey) {
                case ProductionKey.gameObject:
                    if (this.GetComponent<FollowCam>() != null)
                        this.GetComponent<FollowCam>().enabled = false;
                    else if (this.GetComponent<PlayerControl>() != null)
                        this.GetComponent<PlayerControl>().enabled = false;
                    break;

                case ProductionKey.position:
                    isMove = true;
                    Vector2 tempDir = (production.pos - (Vector2)tr.position);
                    moveDir = tempDir.normalized;

                    while (isMove) {
                        if (Vector2.Distance(tr.position, production.pos) <= 0.1f) {
                            isMove = false;
                            moveDir = Vector3.zero;
                        }

                        yield return null;
                    }
                    break;

                case ProductionKey.scriptNum:
                    isDialogue = true;
                    try {
                        QuestManager.init.InsertQuest(production.scriptNum, this);
                    } catch(KeyNotFoundException e) {
                        Debug.Log(e.Message + "해당 퀘스트의 script number 불일치");
                    }
                    while (isDialogue) {
                        yield return null;
                    }
                    break;

                case ProductionKey.moveSpeed:
                    moveSpeed = production.moveSpeed;
                    break;

                case ProductionKey.delayTime:
                    yield return new WaitForSeconds(production.delayTime);
                    break;

                case ProductionKey.anim:
                    break;
            }
        }

        if (this.GetComponent<FollowCam>() != null) this.GetComponent<FollowCam>().enabled = true;
        if (this.GetComponent<PlayerControl>() != null) this.GetComponent<PlayerControl>().enabled = true;

        isProductionStop = true;
        moveSpeed = initMoveSpeed;
    }
}