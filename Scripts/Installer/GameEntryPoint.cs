using u1d202408.Presenter;
using UnityEngine;

namespace u1d202408.Installer
{
    public sealed class GameEntryPoint : MonoBehaviour
    {
        [SerializeField] GameInitializer _gameInitializer;

        /// <summary>
        ///     ここ以外でAwakeしない
        /// </summary>
        void Awake()
        {
            _gameInitializer.Initialize();
        }
    }
}