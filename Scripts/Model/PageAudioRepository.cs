using System.Collections.Generic;

namespace u1d202408.Model
{
    public sealed class PageAudioRepository
    {
        readonly Dictionary<PageNumber, PageAudio> _pageAudios = new();

        public PageAudioRepository(List<PageAudio> audios)
        {
            for (var i = 0; i < audios.Count; i++)
            {
                _pageAudios.Add(new PageNumber(i), audios[i]);
            }
        }

        public PageAudio Get(PageNumber pageNumber)
        {
            return _pageAudios.GetValueOrDefault(pageNumber);
        }
    }
}