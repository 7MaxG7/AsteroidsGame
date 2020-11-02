using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioManager {
    static AudioSource audioSource;
    static Dictionary<AudioClipName, AudioClip> audioClips = new Dictionary<AudioClipName, AudioClip>();
    public static bool Initialized { get; private set; } = false;

    public static void Initialize(AudioSource source) {
        Initialized = true;
        audioSource = source;
        audioClips.Add(AudioClipName.AsteroidHit, Resources.Load<AudioClip>(@"audio\ExplosionLow"));
        audioClips.Add(AudioClipName.MenuButton, Resources.Load<AudioClip>(@"audio\MenuButton"));
        audioClips.Add(AudioClipName.PlayerDeath, Resources.Load<AudioClip>(@"audio\ExplosionHigh"));
        audioClips.Add(AudioClipName.PlayerShot, Resources.Load<AudioClip>(@"audio\Laser"));
    }

    public static void Play(AudioClipName name) {
        audioSource.PlayOneShot(audioClips[name]);
    }
}
