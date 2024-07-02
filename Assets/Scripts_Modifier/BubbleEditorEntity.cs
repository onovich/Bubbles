#if UNITY_EDITOR
using UnityEngine;

namespace Bubbles.Modifier {

    public class BubbleEditorEntity : MonoBehaviour {

        [SerializeField] BubbleTM bubbleTM;

        public void Rename() {
            this.gameObject.name = $"EnemyEditor";
        }

        public BubbleTM GetBubbleTM() {
            return bubbleTM;
        }

        public Vector2 GetSize() {
            return transform.GetChild(0).transform.localScale;
        }

        public Vector2 GetPos() {
            return transform.position;
        }

    }

}
#endif