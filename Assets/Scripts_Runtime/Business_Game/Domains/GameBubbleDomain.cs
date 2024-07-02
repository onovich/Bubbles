using System;
using MortiseFrame.Compass;
using MortiseFrame.Compass.Extension;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Bubbles {

    public static class GameBubbleDomain {

        public static BubbleEntity Spawn(GameBusinessContext ctx, int typeID, Vector2 pos) {
            var bubble = GameFactory.Bubble_Spawn(ctx.templateInfraContext,
                                              ctx.assetsInfraContext,
                                              ctx.idRecordService,
                                              typeID,
                                              pos);

            ctx.bubbleRepo.Add(bubble);
            return bubble;
        }

        public static void CheckAndUnSpawn(GameBusinessContext ctx, BubbleEntity bubble) {
            if (bubble.needTearDown) {
                UnSpawn(ctx, bubble);
            }
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