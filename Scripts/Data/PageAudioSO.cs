using System.Collections.Generic;
using u1d202408.Model;
using UnityEngine;

namespace u1d202408.Data
{
    [CreateAssetMenu(fileName = "PageAudioSO", menuName = "Create PageAudioSO")]
    public sealed class PageAudioSO : ScriptableObject
    {
        [SerializeField] List<PageAudio> _audios;

        public PageAudioRepository Create()
        {
            return new PageAudioRepository(_audios);
        }
    }
}