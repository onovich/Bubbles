using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Bubbles {

    public class SoundAppContext {

        public Transform root;
        public AudioSource audioSourcePrefab;

        public AudioSource bgmPlayer;

        public AudioSource[] bubbleWalkPlayer; // Tap 
        public AudioSource[] bubbleHurtPlayer; // Hurt / Dead
        public AudioSource[] bubbleAttackPlayer; // Swoosh / Slash / Clang
        public AudioSource[] bubbleHitPlayer; // SwooshBreak / ClangBradk

        public AsyncOperationHandle assetsHandle;

        public SoundAppContext(Transform soundRoot) {
            bubbleWalkPlayer = new AudioSource[4];
            bubbleHurtPlayer = new AudioSource[4];
            bubbleAttackPlayer = new AudioSource[4];
            bubbleHitPlayer = new AudioSource[4];
            this.root = soundRoot;
        }

    }

}