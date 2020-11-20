using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(ProductionManager))]
public class ProductionInspector : Editor {

    private ReorderableList productionType;
    private ReorderableList npcList;
    private SerializedProperty prop;
    private SerializedProperty npcProp;
    private ProductionManager productionManager;

    private float keyWidth;
    private float valueWidth;
    private float x, y;

    private void OnEnable() {
        npcProp = serializedObject.FindProperty("npcList");
        prop = serializedObject.FindProperty("productionTypeList");

        productionType = new ReorderableList(serializedObject, prop, true, true, true, true);
        npcList = new ReorderableList(serializedObject, npcProp, true, true, true, true);

        productionManager = (ProductionManager)target;

        npcList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
            rect.height = EditorGUIUtility.singleLineHeight;

            productionManager.npcList[index] = (GameObject)EditorGUI.ObjectField(rect, GUIContent.none, productionManager.npcList[index], typeof(GameObject), true);
        };
        npcList.drawHeaderCallback = (rect) => {
            EditorGUI.LabelField(rect, npcProp.displayName);
        };

        productionType.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
            var element = prop.GetArrayElementAtIndex(index);
            rect.y += 2;

            x = rect.x + 10f;
            y = rect.y;
            keyWidth = rect.width * 0.4f;
            valueWidth = rect.width * 0.55f;

            productionManager.productionTypeList[index].productionKey =
            (ProductionKey)EditorGUI.EnumPopup(new Rect(rect.x, rect.y, keyWidth, EditorGUIUtility.singleLineHeight), GUIContent.none, productionManager.productionTypeList[index].productionKey);

            switch (productionManager.productionTypeList[index].productionKey) {
                case ProductionKey.gameObject:
                    EditorGUI.PropertyField(new Rect(x + keyWidth, y, valueWidth, EditorGUIUtility.singleLineHeight),
                        element.FindPropertyRelative("gameObject"), GUIContent.none);
                    break;

                case ProductionKey.position:
                    EditorGUI.PropertyField(new Rect(x + keyWidth, y, valueWidth, EditorGUIUtility.singleLineHeight),
                        element.FindPropertyRelative("pos"), GUIContent.none);
                    break;

                case ProductionKey.zoom:
                    EditorGUI.PropertyField(new Rect(x + keyWidth, y, valueWidth, EditorGUIUtility.singleLineHeight),
                        element.FindPropertyRelative("zoom"), GUIContent.none);
                    break;

                case ProductionKey.scriptNum:
                    EditorGUI.PropertyField(new Rect(x + keyWidth, y, valueWidth, EditorGUIUtility.singleLineHeight),
                        element.FindPropertyRelative("scriptNum"), GUIContent.none);
                    break;

                case ProductionKey.moveSpeed:
                    EditorGUI.PropertyField(new Rect(x + keyWidth, y, valueWidth, EditorGUIUtility.singleLineHeight),
                        element.FindPropertyRelative("moveSpeed"), GUIContent.none);
                    break;

                case ProductionKey.delayTime:
                    EditorGUI.PropertyField(new Rect(x + keyWidth, y, valueWidth, EditorGUIUtility.singleLineHeight),
                        element.FindPropertyRelative("delayTime"), GUIContent.none);
                    break;

                case ProductionKey.nextQuest:
                    EditorGUI.PropertyField(new Rect(x + keyWidth, y, valueWidth, EditorGUIUtility.singleLineHeight),
                        element.FindPropertyRelative("nextQuestNumber"), GUIContent.none);
                    break;
            }
        };

        productionType.drawHeaderCallback = (rect) => {
            EditorGUI.LabelField(rect, prop.displayName);
        };

    }



    // 유니티가 인스펙터를 GUI로 그려주는함수
    public override void OnInspectorGUI() {
        serializedObject.Update();


        productionManager.questNumber = (string)EditorGUILayout.TextField("Quest Number", productionManager.questNumber);

        EditorGUILayout.Space();
        npcList.DoLayoutList();
        EditorGUILayout.Space();
        productionType.DoLayoutList();
        //productionManager.isCheck = EditorGUILayout.Toggle(new GUIContent("isCheck", "start"), productionManager.isCheck);
        productionManager.checkerImg = (GameObject)EditorGUILayout.ObjectField("CheckerImg", productionManager.checkerImg, typeof(GameObject), true);
        serializedObject.ApplyModifiedProperties();
    }

    private void OnSceneGUI() {
      
        var vertexes = productionManager.productionTypeList;
        //List<Vector3> line = new List<Vector3>;
        foreach(var vertex in vertexes) {
            if (vertex.productionKey.Equals(ProductionKey.position)) {
                vertex.pos = Handles.PositionHandle(vertex.pos, Quaternion.identity);
            }
            else if (vertex.productionKey.Equals(ProductionKey.gameObject)) {
                try {
                    vertex.gameObject.transform.position = Handles.PositionHandle(vertex.gameObject.transform.position, Quaternion.identity);
                }catch(System.NullReferenceException e) {
                    e.GetHashCode();
                }
            }
        }
    }

    Vector3 PositionHandle(Transform transform) {
        var position = transform.position;

        Handles.color = Handles.xAxisColor;
        position = Handles.Slider(position, transform.right); //X축

        Handles.color = Handles.yAxisColor;
        position = Handles.Slider(position, transform.up); //Y축

        Handles.color = Handles.zAxisColor;
        position = Handles.Slider(position, transform.forward); //Z축

        return position;
    }
}