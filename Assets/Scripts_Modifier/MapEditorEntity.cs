#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using MortiseFrame.Compass.Extension;
using TriInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Bubbles.Modifier {

    public class MapEditorEntity : MonoBehaviour {

        [SerializeField] int typeID;
        [SerializeField] GameObject mapSize;
        [SerializeField] MapTM mapTM;
        [SerializeField] Transform bubbleGroup;
        [SerializeField] float gridUnit = 1;

        [Button("Bake")]
        void Bake() {
            BakeMapInfo();
            BakeBubbles();
            EditorUtility.SetDirty(mapTM);
            AssetDatabase.SaveAssets();
            Debug.Log("Bake Sucess");
        }

        void BakeBubbles() {
            List<Vector2> bubblePosList = new List<Vector2>();
            List<BubbleTM> bubbleList = new List<BubbleTM>();
            foreach (Transform bubble in bubbleGroup) {
                bubblePosList.Add(bubble.position);
                bubbleList.Add(bubble.GetComponent<BubbleEditorEntity>().GetBubbleTM());
            }
            mapTM.bubblePosArray = bubblePosList.ToArray();
            mapTM.bubbleArray = bubbleList.ToArray();
        }

        void BakeMapInfo() {
            mapTM.typeID = typeID;
            mapTM.mapSize = mapSize.transform.localScale.RoundToVector2Int();
            mapTM.gridUnit = gridUnit;
        }

        void OnDrawGrid() {
            PathFindingGizmosHelper.OnDrawGrid(gridUnit, -mapTM.mapSize / 2, mapTM.mapSize / 2);
        }

        void OnDrawObstacle() {
            PathFindingGizmosHelper.OnDrawObstacle(mapTM.obstacleData, mapTM.obstacleDataWidth, -mapTM.mapSize / 2, gridUnit);
        }

        void OnDrawGizmos() {
            OnDrawGrid();
            OnDrawObstacle();
        }

    }

}
#endif