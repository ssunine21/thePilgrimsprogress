using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

//[CanEditMultipleObjects]
[CustomEditor(typeof(ProductionManager))]
public class ProductionInspector : Editor {

    ProductionManager productionManager;

    private void OnEnable() {
        productionManager = target as ProductionManager;
    }

    // 유니티가 인스펙터를 GUI로 그려주는함수
    public override void OnInspectorGUI() {
        serializedObject.Update();

        productionManager.type = (Type)EditorGUILayout.EnumPopup("Type", productionManager.type);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("objects"), true);

        serializedObject.ApplyModifiedProperties();
    }

}