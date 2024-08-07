using System;
using System.Collections.Generic;
using UnityEngine;

namespace u1d202408.Model
{
    [Serializable]
    public sealed class PageVisual
    {
        [SerializeField] List<Sprite> _leftIllustrations;
        [SerializeField] List<Sprite> _rightIllustrations;

        [SerializeField] string _diaryText;

        public IEnumerable<Sprite> LeftIllustrations => _leftIllustrations;
        public IEnumerable<Sprite> RightIllustrations => _rightIllustrations;
        public string DiaryText => _diaryText;
    }
}