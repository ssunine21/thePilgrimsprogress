using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectControl : Character
{
    [HideInInspector]
    public List<ProductionType> productionTypeList;

    private bool isMove = false;
    public bool isDialogue = false;

    private void Start() {
        tr = this.transform;
    }

    public void ProductionStart() {
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
        foreach(var production in productionTypeList) {
            switch (production.productionKey) {
                case ProductionKey.gameObject:
                    break;

                case ProductionKey.position:
                    isMove = true;
                    Vector2 tempDir = (production.pos - (Vector2)tr.position);
                    moveDir = tempDir.normalized;

                    while (isMove) {
                        if (Vector3.Distance(tr.position, production.pos) >= 0.1f) isMove = true;
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
    }

}
