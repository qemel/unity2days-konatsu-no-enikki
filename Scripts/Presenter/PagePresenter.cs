using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AnnulusGames.LucidTools.Audio;
using Cysharp.Threading.Tasks;
using R3;
using u1d202408.Model;
using u1d202408.View;
using UnityEngine;
using Random = UnityEngine.Random;

namespace u1d202408.Presenter
{
    /// <summary>
    ///     神
    /// </summary>
    public sealed class PagePresenter : MonoBehaviour
    {
        [SerializeField] GameRegistry _gameRegistry;
        [SerializeField] PageScoreGaugeUIView _pageScoreGaugeUIView;
        [SerializeField] BookView _bookView;
        [SerializeField] FurinViewRoot _furinViewRoot;

        [SerializeField] PageMoveButtonUIView _nextPageButton;
        [SerializeField] PageMoveButtonUIView _prevPageButton;
        [SerializeField] RetryButtonUIView _retryButtonUIView;

        [SerializeField] List<ItemView> _disableOnLastPage;

        [SerializeField] float _clickInterval;
        [SerializeField] float _clickIntervalPageMovement;

        List<ItemView> _itemViews;
        
        CancellationTokenSource _bgmChangeTokenSource;
        CancellationTokenSource _seLoopTokenSource;

        public void Initialize()
        {
            _itemViews = GetComponentsInChildren<ItemView>(true).ToList();

            // model -> view
            // スコア更新
            _gameRegistry
                .CurrentPageScore
                .Subscribe(
                    _ =>
                    {
                        _pageScoreGaugeUIView.SetFillAmount(_gameRegistry.RequirementRate);

                        if (_gameRegistry.FilledRequirement) _gameRegistry.GoToNextPage();
                    }
                )
                .AddTo(gameObject);

            // ページ更新
            _gameRegistry
                .CurrentPageNumber
                .Subscribe(
                    page =>
                    {
                        HandleAudio();

                        if (_gameRegistry.IsInLastPage)
                        {
                            _pageScoreGaugeUIView.gameObject.SetActive(false);
                            _retryButtonUIView.Activate();
                        }

                        _bookView.SetCurrentPages(_gameRegistry.CurrentPageVisual);
                        _bookView.SetNextPages(_gameRegistry.NextPageVisual);
                        foreach (var itemView in _itemViews)
                        {
                            if (page >= itemView.AppearPageNumber)
                                itemView.Show();
                            else
                                itemView.Hide().Forget();
                        }

                        if (_gameRegistry.IsInLastPage)
                        {
                            foreach (var item in _disableOnLastPage)
                            {
                                item.HideForce().Forget();
                            }
                        }

                        _furinViewRoot.FurinClickView.ChangeSprite(_gameRegistry.IsScatterPage);

                        _prevPageButton.gameObject.SetActive(!_gameRegistry.IsInFirstPage);

                        if (_gameRegistry.IsInAchievedPage && !_gameRegistry.IsInMaxAchievedPage)
                            _nextPageButton.gameObject.SetActive(true);
                        else
                            _nextPageButton.gameObject.SetActive(false);
                    }
                )
                .AddTo(gameObject);

            // view -> model
            // 風鈴のクリック
            _furinViewRoot.FurinClickView.OnClick
                          .ThrottleFirst(TimeSpan.FromSeconds(_clickInterval))
                          .Subscribe(
                              _ =>
                              {
                                  _gameRegistry.Add(new PageScore(1));
                                  _furinViewRoot.FurinSound.PlayClickedSfx();
                                  _furinViewRoot.FurinClickView.PlayClickAnimation(_gameRegistry.IsScatterPage)
                                                .Forget();
                              }
                          )
                          .AddTo(gameObject);

            // ページ移動ボタン
            _nextPageButton
                .OnClick
                .ThrottleFirst(TimeSpan.FromSeconds(_clickIntervalPageMovement))
                .Subscribe(x => { _gameRegistry.GoToNextPage(); })
                .AddTo(gameObject);

            _prevPageButton
                .OnClick
                .ThrottleFirst(TimeSpan.FromSeconds(_clickIntervalPageMovement))
                .Subscribe(x => { _gameRegistry.GoToPreviousPage(); })
                .AddTo(gameObject);
        }

        void HandleAudio()
        {
            var bgm = _gameRegistry.CurrentPageAudio.Bgm;
            var sfxs = _gameRegistry.CurrentPageAudio.RandomSfxs;

            if (_gameRegistry.CurrentPageAudio.ShouldChangeBgm)
            {
                LucidAudio.StopAllBGM(2f);
                LucidAudio.PlayBGM(bgm, 2f).SetLoop().SetVolume(_gameRegistry.CurrentPageAudio.BgmVolume);
            }

            if (_gameRegistry.CurrentPageAudio.HasRandomSfxs)
            {
                _seLoopTokenSource?.Cancel();
                _seLoopTokenSource = new CancellationTokenSource();
                PlaySeLoop(sfxs, _seLoopTokenSource.Token).Forget();
            }
        }

        static async UniTask PlaySeLoop(List<AudioClip> sfxs, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                LucidAudio.PlaySE(sfxs[Random.Range(0, sfxs.Count)]);
                await UniTask.Delay(TimeSpan.FromSeconds(Random.Range(5f, 15f)), cancellationToken: token);
            }
        }

        async UniTask ChangeBgmAsync(AudioClip bgm, CancellationToken token)
        {
            var currentVolume = LucidAudio.BGMVolume;

            var duration = 1f;
            var startTime = Time.time;
            while (Time.time - startTime < duration)
            {
                LucidAudio.BGMVolume = Mathf.Lerp(currentVolume, 0, (Time.time - startTime) / duration);
                await UniTask.Yield(PlayerLoopTiming.Update, token);
            }

            LucidAudio.PlayBGM(bgm).SetLoop().SetVolume(0.3f);
            duration = 1f;
            while (Time.time - startTime < duration)
            {
                LucidAudio.BGMVolume = Mathf.Lerp(0, currentVolume, (Time.time - startTime) / duration);
                await UniTask.Yield(PlayerLoopTiming.Update, token);
            }
        }
    }
}