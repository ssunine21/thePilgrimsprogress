using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

//[CustomPropertyDrawer(typeof(Objects))]
public class ProductionDrawer : PropertyDrawer {
    class Data {
        public GameObject game = null;
        public ProductionArray[] productionArrays = null;
    }

    ReorderableList productionList;

    Dictionary<string, Data> propertyData = new Dictionary<string, Data>();

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {

        //Data productionData;

        //if (!propertyData.TryGetValue(property.propertyPath, out productionData)) {
        //    productionData = new Data();
        //    propertyData[property.propertyPath] = productionData;
        //}

        var gameObject = property.FindPropertyRelative("gameObject");
        var productionArray = property.FindPropertyRelative("productionArray");

        EditorGUI.PropertyField(position, gameObject);

        if (productionList == null) {
            productionList = new ReorderableList(productionArray.serializedObject, productionArray, true, true, true, true);
        }

        productionList.DoLayoutList();
    }
}
