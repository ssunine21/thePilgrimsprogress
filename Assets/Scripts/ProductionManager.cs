using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public struct CharacterProductionList {
    public Vector2 currPos;
    public float moveSpeed;
    public Vector2 prevPos;
    public Animation animation;
    public float delayTime;
}

[System.Serializable]
public enum Production // your custom enumeration
{
    Character,
    Camera,
    Object
};

public class ProductionManager : MonoBehaviour
{
    public Production production;
    public ArrayList prodectionList;

}
