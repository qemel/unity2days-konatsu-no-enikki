using System.Collections.Generic;
using u1d202408.Model;
using UnityEngine;

namespace u1d202408.Data
{
    [CreateAssetMenu(fileName = "PageVisualSO", menuName = "Create PageVisualSO")]
    public sealed class PageVisualSO : ScriptableObject
    {
        [SerializeField] List<PageVisual> _visuals;

        public PageVisualRepository Create()
        {
            return new PageVisualRepository(_visuals);
        }
    }
}