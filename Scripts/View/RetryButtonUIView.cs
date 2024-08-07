using LitMotion;
using LitMotion.Extensions;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace u1d202408.View
{
    public sealed class RetryButtonUIView : MonoBehaviour
    {
        [SerializeField] Button _button;
        [SerializeField] TextMeshProUGUI _textMeshProUGUI;
        readonly Subject<Unit> _onClick = new();

        public Observable<Unit> OnClick => _onClick;

        void Awake()
        {
            _button.interactable = false;
            gameObject.SetActive(false);
            _button
                .OnClickAsObservable()
                .Subscribe(_ => _onClick.OnNext(Unit.Default))
                .AddTo(gameObject);
        }

        public void Activate()
        {
            // _button.image.color = new Color(1f, 1f, 1f, 0f);
            _textMeshProUGUI.color = new Color(0f, 0f, 0f, 0f);
            gameObject.SetActive(true);

            // LMotion
            //     .Create(0f, 1f, 2f)
            //     .BindToColorA(_button.image);
            LMotion
                .Create(0f, 1f, 2f)
                .BindToColorA(_textMeshProUGUI);

            _button.interactable = true;
        }
    }
}