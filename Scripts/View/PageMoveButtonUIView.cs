using R3;
using UnityEngine;
using UnityEngine.UI;

namespace u1d202408.View
{
    public sealed class PageMoveButtonUIView : MonoBehaviour
    {
        [SerializeField] Button _button;
        readonly Subject<Unit> _onClick = new();

        public Observable<Unit> OnClick => _onClick;

        void Awake()
        {
            _button
                .OnClickAsObservable()
                .Subscribe(_ => _onClick.OnNext(Unit.Default))
                .AddTo(gameObject);
        }
    }
}