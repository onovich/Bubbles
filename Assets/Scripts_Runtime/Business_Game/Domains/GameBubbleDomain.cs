using System;
using MortiseFrame.Compass;
using MortiseFrame.Compass.Extension;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Bubbles {

    public static class GameBubbleDomain {

        public static BubbleEntity Spawn(GameBusinessContext ctx, int typeID, Vector2 pos, Vector2 size) {
            var bubble = GameFactory.Bubble_Spawn(ctx.templateInfraContext,
                                              ctx.assetsInfraContext,
                                              ctx.idRecordService,
                                              typeID,
                                              pos,
                                              size);

            ctx.bubbleRepo.Add(bubble);
            return bubble;
        }

        public static void CheckAndUnSpawn(GameBusinessContext ctx, BubbleEntity bubble) {
            if (bubble.needTearDown) {
                UnSpawn(ctx, bubble);
            }
        }

        public static void MouseOnBubble(GameBusinessContext ctx, BubbleEntity bubble) {
            var mousePos = ctx.inputEntity.mouseWorldPos;
            if (!ctx.inputEntity.isLeftMouseClick) {
                return;
            }

            var config = ctx.templateInfraContext.Config_Get();

            // 点到圆心的距离的平方，小于半径的平方，则点在圆内
            var pos = bubble.Pos;
            var size = bubble.currentSize;
            var radius = size.x / 2;
            var dis = (pos - mousePos).sqrMagnitude;
            if (dis < radius * radius) {
                SplitBubble(ctx, bubble, mousePos);
            }
        }

        public static void SplitBubble(GameBusinessContext ctx, BubbleEntity bubble, Vector2 point) {
            // 1. 生成小泡泡: 小泡泡直径 = 旧泡泡直径 / 2-点到旧圆心的距离; 坐标 = 旧圆心到点的方向归一化 * 小泡泡直径 / 2 + 点坐标;
            var dir = (bubble.Pos - point).normalized;
            var dis = (bubble.Pos - point).magnitude;
            var size = bubble.currentSize / 2 - new Vector2(dis, dis);
            var pos = -dir * size / 2 + point;
            var smallBubble = Spawn(ctx, bubble.typeID, pos, size);
            // 2. 生成大泡泡: 大泡泡直径 = 旧泡泡直径 / 2+点到旧圆心的距离; 坐标 = 点到旧圆心的方向归一化 * 大泡泡直径 / 2 + 点坐标;
            dir = (point - bubble.Pos).normalized;
            size = bubble.currentSize / 2 + new Vector2(dis, dis);
            pos = -dir * size / 2 + point;
            var bigBubble = Spawn(ctx, bubble.typeID, pos, size);
            // 3. 销毁旧泡泡
            UnSpawn(ctx, bubble);
        }

        public static void ApplyDamage(GameBusinessContext ctx, BubbleEntity bubble) {
        }

        public static void UnSpawn(GameBusinessContext ctx, BubbleEntity bubble) {
            ctx.bubbleRepo.Remove(bubble);
            bubble.TearDown();
        }

        public static void ApplyConstraint(GameBusinessContext ctx, BubbleEntity bubble) {
            var map = ctx.currentMapEntity;
            var pos = bubble.Pos;
            var halfSize = map.mapSize / 2;
            var gridUnit = map.gridUnit;
            var min = -halfSize;
            var max = halfSize - new Vector2(gridUnit, gridUnit);
            var x = Mathf.Clamp(pos.x, min.x, max.x);
            var y = Mathf.Clamp(pos.y, min.y, max.y);
            bubble.Pos_SetPos(new Vector2(x, y));
        }

    }

}