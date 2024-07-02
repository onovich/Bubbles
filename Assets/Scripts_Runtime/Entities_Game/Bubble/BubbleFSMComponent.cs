using UnityEngine;

namespace Bubbles {

    public class BubbleFSMComponent {

        public BubbleFSMStatus status;

        public bool idle_isEntering;
        public bool dead_isEntering;
        public bool casting_isEntering;

        public BubbleFSMComponent() { }

        public void EnterIdle() {
            status = BubbleFSMStatus.Idle;
            idle_isEntering = true;
        }

        public void EnterDead() {
            status = BubbleFSMStatus.Dead;
            dead_isEntering = true;
        }

    }

}