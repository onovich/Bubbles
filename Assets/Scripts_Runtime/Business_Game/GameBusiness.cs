using UnityEngine;

namespace Bubbles {

    public static class GameBusiness {

        public static void Init(GameBusinessContext ctx) {

        }

        public static void StartGame(GameBusinessContext ctx) {
            GameGameDomain.NewGame(ctx);
        }

        public static void ExitGame(GameBusinessContext ctx) {
            GameGameDomain.ExitGame(ctx);
        }

        public static void Tick(GameBusinessContext ctx, float dt) {

            InputEntity inputEntity = ctx.inputEntity;

            ProcessInput(ctx, dt);
            PreTick(ctx, dt);

            const float intervalTime = 0.01f;
            ref float restSec = ref ctx.fixedRestSec;
            restSec += dt;
            if (restSec < intervalTime) {
                FixedTick(ctx, restSec);
                restSec = 0;
            } else {
                while (restSec >= intervalTime) {
                    restSec -= intervalTime;
                    FixedTick(ctx, intervalTime);
                }
            }
            LateTick(ctx, dt);

        }

        public static void ProcessInput(GameBusinessContext ctx, float dt) {
            GameInputDomain.Player_BakeInput(ctx, dt);
        }

        public static void ResetInput(GameBusinessContext ctx, float dt) {
            GameInputDomain.Player_ResetInput(ctx);
        }

        static void PreTick(GameBusinessContext ctx, float dt) {
            var game = ctx.gameEntity;
            var status = game.fsmComponent.status;
            var map = ctx.currentMapEntity;
            if (status == GameStatus.Gaming) {

                // Game
                GameGameDomain.ApplyGameTime(ctx, dt);

            }
        }

        static void FixedTick(GameBusinessContext ctx, float dt) {
            var game = ctx.gameEntity;
            var status = game.fsmComponent.status;
            var map = ctx.currentMapEntity;
            if (status == GameStatus.Gaming) {

                // Bubbles
                var bubbleLen = ctx.bubbleRepo.TakeAll(out var bubbleArr);
                for (int i = 0; i < bubbleLen; i++) {
                    var bubble = bubbleArr[i];
                    GameBubbleFSMController.FixedTickFSM(ctx, bubble, dt);
                }

                Physics2D.Simulate(dt);
            }
            // VFX
            VFXFrameApp.LateTick(ctx.vfxFrameContext, dt);

            // VFX
            VFXParticelApp.LateTick(ctx.vfxParticelContext, dt);

            // Reset Input
            ResetInput(ctx, dt);
        }

        static void LateTick(GameBusinessContext ctx, float dt) {
            var game = ctx.gameEntity;
            var status = game.fsmComponent.status;
            if (status == GameStatus.Gaming) {
                // Camera   
                var cameraPos = CameraApp.LateTick(ctx.cameraContext, dt);
                ctx.mainCamera.transform.position = cameraPos;

                // UI
                UIApp.GameInfo_RefreshTime(ctx.uiContext, game.fsmComponent.gaming_timer);

                // Result
                GameGameDomain.ApplyGameResult(ctx);

                // Bubbles
                var bubbleLen = ctx.bubbleRepo.TakeAll(out var bubbleArr);
                for (int i = 0; i < bubbleLen; i++) {
                    var bubble = bubbleArr[i];
                    GameBubbleDomain.CheckAndUnSpawn(ctx, bubble);
                }

                // VFX
                VFXParticelApp.LateTick(ctx.vfxParticelContext, dt);
            }

            // Game Over
            if (status == GameStatus.GameOver) {
                GameGameDomain.ApplyGameOver(ctx, dt);
            }
        }

        public static void TearDown(GameBusinessContext ctx) {
            var game = ctx.gameEntity;
            var status = game.fsmComponent.status;
            if (status == GameStatus.Gaming) {
                ExitGame(ctx);
            }
        }

        public static void OnRenderObject(GameBusinessContext ctx) {
            var game = ctx.gameEntity;
            var status = game.fsmComponent.status;
            if (status == GameStatus.Gaming) {
                GameMapDomain.DrawMap(ctx);
                GLApp.Tick(ctx.glContext);
            }
        }

        public static void OnDrawGizmos(GameBusinessContext ctx, bool drawCameraGizmos) {
            if (ctx == null) {
                return;
            }
            var game = ctx.gameEntity;
            var status = game.fsmComponent.status;
            if (status == GameStatus.Gaming) {
                if (drawCameraGizmos) {
                    CameraApp.OnDrawGizmos(ctx.cameraContext);
                }
            }
        }

        // UI
        public static void UIGameOver_OnRestartGame(GameBusinessContext ctx) {
            UIApp.GameOver_Close(ctx.uiContext);
            GameGameDomain.RestartGame(ctx);
        }

        public static void UIGameOver_OnExitGameClick(GameBusinessContext ctx) {
            ExitGame(ctx);
            Application.Quit();
            GLog.Log("Application.Quit");
        }

    }

}