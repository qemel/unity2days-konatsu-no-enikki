using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace u1d202408.View
{
    public sealed class PageView : MonoBehaviour
    {
        const int MaxTextLengthPerLine = 7;
        const int MaxTextLineCount = 6;
        [SerializeField] SpriteRenderer _spriteRenderer;
        [SerializeField] List<TextLineView> _textLineViews;
        [SerializeField] float _animationSpeed;
        CancellationToken _cancellationToken;

        CancellationTokenSource _cancellationTokenSource;

        public int MaxTextLength => MaxTextLengthPerLine * MaxTextLineCount;

        public void Init()
        {
            _textLineViews.ForEach(textLineView => textLineView.Init());
        }

        public void SetPage(IEnumerable<Sprite> sprites, string str, bool exceedsMax)
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
            PlayAnimation(sprites, _cancellationToken).Forget();

            if (exceedsMax && str.Length > MaxTextLength)
            {
                var strAfterMax = str[MaxTextLength..];
                SetAlignText(strAfterMax);
            }
            else
                SetAlignText(str);
        }

        async UniTask PlayAnimation(IEnumerable<Sprite> sprites, CancellationToken token)
        {
            while (token.IsCancellationRequested == false)
            {
                foreach (var sprite in sprites)
                {
                    _spriteRenderer.sprite = sprite;
                    await UniTask.Delay((int)(_animationSpeed * 1000), cancellationToken: token);
                }
            }
        }

        void SetAlignText(string str)
        {
            var text = str;
            var lines = new List<string>();

            while (text.Length > 0)
            {
                var line = text.Length > MaxTextLengthPerLine
                    ? text[..MaxTextLengthPerLine]
                    : text;
                lines.Add(line);
                text = text.Remove(0, line.Length);
            }

            for (var i = 0; i < _textLineViews.Count; i++)
            {
                _ = _textLineViews[i].UpdateTextAnimation(i < lines.Count ? lines[i] : string.Empty);
            }
        }
    }
}