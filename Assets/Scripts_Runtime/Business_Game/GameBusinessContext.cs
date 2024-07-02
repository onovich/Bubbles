using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bubbles {

    public class GameBusinessContext {

        // Entity
        public GameEntity gameEntity;
        public PlayerEntity playerEntity;
        public InputEntity inputEntity; // External
        public MapEntity currentMapEntity;

        public BubbleRepository bubbleRepo;
        public BlockRepository blockRepo;
        public SpikeRepository spikeRepo;

        // App
        public UIAppContext uiContext;
        public VFXParticelAppContext vfxParticelContext;
        public VFXFrameAppContext vfxFrameContext;
        public CameraAppContext cameraContext;
        public SoundAppContext soundContext;
        public GLAppContext glContext;

        // Camera
        public Camera mainCamera;

        // Service
        public IDRecordService idRecordService;
        public PathFindingService pathFindingService;

        // Infra
        public TemplateInfraContext templateInfraContext;
        public AssetsInfraContext assetsInfraContext;

        // Timer
        public float fixedRestSec;

        // SpawnPoint
        public Vector2 ownerSpawnPoint;

        // TEMP
        public RaycastHit2D[] hitResults;

        public GameBusinessContext() {
            gameEntity = new GameEntity();
            playerEntity = new PlayerEntity();
            idRecordService = new IDRecordService();
            pathFindingService = new PathFindingService();
            bubbleRepo = new BubbleRepository();
            blockRepo = new BlockRepository();
            spikeRepo = new SpikeRepository();
            hitResults = new RaycastHit2D[100];
        }

        public void Reset() {
            idRecordService.Reset();
            bubbleRepo.Clear();
            blockRepo.Clear();
            spikeRepo.Clear();
        }

        // Bubble
        public void Bubble_ForEach(Action<BubbleEntity> onAction) {
            bubbleRepo.ForEach(onAction);
        }

        // Block
        public void Block_ForEach(Action<BlockEntity> onAction) {
            blockRepo.ForEach(onAction);
        }

    }

}