using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(ProductionManager))]
public class ProductionInspector : Editor {

    public ProductionManager productionObject;

    private void OnEnable() {
        // target은 Editor에 있는 변수로 선택한 오브젝트를 받아옴.
        if (AssetDatabase.Contains(target)) {
            productionObject = null;
        } else {
            // target은 Object형이므로 Enemy로 형변환
            productionObject = (ProductionManager)target;
        }
    }

    // 유니티가 인스펙터를 GUI로 그려주는함수
    public override void OnInspectorGUI() {
        if (productionObject == null)
            return;

        EditorGUILayout.Space();

        switch (productionObject.production) {
            case Production.Character:

                break;
            case Production.Camera:

                break;
            case Production.Object:

                break;
            default:
                break;
        }

/*        GUI.color = tempColor;
        selected.monsterType = (MonsterType)EditorGUILayout.EnumPopup("몬스터 종류", selected.monsterType);

        GUI.color = Color.white;
        selected.hp = EditorGUILayout.IntField("몬스터 체력", selected.hp);
        if (selected.hp < 0)
            selected.hp = 0;

        selected.damage = EditorGUILayout.FloatField("몬스터 공격력", selected.damage);
        selected.tag = EditorGUILayout.TextField("설명", selected.tag);

        // Release 세팅하고 버튼누르면 모든변수가 다바뀌게. Test 세팅하면 그렇게 바뀌게 그런식으로 사용할 수 있음.
        if (GUILayout.Button("Resize")) {
            selected.transform.localScale = Vector3.one * Random.Range(0.5f, 1f);
        }*/
    }
}