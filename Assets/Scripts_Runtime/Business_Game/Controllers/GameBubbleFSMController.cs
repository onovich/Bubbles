using MortiseFrame.Swing;
using UnityEngine;

namespace Bubbles {

    public static class GameBubbleFSMController {

        public static void FixedTickFSM(GameBusinessContext ctx, BubbleEntity bubble, float dt) {

            FixedTickFSM_Any(ctx, bubble, dt);

            BubbleFSMStatus status = bubble.FSM_GetStatus();
            if (status == BubbleFSMStatus.Idle) {
                FixedTickFSM_Idle(ctx, bubble, dt);
            } else if (status == BubbleFSMStatus.Dead) {
                FixedTickFSM_Dead(ctx, bubble, dt);
            } else {
                GLog.LogError($"GameBubbleFSMController.FixedTickFSM: unknown status: {status}");
            }

        }

        static void FixedTickFSM_Any(GameBusinessContext ctx, BubbleEntity bubble, float dt) {

        }

        static void FixedTickFSM_Idle(GameBusinessContext ctx, BubbleEntity bubble, float dt) {
            BubbleFSMComponent fsm = bubble.FSM_GetComponent();
            if (fsm.idle_isEntering) {
                fsm.idle_isEntering = false;
            }

            // Move
            GameBubbleDomain.ApplyDamage(ctx, bubble);
            GameBubbleDomain.ApplyConstraint(ctx, bubble);
            GameBubbleDomain.MouseOnBubble(ctx, bubble);
        }

        static void FixedTickFSM_Dead(GameBusinessContext ctx, BubbleEntity bubble, float dt) {
            BubbleFSMComponent fsm = bubble.FSM_GetComponent();
            if (fsm.dead_isEntering) {
                fsm.dead_isEntering = false;
            }

            // VFX
            VFXParticelApp.AddVFXToWorld(ctx.vfxParticelContext, bubble.deadVFXName, bubble.deadVFXDuration, bubble.Pos);

            // Camera
            GameCameraDomain.ShakeOnce(ctx);
            bubble.needTearDown = true;
        }

    }

}