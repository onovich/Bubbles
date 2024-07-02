using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bubbles {

    public class BubbleVFXComponent {

        public List<int> vfxIDList;
        public BubbleVFXComponent() {
            vfxIDList = new List<int>();
        }

        public void AddVFX(int id) {
            vfxIDList.Add(id);
        }

        public void ForEach(Action<int> action) {
            vfxIDList.ForEach(action);
        }

        public void Clear() {
            vfxIDList.Clear();
        }

    }

}