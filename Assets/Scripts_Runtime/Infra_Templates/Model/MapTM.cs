using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Bubbles {

    [CreateAssetMenu(fileName = "TM_Map", menuName = "Bubbles/MapTM")]
    public class MapTM : ScriptableObject {

        public int typeID;
        public Vector2Int mapSize;
        public float gridUnit;
        public bool[] obstacleData;
        public int obstacleDataWidth;

        // Bubbles
        public Vector2[] bubblePosArray;
        public BubbleTM[] bubbleArray;

        // Camera
        public Vector2 cameraConfinerWorldMax;
        public Vector2 cameraConfinerWorldMin;

    }

}