using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using LitMotion;
using LitMotion.Extensions;
using TMPro;
using UnityEngine;

namespace u1d202408.View
{
    public sealed class TextLineView : MonoBehaviour
    {
        [SerializeField] float _gridSize = 0.45f;
        List<TMP_Text> _singleTexts = new();

        public void Init()
        {
            _singleTexts = GetComponentsInChildren<TMP_Text>(true).ToList();

            for (var i = 0; i < _singleTexts.Count; i++)
            {
                _singleTexts[i].transform.localPosition = new Vector3(0, -_gridSize * i, 0);
            }
        }

        public async UniTask UpdateTextAnimation(string text)
        {
            foreach (var tex in _singleTexts)
            {
                LMotion
                    .Create(1f, 0f, 1f)
                    .WithEase(Ease.InSine)
                    .BindToColorA(tex);
            }

            await UniTask.Delay(1000);

            for (var i = 0; i < _singleTexts.Count; i++)
            {
                _singleTexts[i].text = i < text.Length ? text[i].ToString() : string.Empty;
            }

            foreach (var tex in _singleTexts)
            {
                LMotion
                    .Create(0f, 1f, 1f)
                    .WithEase(Ease.InSine)
                    .BindToColorA(tex);
            }
        }
    }
}