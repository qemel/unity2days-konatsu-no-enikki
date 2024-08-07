using System.Collections.Generic;

namespace u1d202408.Model
{
    public sealed class PageRequirementRepository
    {
        readonly Dictionary<PageNumber, PageScoreRequirement> _pageScoreRequirements = new();

        public PageRequirementRepository(List<int> pageScoreRequirements)
        {
            for (var i = 0; i < pageScoreRequirements.Count; i++)
            {
                _pageScoreRequirements.Add(new PageNumber(i), new PageScoreRequirement(pageScoreRequirements[i]));
            }
        }

        public PageScoreRequirement Get(PageNumber pageNumber)
        {
            return _pageScoreRequirements[pageNumber];
        }

        public int Count()
        {
            return _pageScoreRequirements.Count;
        }
    }
}