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

        list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
            var element = list.serializedProperty.GetArrayElementAtIndex(index);
            rect.y += 2;
            EditorGUI.PropertyField(
                new Rect(rect.x, rect.y, 60, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("pos"), GUIContent.none);
            EditorGUI.PropertyField(
                new Rect(rect.x + 60, rect.y, rect.width - 60 - 30, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("moveSpeed"), GUIContent.none);
            EditorGUI.PropertyField(
                new Rect(rect.x + rect.width - 30, rect.y, 30, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("delayTime"), GUIContent.none);
            EditorGUI.PropertyField(
                new Rect(rect.x + rect.width - 30, rect.y, 30, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("anim"), GUIContent.none);
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