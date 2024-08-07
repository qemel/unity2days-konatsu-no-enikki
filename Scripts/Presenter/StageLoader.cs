using R3;
using u1d202408.View;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace u1d202408.Presenter
{
    public sealed class StageLoader : MonoBehaviour
    {
        [SerializeField] RetryButtonUIView _retryButtonUIView;

        void Awake()
        {
            _retryButtonUIView.OnClick.Subscribe(_ => ReloadStage()).AddTo(gameObject);
        }

        static void ReloadStage()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}