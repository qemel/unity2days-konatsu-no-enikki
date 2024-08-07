using LitMotion;
using LitMotion.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace u1d202408.View
{
    public sealed class PageScoreGaugeUIView : MonoBehaviour
    {
        [SerializeField] Image _fillBack;
        [SerializeField] Image _fillFront;

        [SerializeField] float _animationSpeed;


        float _currentFillAmount;

        public void SetFillAmount(float fillAmount)
        {
            if (fillAmount == 0f)
            {
                _fillFront.fillAmount = 0f;
                _currentFillAmount = 0f;
                return;
            }

            LMotion
                .Create(_currentFillAmount, fillAmount, _animationSpeed)
                .WithEase(Ease.OutSine)
                .WithOnComplete(
                    () =>
                    {
                        if (!Mathf.Approximately(_currentFillAmount, 1f)) return;
                        _fillFront.fillAmount = 0f;
                        _currentFillAmount = 0f;
                    }
                )
                .BindToFillAmount(_fillFront);

            _currentFillAmount = fillAmount;
        }

        public void SetColor(Color color)
        {
            _fillBack.color = color;
            _fillFront.color = color;
        }
    }
}