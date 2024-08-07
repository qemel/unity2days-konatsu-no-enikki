using u1d202408.Model;
using UnityEngine;

namespace u1d202408.View
{
    public sealed class BookView : MonoBehaviour
    {
        [SerializeField] PageView _pageLeftView;
        [SerializeField] PageView _pageRightView;
        [SerializeField] PageView _nextLeftView;
        [SerializeField] PageView _nextRightView;

        [SerializeField] float _rotateDuration;

        public void Init()
        {
            _pageLeftView.Init();
            _pageRightView.Init();
            _nextLeftView.Init();
            _nextRightView.Init();
        }

        public void SetCurrentPages(PageVisual pageVisual)
        {
            if (pageVisual == null) return;
            _pageLeftView.SetPage(pageVisual.LeftIllustrations, pageVisual.DiaryText, true);
            _pageRightView.SetPage(pageVisual.RightIllustrations, pageVisual.DiaryText, false);
        }

        public void SetNextPages(PageVisual pageVisual)
        {
            if (pageVisual == null) return;
            _nextLeftView.SetPage(pageVisual.LeftIllustrations, pageVisual.DiaryText, true);
            _nextRightView.SetPage(pageVisual.RightIllustrations, pageVisual.DiaryText, false);
        }
    }
}