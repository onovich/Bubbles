using System.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace Bubbles {

    public static class TemplateInfra {

        public static async Task LoadAssets(TemplateInfraContext ctx) {

            {
                var handle = Addressables.LoadAssetAsync<GameConfig>("TM_Config");
                var cotmfig = await handle.Task;
                ctx.Config_Set(cotmfig);
                ctx.configHandle = handle;
            }

            {
                var handle = Addressables.LoadAssetsAsync<MapTM>("TM_Map", null);
                var mapList = await handle.Task;
                foreach (var tm in mapList) {
                    ctx.Map_Add(tm);
                }
                ctx.mapHandle = handle;
            }

            {
                var handle = Addressables.LoadAssetsAsync<BubbleTM>("TM_Bubble", null);
                var bubbleList = await handle.Task;
                foreach (var tm in bubbleList) {
                    ctx.Bubble_Add(tm);
                }
                ctx.bubbleHandle = handle;
            }

            {
                var handle = Addressables.LoadAssetAsync<SoundTable>("Table_Sound");
                var soundTable = await handle.Task;
                ctx.SoundTable_Set(soundTable);
                ctx.soundTableHandle = handle;
            }

        }

        public static void Release(TemplateInfraContext ctx) {
            if (ctx.configHandle.IsValid()) {
                Addressables.Release(ctx.configHandle);
            }
            if (ctx.mapHandle.IsValid()) {
                Addressables.Release(ctx.mapHandle);
            }
            if (ctx.bubbleHandle.IsValid()) {
                Addressables.Release(ctx.bubbleHandle);
            }
            if (ctx.soundTableHandle.IsValid()) {
                Addressables.Release(ctx.soundTableHandle);
            }
        }

    }

}