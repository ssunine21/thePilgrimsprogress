using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(Objects))]
public class ProductionInspector : Editor {

    private ReorderableList production;

    private void OnEnable() {
        var prop = serializedObject.FindProperty("productionArray");
        production = new ReorderableList(serializedObject, prop, true, true, true, true);
        production.elementHeight = EditorGUIUtility.singleLineHeight;

        production.drawElementCallback =
            (Rect rect, int index, bool isActive, bool isFocused) => {
                var element = prop.GetArrayElementAtIndex(index);
                rect.y += 2;
                EditorGUI.PropertyField(rect, element);
            };

        production.drawHeaderCallback = (rect) => {
            EditorGUI.LabelField(rect, prop.displayName);
        };

    }



    // 유니티가 인스펙터를 GUI로 그려주는함수
    public override void OnInspectorGUI() {
        serializedObject.Update();
        production.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }

}