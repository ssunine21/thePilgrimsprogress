using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(ProductionManager))]
public class ProductionInspector : Editor {

    private ReorderableList productionType;
    private SerializedProperty prop;
    private ProductionManager productionManager;

    private float keyWidth;
    private float valueWidth;
    private float x, y;

    private void OnEnable() {
        prop = serializedObject.FindProperty("productionType");
        productionType = new ReorderableList(serializedObject, prop, true, true, true, true);

        productionManager = (ProductionManager)target;

        productionType.drawElementCallback =
            (Rect rect, int index, bool isActive, bool isFocused) => {
                var element = prop.GetArrayElementAtIndex(index);
                rect.y += 2;

                x = rect.x + 10f;
                y = rect.y;
                keyWidth = rect.width * 0.4f;
                valueWidth = rect.width * 0.55f;

                productionManager.productionType[index].productionKey = (ProductionKey)EditorGUI.EnumPopup(new Rect(rect.x, rect.y, keyWidth, EditorGUIUtility.singleLineHeight), GUIContent.none, productionManager.productionType[index].productionKey);


                switch (productionManager.productionType[index].productionKey) {
                    case ProductionKey.gameObject:
                        EditorGUI.PropertyField(new Rect(x + keyWidth, y, valueWidth, EditorGUIUtility.singleLineHeight),
                            element.FindPropertyRelative("gameObject"), GUIContent.none);
                        break;

                    case ProductionKey.position:
                        EditorGUI.PropertyField(new Rect(x + keyWidth, y, valueWidth, EditorGUIUtility.singleLineHeight),
                            element.FindPropertyRelative("pos"), GUIContent.none);
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
                }
            };

        productionType.drawHeaderCallback = (rect) => {
            EditorGUI.LabelField(rect, prop.displayName);
        };

    }



    // 유니티가 인스펙터를 GUI로 그려주는함수
    public override void OnInspectorGUI() {
        serializedObject.Update();
        productionType.DoLayoutList();
        productionManager.isStart = EditorGUILayout.Toggle(new GUIContent("StartButton", "start"), productionManager.isStart);
        serializedObject.ApplyModifiedProperties();
    }

    private void OnSceneGUI() {
        Tools.current = Tool.None;
        var vertexes = productionManager.productionType;
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