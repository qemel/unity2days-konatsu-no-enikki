using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using LitMotion;
using LitMotion.Extensions;
using u1d202408.Model;
using UnityEngine;

namespace u1d202408.View
{
    public sealed class ItemView : MonoBehaviour
    {
        [SerializeField] int _appearPageNumber;
        [SerializeField] SpriteRenderer _spriteRenderer;

        [SerializeField] List<Sprite> _sprites;

        [SerializeField] float _appaerDuration = 3f;
        [SerializeField] float _animationSpeed;

        CancellationToken _cancellationToken;

        bool _isAppeared;

        public PageNumber AppearPageNumber => new(_appearPageNumber);

        void Awake()
        {
            if (AppearPageNumber != new PageNumber(0)) gameObject.SetActive(false);

            _cancellationToken = this.GetCancellationTokenOnDestroy();
        }

        async UniTask PlayAnimation(CancellationToken token)
        {
            while (token.IsCancellationRequested == false)
            {
                foreach (var sprite in _sprites)
                {
                    _spriteRenderer.sprite = sprite;
                    await UniTask.Delay((int)(_animationSpeed * 1000), cancellationToken: token);
                }
            }
        }

        public void Show()
        {
            if (_isAppeared) return;

            _spriteRenderer.color = new Color(1f, 1f, 1f, 0f);
            gameObject.SetActive(true);

            LMotion
                .Create(0f, 1f, _appaerDuration)
                .WithEase(Ease.InSine)
                .BindToColorA(_spriteRenderer);

            PlayAnimation(_cancellationToken).Forget();

            _isAppeared = true;
        }

        public async UniTask Hide()
        {
            if (!_isAppeared) return;

            await LMotion
                  .Create(1f, 0f, _appaerDuration)
                  .WithEase(Ease.OutSine)
                  .BindToColorA(_spriteRenderer);

            gameObject.SetActive(false);
            _isAppeared = false;
        }

        public async UniTask HideForce()
        {
            await LMotion
                  .Create(1f, 0f, _appaerDuration)
                  .WithEase(Ease.OutSine)
                  .BindToColorA(_spriteRenderer);

            gameObject.SetActive(false);
        }
    }
}