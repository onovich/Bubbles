using System;
using UnityEngine;

namespace Bubbles {

    public static class GameFactory {

        public static MapEntity Map_Spawn(TemplateInfraContext templateInfraContext,
                                 AssetsInfraContext assetsInfraContext,
                                 int typeID) {

            var has = templateInfraContext.Map_TryGet(typeID, out var mapTM);
            if (!has) {
                GLog.LogError($"Map {typeID} not found");
            }

            var prefab = assetsInfraContext.Entity_GetMap();
            var map = GameObject.Instantiate(prefab).GetComponent<MapEntity>();
            map.Ctor();
            map.typeID = typeID;
            map.SetSize(mapTM.mapSize);
            map.gridUnit = mapTM.gridUnit;
            map.obstacleData = new bool[mapTM.obstacleData.Length];
            Array.Copy(mapTM.obstacleData, map.obstacleData, mapTM.obstacleData.Length);
            map.obstacleDataWidth = mapTM.obstacleDataWidth;
            return map;
        }

        public static BubbleEntity Bubble_Spawn(TemplateInfraContext templateInfraContext,
                                 AssetsInfraContext assetsInfraContext,
                                 IDRecordService idRecordService,
                                 int typeID,
                                 Vector2 pos,
                                 Vector2 size) {

            var has = templateInfraContext.Bubble_TryGet(typeID, out var bubbleTM);
            if (!has) {
                GLog.LogError($"Bubble {typeID} not found");
            }

            var prefab = assetsInfraContext.Entity_GetBubble();
            var bubble = GameObject.Instantiate(prefab).GetComponent<BubbleEntity>();
            bubble.Ctor();

            // Base Info
            bubble.entityID = idRecordService.PickBubbleEntityID();
            bubble.typeID = typeID;
            bubble.allyStatus = bubbleTM.allyStatus;

            // Set Attr
            bubble.hpMax = bubbleTM.hpMax;
            bubble.hp = bubble.hpMax;
            bubble.typeName = bubbleTM.typeName;

            // Rename
            bubble.gameObject.name = $"{bubble.typeName} - {bubble.entityID}";

            // Set Pos
            bubble.Pos_SetPos(pos);

            // Set Size
            bubble.Size_Set(size);

            // Set Mod
            var modPrefab = bubbleTM.mod;
            if (modPrefab != null) {
                var mod = GameObject.Instantiate(modPrefab, bubble.body).GetComponent<BubbleMod>();
                bubble.Mod_Set(mod);
            }

            // Set FSM
            bubble.FSM_EnterIdle();

            // Set VFX
            bubble.deadVFXName = bubbleTM.deadVFX.name;
            bubble.deadVFXDuration = bubbleTM.deadVFXDuration;

            return bubble;
        }

    }

}