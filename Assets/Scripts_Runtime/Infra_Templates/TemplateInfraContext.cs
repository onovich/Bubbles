using System.Collections.Generic;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Bubbles {

    public class TemplateInfraContext {

        GameConfig config;
        public AsyncOperationHandle configHandle;

        Dictionary<int, MapTM> mapDict;
        public AsyncOperationHandle mapHandle;

        Dictionary<int, BubbleTM> bubbleDict;
        public AsyncOperationHandle bubbleHandle;

        SoundTable soundTable;
        public AsyncOperationHandle soundTableHandle;

        public TemplateInfraContext() {
            mapDict = new Dictionary<int, MapTM>();
            bubbleDict = new Dictionary<int, BubbleTM>();
        }

        // Game
        public void Config_Set(GameConfig config) {
            this.config = config;
        }

        public GameConfig Config_Get() {
            return config;
        }

        // Map
        public void Map_Add(MapTM map) {
            mapDict.Add(map.typeID, map);
        }

        public bool Map_TryGet(int typeID, out MapTM map) {
            var has = mapDict.TryGetValue(typeID, out map);
            if (!has) {
                GLog.LogError($"Map {typeID} not found");
            }
            return has;
        }

        // Bubble
        public void Bubble_Add(BubbleTM bubble) {
            bubbleDict.Add(bubble.typeID, bubble);
        }

        public bool Bubble_TryGet(int typeID, out BubbleTM bubble) {
            var has = bubbleDict.TryGetValue(typeID, out bubble);
            if (!has) {
                GLog.LogError($"Bubble {typeID} not found");
            }
            return has;
        }

        // Sound
        public void SoundTable_Set(SoundTable soundTable) {
            this.soundTable = soundTable;
        }

        public SoundTable SoundTable_Get() {
            return soundTable;
        }

        // Clear
        public void Clear() {
            mapDict.Clear();
            bubbleDict.Clear();
        }

    }

}