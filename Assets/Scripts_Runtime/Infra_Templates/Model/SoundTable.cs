using UnityEngine;

namespace Bubbles    {

    [CreateAssetMenu(fileName = "SoundTable", menuName = "Oshi/SoundTable")]
    public class SoundTable : ScriptableObject {

        [Header("Bubble SE")]
        public AudioClip bubbleBroken;
        public float bubbleBrokenVolume;

        [Header("BGM")]
        public AudioClip bgmLoop;
        public float bgmVolume;

    }

}