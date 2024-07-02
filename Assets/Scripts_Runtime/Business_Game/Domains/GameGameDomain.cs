using UnityEngine;

namespace Bubbles {

    public static class GameGameDomain {

        public static void NewGame(GameBusinessContext ctx) {

            var config = ctx.templateInfraContext.Config_Get();

            // Game
            var game = ctx.gameEntity;
            game.fsmComponent.Gaming_Enter();

            // Map
            var mapTypeID = config.originalMapTypeID;
            var map = GameMapDomain.Spawn(ctx, mapTypeID);
            var has = ctx.templateInfraContext.Map_TryGet(mapTypeID, out var mapTM);
            if (!has) {
                GLog.LogError($"MapTM Not Found {mapTypeID}");
            }

            // Bubble
            var bubblePosArr = mapTM.bubblePosArray;
            var bubbleArr = mapTM.bubbleArray;
            for (int i = 0; i < bubblePosArr.Length; i++) {
                var pos = bubblePosArr[i];
                var bubbleTM = bubbleArr[i];
                var bubble = GameBubbleDomain.Spawn(ctx, bubbleTM.typeID, pos);
            }

            // Camera
            var mainCamera = ctx.mainCamera;
            var cameraID = CameraApp.CreateMainCamera(ctx.cameraContext,
                                                          mainCamera.transform.rotation.eulerAngles.z,
                                                          mainCamera.orthographicSize,
                                                          mainCamera.aspect,
                                                          mainCamera.transform.position,
                                                          mapTM.cameraConfinerWorldMax, mapTM.cameraConfinerWorldMax,
                                                          new Vector2(0, 0));
            CameraApp.SetCurrentCamera(ctx.cameraContext, cameraID);

            // BGM
            var soundTable = ctx.templateInfraContext.SoundTable_Get();
            SoundApp.BGM_PlayLoop(ctx.soundContext, soundTable.bgmLoop, 1, soundTable.bgmVolume, false);

        }

        public static void ApplyGameTime(GameBusinessContext ctx, float dt) {
            var game = ctx.gameEntity;
            var fsm = game.fsmComponent;

            fsm.Gaming_IncTimer(dt);
        }

        public static void ApplyGameOver(GameBusinessContext ctx, float dt) {
            var game = ctx.gameEntity;
            var fsm = game.fsmComponent;

            fsm.GameOver_DecTimer(dt);

            var enterTime = fsm.gameOver_enterTime;
            if (enterTime <= 0) {
                UIApp.GameOver_Open(ctx.uiContext, fsm.gameOver_result);
            }
        }

        public static void RestartGame(GameBusinessContext ctx) {
            var game = ctx.gameEntity;
            var fsm = game.fsmComponent;
            ExitGame(ctx);
            NewGame(ctx);
        }

        public static void ApplyGameResult(GameBusinessContext ctx) {
        }

        public static void ExitGame(GameBusinessContext ctx) {
            // Game
            var game = ctx.gameEntity;
            game.fsmComponent.NotInGame_Enter();

            // Map
            GameMapDomain.UnSpawn(ctx);

            // Bubble
            int bubbleLen = ctx.bubbleRepo.TakeAll(out var bubbleArr);
            for (int i = 0; i < bubbleLen; i++) {
                var bubble = bubbleArr[i];
                GameBubbleDomain.UnSpawn(ctx, bubble);
            }

            // UI
            UIApp.GameOver_Close(ctx.uiContext);
            UIApp.GameInfo_Close(ctx.uiContext);

        }

    }
}