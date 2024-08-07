using System.Collections.Generic;
using AnnulusGames.LucidTools.Audio;
using UnityEngine;

namespace u1d202408.View
{
    public sealed class FurinSound : MonoBehaviour
    {
        [SerializeField] List<AudioClip> _clickedSfxs;

        public void PlayClickedSfx()
        {
            var randomIndex = Random.Range(0, _clickedSfxs.Count);
            LucidAudio.PlaySE(_clickedSfxs[randomIndex]).SetTimeSamples();
        }
    }
}