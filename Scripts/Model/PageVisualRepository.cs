using System.Collections.Generic;

namespace u1d202408.Model
{
    public sealed class PageVisualRepository
    {
        readonly Dictionary<PageNumber, PageVisual> _pageVisuals = new();

        public PageVisualRepository(List<PageVisual> visuals)
        {
            for (var i = 0; i < visuals.Count; i++)
            {
                _pageVisuals.Add(new PageNumber(i), visuals[i]);
            }
        }

        public PageVisual Get(PageNumber pageNumber)
        {
            return _pageVisuals.GetValueOrDefault(pageNumber);
        }
    }
}