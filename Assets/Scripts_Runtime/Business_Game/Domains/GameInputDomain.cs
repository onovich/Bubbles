namespace Bubbles {

    public static class GameInputDomain {

        public static void Player_BakeInput(GameBusinessContext ctx, float dt) {
            InputEntity inputEntity = ctx.inputEntity;
            inputEntity.ProcessInput(ctx.mainCamera, dt);
        }

        public static void Player_ResetInput(GameBusinessContext ctx) {
            InputEntity inputEntity = ctx.inputEntity;
            inputEntity.Reset();
        }

    }

}