namespace Bubbles {

    public static class GameCameraDomain {

        public static void ShakeOnce(GameBusinessContext ctx) {

            var config = ctx.templateInfraContext.Config_Get();

            var shakeFrequency = config.cameraShakeFrequency_bubbleDamage;
            var shakeAmplitude = config.cameraShakeAmplitude_bubbleDamage;
            var shakeDuration = config.cameraShakeDuration_bubbleDamage;
            var easingType = config.cameraShakeEasingType_bubbleDamage;
            var easingMode = config.cameraShakeEasingMode_bubbleDamage;

            var cameraID = ctx.cameraContext.mainCameraID;
            CameraApp.ShakeOnce(ctx.cameraContext,cameraID, shakeFrequency, shakeAmplitude, shakeDuration, easingType, easingMode);

        }

    }

}