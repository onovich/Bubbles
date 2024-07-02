using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Bubbles {

    public static class SoundApp {

        public static async Task LoadAssets(SoundAppContext ctx) {

            var handle = Addressables.LoadAssetAsync<GameObject>("Sound_AudioSource");
            var prefab = await handle.Task;
            ctx.audioSourcePrefab = prefab.GetComponent<AudioSource>();
            ctx.assetsHandle = handle;

            var bgmAudio = GameObject.Instantiate(ctx.audioSourcePrefab, ctx.root);
            bgmAudio.name = "BGMPlayer";
            ctx.bgmPlayer = bgmAudio;

            for (int i = 0; i < ctx.bubbleWalkPlayer.Length; i++) {
                var audio = GameObject.Instantiate(ctx.audioSourcePrefab, ctx.root);
                audio.name = "BubbleWalkSE - " + i;
                ctx.bubbleWalkPlayer[i] = audio;
            }

            for (int i = 0; i < ctx.bubbleHurtPlayer.Length; i++) {
                var audio = GameObject.Instantiate(ctx.audioSourcePrefab, ctx.root);
                audio.name = "BubbleHurtSE - " + i;
                ctx.bubbleHurtPlayer[i] = audio;
            }

            for (int i = 0; i < ctx.bubbleAttackPlayer.Length; i++) {
                var audio = GameObject.Instantiate(ctx.audioSourcePrefab, ctx.root);
                audio.name = "BubbleAttackSE - " + i;
                ctx.bubbleAttackPlayer[i] = audio;
            }

            for (int i = 0; i < ctx.bubbleHitPlayer.Length; i++) {
                var audio = GameObject.Instantiate(ctx.audioSourcePrefab, ctx.root);
                audio.name = "BubbleHitSE - " + i;
                ctx.bubbleHitPlayer[i] = audio;
            }

        }

        public static void ReleaseAssets(SoundAppContext ctx) {
            if (ctx.assetsHandle.IsValid()) {
                Addressables.Release(ctx.assetsHandle);
            }
        }

        public static void TearDown(SoundAppContext ctx) {
            foreach (var player in ctx.bubbleWalkPlayer) {
                player.Stop();
                GameObject.Destroy(player.gameObject);
            }
            foreach (var player in ctx.bubbleHurtPlayer) {
                player.Stop();
                GameObject.Destroy(player.gameObject);
            }
            foreach (var player in ctx.bubbleAttackPlayer) {
                player.Stop();
                GameObject.Destroy(player.gameObject);
            }
            foreach (var player in ctx.bubbleHitPlayer) {
                player.Stop();
                GameObject.Destroy(player.gameObject);
            }
            ctx.bgmPlayer.Stop();
            GameObject.Destroy(ctx.bgmPlayer.gameObject);
        }

        public static void BGM_PlayLoop(SoundAppContext ctx, AudioClip clip, int layer, float volume, bool replay) {
            var player = ctx.bgmPlayer;
            if (player.isPlaying && !replay) {
                return;
            }

            player.clip = clip;
            player.Play();
            player.loop = true;
            player.volume = volume;
        }

        public static void BGM_Stop(SoundAppContext ctx, int layer) {
            var player = ctx.bgmPlayer;
            player.Stop();
        }

        public static void Bubble_Walk(SoundAppContext ctx, AudioClip clip, float volume) {
            PlayWhenFree(ctx, ctx.bubbleWalkPlayer, clip, volume);
        }

        public static void Bubble_Hurt(SoundAppContext ctx, AudioClip clip, float volume) {
            PlayWhenFree(ctx, ctx.bubbleHurtPlayer, clip, volume);
        }

        public static void Bubble_Attack(SoundAppContext ctx, AudioClip clip, float volume) {
            PlayWhenFree(ctx, ctx.bubbleAttackPlayer, clip, volume);
        }

        public static void Bubble_Hit(SoundAppContext ctx, AudioClip clip, float volume) {
            PlayWhenFree(ctx, ctx.bubbleHitPlayer, clip, volume);
        }

        public static float GetVolume(Vector2 listenerPos, Vector2 hitPos, float thresholdDistance, float volume) {
            float dis = Vector2.Distance(listenerPos, hitPos);
            if (dis >= thresholdDistance) {
                return 0;
            }
            return (1 - dis / thresholdDistance) * volume;
        }

        public static void SetMuteAll(SoundAppContext ctx, bool isMute) {
            foreach (var player in ctx.bubbleWalkPlayer) {
                player.mute = isMute;
            }
            foreach (var player in ctx.bubbleHurtPlayer) {
                player.mute = isMute;
            }
            foreach (var player in ctx.bubbleAttackPlayer) {
                player.mute = isMute;
            }
            foreach (var player in ctx.bubbleHitPlayer) {
                player.mute = isMute;
            }
            ctx.bgmPlayer.mute = isMute;
        }

        static void PlayWhenFree(SoundAppContext ctx, AudioSource[] players, AudioClip clip, float volume) {
            if (clip == null || volume <= 0) {
                return;
            }
            foreach (var player in players) {
                if (!player.isPlaying) {
                    player.clip = clip;
                    player.Play();
                    player.volume = volume;
                    return;
                }
            }
        }

    }

}