using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;


[CustomEditor(typeof(ProductionManager))]
public class ProductionInspector : Editor {
    private ReorderableList list;

    ProductionManager productionManager;

    private void OnEnable() {
        productionManager = target as ProductionManager;
        list = new ReorderableList(serializedObject, serializedObject.FindProperty("productionList"), true, true, true, true);
        list.elementHeight = 80;
        list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
            var element = list.serializedProperty.GetArrayElementAtIndex(index);

            float gap = 20f;
            float width = rect.width;
            rect.height = 16f;
            rect.width = width;

            EditorGUI.PropertyField(rect, element.FindPropertyRelative("pos"), GUIContent.none);
            rect.y += gap;

            EditorGUI.PropertyField(rect, element.FindPropertyRelative("moveSpeed"));
            rect.y += gap;

            EditorGUI.PropertyField(rect, element.FindPropertyRelative("delayTime"));

            rect.y += gap;
            EditorGUI.PropertyField(rect, element.FindPropertyRelative("anim"));

        };

    }

    // 유니티가 인스펙터를 GUI로 그려주는함수
    public override void OnInspectorGUI() {

        productionManager.production = (Production)EditorGUILayout.EnumPopup("Type", productionManager.production);

        serializedObject.Update();
        list.DoLayoutList();

        serializedObject.ApplyModifiedProperties();
    }
}