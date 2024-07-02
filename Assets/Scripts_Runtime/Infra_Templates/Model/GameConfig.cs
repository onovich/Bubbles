using System;
using MortiseFrame.Swing;
using UnityEngine;

namespace Bubbles {

    [CreateAssetMenu(fileName = "TM_GameConfig", menuName = "Bubbles/GameConfig")]
    public class GameConfig : ScriptableObject {

        // Game
        [Header("Game Config")]
        public float gameResetEnterTime;

        // Bubble
        [Header("Bubble Config")]
        public int ownerBubbleTypeID;

        // Map
        [Header("Map Config")]
        public int originalMapTypeID;
        public Material gridMat;
        public Color gridColor;
        public float gridThickness;

        [Header("Obstacle Config")]
        public Material obstacleMat;
        public Color obstacleColor;

        [Header("Path Config")]
        public Material pathMat;
        public Color pathColor;
        public float pathThickness;

        // Camera
        [Header("DeadZone Config")]
        public Vector2 cameraDeadZoneNormalizedSize;

        [Header("Shake Config")]
        public float cameraShakeFrequency_bubbleDamage;
        public float cameraShakeAmplitude_bubbleDamage;
        public float cameraShakeDuration_bubbleDamage;
        public EasingType cameraShakeEasingType_bubbleDamage;
        public EasingMode cameraShakeEasingMode_bubbleDamage;

    }

}