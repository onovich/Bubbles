using System;
using UnityEngine;

namespace Bubbles {

    public class BubbleEntity : MonoBehaviour {

        // Base Info
        public int entityID;
        public int typeID;
        public string typeName;
        public AllyStatus allyStatus;

        // Attr
        public Vector2 faceDir;
        public int hpMax;
        public int hp;

        // State
        public bool needTearDown;

        // FSM
        public BubbleFSMComponent fsmCom;

        // Render
        [SerializeField] public Transform body;
        BubbleMod bubbleMod;

        // VFX
        public string deadVFXName;
        public float deadVFXDuration;

        public BubbleVFXComponent vfxCom;

        // Pos
        public Vector2 Pos => Pos_GetPos();

        // SIze
        public Vector2 sizeOrigin;
        public Vector2 sizeMax;
        public Vector2 currentSize => body.localScale;

        // Path
        public Vector2[] path;
        public int pathLen;

        public void Ctor() {
            fsmCom = new BubbleFSMComponent();
            vfxCom = new BubbleVFXComponent();
            path = new Vector2[100];
        }

        // Pos
        public void Pos_SetPos(Vector2 pos) {
            transform.position = pos;
        }

        Vector2 Pos_GetPos() {
            return transform.position;
        }

        // Size
        public void Size_Set(Vector2 size) {
            body.localScale = size;
        }

        // Color
        public void Color_SetAlpha(float alpha) {
            bubbleMod?.SetColorAlpha(alpha);
        }

        // FSM
        public BubbleFSMStatus FSM_GetStatus() {
            return fsmCom.status;
        }

        public BubbleFSMComponent FSM_GetComponent() {
            return fsmCom;
        }

        public void FSM_EnterIdle() {
            fsmCom.EnterIdle();
        }

        public void FSM_EnterDead() {
            fsmCom.EnterDead();
        }

        // Mod
        public void Mod_Set(BubbleMod mod) {
            bubbleMod = mod;
        }

        // VFX
        public void TearDown() {
            Array.Clear(path, 0, path.Length);
            vfxCom.Clear();
            bubbleMod?.TearDown();
            Destroy(this.gameObject);
        }

    }

}