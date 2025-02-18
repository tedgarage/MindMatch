
using UnityEngine;
using DG.Tweening;
using Jambav.Utilities;
using System;

public class AudioManager : Singleton<AudioManager>
{
    #region EDITOR VARIABLES

    [Header("Audio Sources")]
    [SerializeField] private AudioSource mainBGAudioSource;
    [SerializeField] private AudioSource mainSFXAudioSource;
    [SerializeField] private AudioSource timerTickingAudioSource;

    #region  SFX
    [Header("World Clips")]
    [SerializeField] private AudioClip correctAnswerClip;
    [SerializeField] private AudioClip wrongAnswerClip;
    [SerializeField] private AudioClip cardReveal;
    [SerializeField] private AudioClip timerTickClip;
    #endregion

    #endregion

    #region PRIVATE VARIABLES
    private readonly Tweener mainBgTweener;
    #endregion

    #region UNITY METHODS
    private void Update()
    {

    }
    #endregion

    #region MAIN BGM
    public void StartPlayMainBGAudio()
    {
        //mainBG_AudioSource.Stop();
        mainBGAudioSource.gameObject.SetActive(true);
        mainBGAudioSource.volume = 0f;
        mainBGAudioSource.Play();
        AdjustVolume(1f, mainBGAudioSource, mainBgTweener);
    }

    public void DecreaseMainBGVolumeAfterIntro()
    {
        AdjustVolume(.1f, mainBGAudioSource, mainBgTweener);
    }


    public void StopPlayMainBGAudio()
    {
        float value = mainBGAudioSource.volume;
        Tweener tweener = DOVirtual.Float(value, 0, 0.25f, val =>
        {
            mainBGAudioSource.volume = val;

        }).OnComplete(() =>
        {
            mainBGAudioSource.Stop();
        });
    }
    public void StopBGPlayNewAudio(AudioClip _audioClip)
    {
        float value = mainBGAudioSource.volume;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(DOVirtual.Float(value, 0, .1f, val =>
        {
            mainBGAudioSource.volume = val;

        }));
        sequence.AppendCallback(() =>
        {

            mainBGAudioSource.clip = _audioClip;
            mainBGAudioSource.Play();
        });
        sequence.Append(DOVirtual.Float(0, .1f, .1f, val =>
      {
          mainBGAudioSource.volume = val;

      }));

    }

    #endregion

    #region SFX Audio


    public void PlayCorrectAnswerClip()
    {
        if (correctAnswerClip == null) return;
        mainSFXAudioSource.PlayOneShot(correctAnswerClip, .4f);
    }
    public void PlayWrongAnswerClip()
    {
        if (wrongAnswerClip == null) return;
        mainSFXAudioSource.PlayOneShot(wrongAnswerClip, 1f);
    }
    public void PlayCardRevealClip()
    {
        if (cardReveal == null) return;
        mainSFXAudioSource.PlayOneShot(cardReveal, 10);
    }

    public void PlayTimerTickClip()
    {
        if (timerTickClip == null) return;
        timerTickingAudioSource.PlayOneShot(timerTickClip, .5f);
    }
   
    #endregion

    #region  PRIVATE METHODS
    private void AdjustVolume(float _volumeLevel, AudioSource _audioSource, Tweener _tween, float duration = 0.5f)
    {
        if (_tween != null && _tween.IsActive())
        {
            _tween.Kill();
        }
        _tween = DOVirtual.Float(_audioSource.volume, _volumeLevel, duration, val =>
        {
            _audioSource.volume = val;

        });
    }
    private void PlayAudio(AudioSource _audioSource, AudioClip clip, float volume = 1)
    {
        _audioSource.PlayOneShot(clip, volume);
    }
    private void ClearAudioSource(AudioSource _audioSource)
    {
        _audioSource.Stop();
        // _audioSource.volume = 0;
        _audioSource.clip = null;

    }


    #endregion
}