using UnityEngine;

namespace u1d202408.View
{
    public sealed class BGMPlayer : MonoBehaviour
    {
        [SerializeField] AudioClip _audioClip;

        void Awake()
        {
            // LucidAudio.PlayBGM(_audioClip)
            //     // .SetVolume(0.3f)
            //     .SetLink(gameObject)
            //     .SetLoop();
        }
    }
}