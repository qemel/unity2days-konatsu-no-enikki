using UnityEngine;

namespace u1d202408.View
{
    /// <summary>
    ///     風鈴
    /// </summary>
    public sealed class FurinViewRoot : MonoBehaviour
    {
        [SerializeField] FurinClickView _furinClickView;
        [SerializeField] FurinSound _furinSound;

        public FurinClickView FurinClickView => _furinClickView;
        public FurinSound FurinSound => _furinSound;
    }
}