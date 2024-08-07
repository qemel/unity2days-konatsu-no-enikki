using LitMotion;
using LitMotion.Extensions;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace u1d202408.View
{
    public sealed class FadeLoopView : MonoBehaviour
    {
        [FormerlySerializedAs("_buttonRef")] [SerializeField]
        FurinViewRoot _furin;

        bool _isClicked;

        TextMeshProUGUI _textMeshProUGUI;

        void Start()
        {
            _textMeshProUGUI = GetComponent<TextMeshProUGUI>();

            LMotion
                .Create(0.1f, 1f, 1.5f)
                .WithLoops(-1, LoopType.Yoyo)
                .BindToColorA(_textMeshProUGUI);

            _furin.FurinClickView.OnClick.Subscribe(
                      x =>
                      {
                          if (_isClicked) return;
                          LMotion
                              .Create(1f, 0f, 1.5f)
                              .WithOnComplete(() => gameObject.SetActive(false))
                              .BindToColorA(_textMeshProUGUI);

                          _isClicked = true;
                      }
                  )
                  .AddTo(gameObject);
        }
    }
}