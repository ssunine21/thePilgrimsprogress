using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectControl : Character {

    private bool isProductionStop = false;
    private bool isMove = false;

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
    }

    public void ProductionStart() {
        if (this.GetComponent<FollowCam>() != null)
            this.GetComponent<FollowCam>().enabled = false;
        else if (this.GetComponent<PlayerControl>() != null)
                this.GetComponent<PlayerControl>().enabled = false;

        StopAllCoroutines();
        StartCoroutine("ProductionControl");
    }

    private void FixedUpdate() {
        Move();
    }

    IEnumerator ProductionControl() {

        while(preObject != null){
            if (preObject.isProductionStop)
                preObject = null;

            yield return null;
        }

        foreach(var production in productionTypeList) {
            switch (production.productionKey) {
                case ProductionKey.gameObject:
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
                    QuestManager.init.InsertQuest(production.scriptNum, this);
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
    }
}