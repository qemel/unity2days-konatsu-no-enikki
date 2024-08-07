using AnnulusGames.LucidTools.Audio;
using R3;
using R3.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace u1d202408.Presenter
{
    public sealed class AudioVolumePresenter : MonoBehaviour
    {
        [SerializeField] Slider _sliderBgm;
        [SerializeField] Slider _sliderSfx;

        [SerializeField] AudioClip _sfxTester;


        void Awake()
        {
            _sliderBgm
                .OnValueChangedAsObservable()
                .Subscribe(x => { LucidAudio.BGMVolume = x; })
                .AddTo(gameObject);

            _sliderSfx
                .OnValueChangedAsObservable()
                .Subscribe(x => { LucidAudio.SEVolume = x; })
                .AddTo(gameObject);

            _sliderSfx
                .OnPointerUpAsObservable()
                .Subscribe(_ => { LucidAudio.PlaySE(_sfxTester).SetTimeSamples(); })
                .AddTo(gameObject);

            LucidAudio.BGMVolume = 0.3f;
            LucidAudio.SEVolume = 0.3f;

            _sliderBgm.value = LucidAudio.BGMVolume;
            _sliderSfx.value = LucidAudio.SEVolume;
        }
    }
}