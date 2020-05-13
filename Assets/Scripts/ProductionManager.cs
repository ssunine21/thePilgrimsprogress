using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public struct CharacterProductionList {
    public Vector2 currPos;
    public float moveSpeed;
    public Vector2 prevPos;
    public Animation animation;
    public float delayTime;
}

public struct CameraProductionList {

}

public enum ProductionList {
    CharacterProductionList,
    CameraProductionList
}

[CustomEditor(typeof(ProductionManager))]
public class ProductionManager : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        if (GUILayout.Box()) {

        }
    }

}