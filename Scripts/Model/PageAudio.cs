using System;
using System.Collections.Generic;
using UnityEngine;

namespace u1d202408.Model
{
    [Serializable]
    public sealed class PageAudio
    {
        [SerializeField] AudioClip _bgm;
        [SerializeField] List<AudioClip> _randomSfxs;
        [SerializeField] float _bgmVolume;


        public bool ShouldChangeBgm => _bgm != null;
        public bool HasRandomSfxs => _randomSfxs.Count > 0;
        public AudioClip Bgm => _bgm;
        public float BgmVolume => _bgmVolume;
        public List<AudioClip> RandomSfxs => _randomSfxs;
    }
}