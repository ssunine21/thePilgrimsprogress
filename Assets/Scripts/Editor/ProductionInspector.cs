using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(ProductionManager))]
public class ProductionInspector : Editor {

    private ReorderableList productionType;
    private ProductionKey productionKey;
    private SerializedProperty prop;

    private void OnEnable() {
        prop = serializedObject.FindProperty("productionType");
        productionType = new ReorderableList(serializedObject, prop, true, true, true, true);
        //productionType.elementHeight = EditorGUIUtility.singleLineHeight;

        productionType.drawElementCallback =
            (Rect rect, int index, bool isActive, bool isFocused) => {
                var element = prop.GetArrayElementAtIndex(index);
                rect.y += 2;

                float width = rect.width * 0.5f;

                this.productionKey = EditorGUI.IntPopup(new Rect(rect.x, rect.y, width, EditorGUIUtility.singleLineHeight),
                    element.FindPropertyRelative("productionKey"), GUIContent.none);



                switch (this.productionKey) {
                    case ProductionKey.GameObject:
                        EditorGUI.PropertyField(new Rect(rect.x + width, rect.y, width, EditorGUIUtility.singleLineHeight),
                            element.FindPropertyRelative("gameObject"), GUIContent.none);
                        break;

                    case ProductionKey.pos:
                        EditorGUI.PropertyField(new Rect(rect.x + width, rect.y, width, EditorGUIUtility.singleLineHeight),
                            element.FindPropertyRelative("pos"), GUIContent.none);
                        break;
                }
            };



        //productionType.drawHeaderCallback = (rect) => {
        //    EditorGUI.LabelField(rect, prop.displayName);
        //};

    }



    // 유니티가 인스펙터를 GUI로 그려주는함수
    public override void OnInspectorGUI() {
        serializedObject.Update();
        productionType.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }

}