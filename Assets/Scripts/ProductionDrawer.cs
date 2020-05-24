using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

//[CustomPropertyDrawer(typeof(ProductionType))]
public class ProductionDrawer : PropertyDrawer {


    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        //base.OnGUI(position, property, label);

        position.x += 10;
        if (property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, GUIContent.none)) {

            var halfWidth = position.width * 0.5f;

            var keyRect = new Rect(position) {
                y = EditorGUIUtility.singleLineHeight + 6
            };
            var valueRect = new Rect(keyRect) {
                y = keyRect.y + EditorGUIUtility.singleLineHeight + 6
            };

            var key = property.FindPropertyRelative("gameObject");
            var value = property.FindPropertyRelative("pos");

            //EditorGUILayout.ObjectField(gameObject);
            EditorGUILayout.PropertyField(key);
            EditorGUILayout.PropertyField(value);
        }

    }
}

//[CustomPropertyDrawer(typeof(Objects))]
//public class ProductionDrawer : PropertyDrawer {

//    private Objects objects;
//    private ReorderableList production;

//    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
//        //base.OnGUI(position, property, label);

//        using (new EditorGUI.PropertyScope(position, label, property)) {
//            position.x += 10;
//            if (property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, new GUIContent("Unit"))) {

//                var halfWidth = position.width * 0.5f;

//                var gameObjectRect = new Rect(position) {
//                    y = EditorGUIUtility.singleLineHeight + 6
//                };
//                var productionArrayRect = new Rect(gameObjectRect) {
//                    y = gameObjectRect.y + EditorGUIUtility.singleLineHeight + 6
//                };

//                var gameObject = property.FindPropertyRelative("gameObject");
//                var productionArray = property.FindPropertyRelative("productionArray");

//                gameObject.objectReferenceValue = EditorGUI.ObjectField(
//                    gameObjectRect, gameObject.objectReferenceValue, typeof(GameObject), false);
//                //EditorGUILayout.ObjectField(gameObject);
//                EditorGUILayout.PropertyField(productionArray);
//            }
//        }
//    }
//}
