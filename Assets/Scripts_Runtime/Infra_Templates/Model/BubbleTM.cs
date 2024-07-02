using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Bubbles {

    [CreateAssetMenu(fileName = "TM_Bubble", menuName = "Bubbles/BubbleTM")]
    public class BubbleTM : ScriptableObject {

        public int typeID;
        public string typeName;

        public AllyStatus allyStatus;
        public int hpMax;

        public BubbleMod mod;
        public GameObject deadVFX;
        public float deadVFXDuration;

        public float size;
        public float sizeMax;

    }

}