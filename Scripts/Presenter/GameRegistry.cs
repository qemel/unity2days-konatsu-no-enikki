using System;
using R3;
using u1d202408.Model;
using UnityEngine;

namespace u1d202408.Presenter
{
    public sealed class GameRegistry : MonoBehaviour
    {
        readonly ReactiveProperty<PageNumber> _currentPageNumber = new(new PageNumber(0));
        readonly ReactiveProperty<PageScore> _currentPageScore = new(new PageScore(0));

        PageAudioRepository _pageAudioRepository;
        /// <summary>
        ///     最初に設定する
        /// </summary>
        PageRequirementRepository _pageRequirementRepository;
        PageVisualRepository _pageVisualRepository;

        /// <summary>
        ///     現在のページ番号
        /// </summary>
        public ReadOnlyReactiveProperty<PageNumber> CurrentPageNumber => _currentPageNumber;

        public PageNumber MaxAchievedPageNumber { get; private set; } = new(0);

        /// <summary>
        ///     現在のページの遷移要件
        /// </summary>
        public PageScoreRequirement CurrentPageScoreRequirement
        {
            get
            {
                var requirement = _pageRequirementRepository.Get(_currentPageNumber.CurrentValue);
                if (requirement == new PageScoreRequirement(0))
                    throw new Exception($"PageScoreRequirement is not set. PageNumber: {_currentPageNumber.Value}");

                return requirement;
            }
        }

        /// <summary>
        ///     現在のページのスコア
        /// </summary>
        public ReadOnlyReactiveProperty<PageScore> CurrentPageScore => _currentPageScore;

        public PageAudio CurrentPageAudio => _pageAudioRepository.Get(_currentPageNumber.CurrentValue);

        public PageVisual CurrentPageVisual => _pageVisualRepository.Get(_currentPageNumber.Value);
        public PageVisual NextPageVisual => _pageVisualRepository.Get(_currentPageNumber.Value.Next());

        /// <summary>
        ///     条件達成しているか
        /// </summary>
        public bool FilledRequirement => CurrentPageScore.CurrentValue.Value >=
                                         CurrentPageScoreRequirement.Value;

        /// <summary>
        ///     どのくらい条件を満たしているか
        /// </summary>
        public float RequirementRate => (float)CurrentPageScore.CurrentValue.Value /
                                        CurrentPageScoreRequirement.Value;

        /// <summary>
        ///     最後のページ番号にいるか
        /// </summary>
        public bool IsInLastPage => _currentPageNumber.Value.Value + 1 == _pageRequirementRepository.Count();

        /// <summary>
        ///     最初のページ番号にいるか
        /// </summary>
        public bool IsInFirstPage => _currentPageNumber.Value.Value == 0;

        /// <summary>
        ///     風鈴が割れるページか
        /// </summary>
        /// <returns></returns>
        public bool IsScatterPage => _currentPageNumber.Value.Value >= 2;

        /// <summary>
        ///     既に達成済みのページにいるか
        /// </summary>
        public bool IsInAchievedPage => MaxAchievedPageNumber >= _currentPageNumber.CurrentValue;

        /// <summary>
        ///     最大達成ページにいるか
        /// </summary>
        public bool IsInMaxAchievedPage => MaxAchievedPageNumber == _currentPageNumber.CurrentValue;

        public void Init(
            PageRequirementRepository repository, PageVisualRepository visualRepository,
            PageAudioRepository audioRepository
        )
        {
            if (_pageRequirementRepository != null) throw new ArgumentNullException(nameof(repository));
            if (_pageVisualRepository != null) throw new ArgumentNullException(nameof(visualRepository));
            if (_pageAudioRepository != null) throw new ArgumentNullException(nameof(audioRepository));

            _pageRequirementRepository = repository;
            _pageVisualRepository = visualRepository;
            _pageAudioRepository = audioRepository;

            _currentPageNumber.Subscribe(
                                  pageNumber =>
                                  {
                                      MaxAchievedPageNumber =
                                          new PageNumber(Mathf.Max(MaxAchievedPageNumber.Value, pageNumber.Value));
                                  }
                              )
                              .AddTo(gameObject);

            _currentPageNumber.AddTo(gameObject);
            _currentPageScore.AddTo(gameObject);
        }

        public void Set(PageNumber pageNumber)
        {
            _currentPageNumber.Value = pageNumber;
        }

        public void SetNextPageNumber()
        {
            _currentPageNumber.Value = _currentPageNumber.Value.Next();
        }

        public void Set(PageScore pageScore)
        {
            _currentPageScore.Value = pageScore;
        }

        public void Add(PageScore pageScore)
        {
            _currentPageScore.Value += pageScore;
        }

        public void GoToNextPage()
        {
            if (IsInLastPage) return;
            SetNextPageNumber();
            Set(new PageScore(0));
        }

        public void GoToPreviousPage()
        {
            if (IsInFirstPage) return;
            _currentPageNumber.Value = _currentPageNumber.Value.Previous();
            Set(new PageScore(0));
        }
    }
}