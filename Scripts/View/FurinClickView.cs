using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using UnityEngine.EventSystems;

namespace u1d202408.View
{
    public sealed class FurinClickView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] SpriteRenderer _spriteRenderer;
        [SerializeField] float _animationSpeed;

        /// <summary>
        ///     割れてない方
        /// </summary>
        [SerializeField] Sprite _spriteDefault;
        [SerializeField] List<Sprite> _sprites;

        /// <summary>
        ///     割れた方
        /// </summary>
        [SerializeField] Sprite _scatteredSpriteDefault;
        [SerializeField] List<Sprite> _scatteredSprites;
        readonly Subject<Unit> _onClick = new();
        public Observable<Unit> OnClick => _onClick;

        void OnDestroy()
        {
            _onClick.OnCompleted();
        }


        public void OnPointerClick(PointerEventData eventData)
        {
            _onClick.OnNext(Unit.Default);
        }

        public async UniTask PlayClickAnimation(bool scatter)
        {
            var sprites = scatter ? _scatteredSprites : _sprites;

            foreach (var sprite in sprites)
            {
                _spriteRenderer.sprite = sprite;
                await UniTask.Delay((int)(_animationSpeed * 1000));
            }

            await UniTask.Delay((int)(_animationSpeed * 1000));

            _spriteRenderer.sprite = scatter ? _scatteredSpriteDefault : _spriteDefault;
        }

        public void ChangeSprite(bool scatter)
        {
            _spriteRenderer.sprite = scatter ? _scatteredSpriteDefault : _spriteDefault;
        }
    }
}