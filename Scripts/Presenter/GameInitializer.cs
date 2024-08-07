using u1d202408.Data;
using u1d202408.View;
using UnityEngine;

namespace u1d202408.Presenter
{
    /// <summary>
    ///     ゲーム全体の初期化
    /// </summary>
    public sealed class GameInitializer : MonoBehaviour
    {
        [SerializeField] PagePresenter _pagePresenter;
        [SerializeField] BookView _bookView;
        [SerializeField] PageRequirementSO _pageRequirementSO;
        [SerializeField] PageVisualSO _pageVisualSO;
        [SerializeField] PageAudioSO _pageAudioSO;

        [SerializeField] GameRegistry _gameRegistry;

        public void Initialize()
        {
            _bookView.Init();
            _gameRegistry.Init(_pageRequirementSO.Create(), _pageVisualSO.Create(), _pageAudioSO.Create());
            _pagePresenter.Initialize();
        }
    }
}