using System.Collections.Generic;
using u1d202408.Model;
using UnityEngine;

namespace u1d202408.Data
{
    [CreateAssetMenu(fileName = "PageRequirementSO", menuName = "Create PageRequirementSO")]
    public sealed class PageRequirementSO : ScriptableObject
    {
        [SerializeField] List<int> _pageScoreRequirements = new();

        /// <summary>
        ///     ページ番号とそのページの遷移要件を紐づける
        /// </summary>
        public PageRequirementRepository Create()
        {
            return new PageRequirementRepository(_pageScoreRequirements);
        }
    }
}