using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bubbles {

    public class BubbleRepository {

        Dictionary<int, BubbleEntity> all;

        BubbleEntity[] temp;

        public BubbleRepository() {
            all = new Dictionary<int, BubbleEntity>();
            temp = new BubbleEntity[1000];
        }

        public void Add(BubbleEntity bubble) {
            all.Add(bubble.entityID, bubble);
        }

        public int TakeAll(out BubbleEntity[] bubbles) {
            int count = all.Count;
            if (count > temp.Length) {
                temp = new BubbleEntity[(int)(count * 1.5f)];
            }
            all.Values.CopyTo(temp, 0);
            bubbles = temp;
            return count;
        }

        public void Remove(BubbleEntity bubble) {
            all.Remove(bubble.entityID);
        }

        public bool TryGetBubble(int entityID, out BubbleEntity bubble) {
            return all.TryGetValue(entityID, out bubble);
        }

        public bool IsInRange(int entityID, in Vector2 pos, float range) {
            bool has = TryGetBubble(entityID, out var bubble);
            if (!has) {
                return false;
            }
            return Vector2.SqrMagnitude(bubble.Pos - pos) <= range * range;
        }

        public void ForEach(Action<BubbleEntity> action) {
            foreach (var bubble in all.Values) {
                action(bubble);
            }
        }

        public BubbleEntity GetNeareast(AllyStatus allyStatus, Vector2 pos, float radius) {
            BubbleEntity nearestBubble = null;
            float nearestDist = float.MaxValue;
            float radiusSqr = radius * radius;
            foreach (var bubble in all.Values) {
                if (bubble.allyStatus != allyStatus) {
                    continue;
                }
                float dist = Vector2.SqrMagnitude(bubble.Pos - pos);
                if (dist <= radiusSqr && dist < nearestDist) {
                    nearestDist = dist;
                    nearestBubble = bubble;
                }
            }
            return nearestBubble;
        }

        public void Clear() {
            all.Clear();
        }

    }

}