using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectControl : Character
{
    [HideInInspector]
    public List<ProductionType> productionTypeList;
    [HideInInspector]
    public ObjectControl preObject;

    private bool isMove = false;
    public bool isDialogue = false;
    public bool isProductionStop = false;

    private void Start() {
        tr = this.transform;
    }

    public void ProductionStart() {

        if (this.GetComponent<PlayerControl>() != null) this.GetComponent<PlayerControl>().enabled = false;
        else if (this.GetComponent<FollowCam>() != null) this.GetComponent<FollowCam>().enabled = false;

        StopAllCoroutines();
        StartCoroutine("ProductionControl");
    }

    public void setDebug() {
        Debug.Log(productionTypeList);
    }

    private void FixedUpdate() {

        if (isMove) {
            Move();
        }
    }

    IEnumerator ProductionControl() {

        while(preObject != null){
            if (preObject.isProductionStop) preObject = null;

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
                        if (Vector2.Distance(tr.position, production.pos) >= 0.05f) isMove = true;
                        else isMove = false;

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

        if (this.GetComponent<PlayerControl>() != null) this.GetComponent<PlayerControl>().enabled = true;
        else if (this.GetComponent<FollowCam>() != null) this.GetComponent<FollowCam>().enabled = true;

        isProductionStop = true;
    }
}
