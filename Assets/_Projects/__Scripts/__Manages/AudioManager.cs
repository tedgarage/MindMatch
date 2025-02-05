
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
    [SerializeField] private AudioSource timerTickingBGAudioSource;


    #region  BG
        [SerializeField] private AudioClip homeBg;
        [SerializeField] private AudioClip gameBg;
        [SerializeField] private AudioClip gameEndBg;
    #endregion
    #region  SFX
    [Header("World Clips")]
    [SerializeField] private AudioClip correctAnswerClip;
    [SerializeField] private AudioClip wrongAnswerClip;
    [SerializeField] private AudioClip buttonPressClip;
    [SerializeField] private AudioClip moveToNextQuestionClip;
    [SerializeField] private AudioClip timerTickClip;
    [SerializeField] private AudioClip timerTickingBgClip;
    [SerializeField] private AudioClip showInstructionClip;
    [SerializeField] private AudioClip gameEndClip;
    [SerializeField] private AudioClip reachedHighScoreClip;
    [SerializeField] private AudioClip countDownClip;
    [SerializeField] private AudioClip countDownGoClip;
    [SerializeField] private AudioClip answeredWrongDirection;
    [SerializeField] private AudioClip clapClip;
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
    

    public void PlayHomeBG()
    {
        StopBGPlayNewAudio(homeBg);
    }
    public void PlayGameBG()
    {
        StopBGPlayNewAudio(gameBg);
    }
    public void PlayGameEndBG()
    {
        StopBGPlayNewAudio(gameEndBg);
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
        Sequence sequence =DOTween.Sequence();
        sequence.Append(DOVirtual.Float(value, 0, .1f, val =>
        {
            mainBGAudioSource.volume = val;

        }));
        sequence.AppendCallback(()=>{

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


    public void PlayInstructionShowClip(Vector3 _position)
    {
        if (showInstructionClip == null) return;
        AudioSource.PlayClipAtPoint(showInstructionClip, _position, 10);
    }

    public void PlayCorrectAnswerClip()
    {
        if (correctAnswerClip == null) return;
        mainSFXAudioSource.PlayOneShot(correctAnswerClip, 4f);
    }
       public void PlayCorrectAnswerWrongDirectionClip()
    {
        if (answeredWrongDirection == null) return;
        mainSFXAudioSource.PlayOneShot(answeredWrongDirection, 8f);
    }
    public void PlayWrongAnswerClip()
    {
        if (wrongAnswerClip == null) return;
        mainSFXAudioSource.PlayOneShot(wrongAnswerClip, 10f);
    }
    public void PlayButtonPressClip()
    {
        if (buttonPressClip == null) return;
        mainSFXAudioSource.PlayOneShot(buttonPressClip, 1);
    }

    public void PlayTimerTickClip()
    {
        if (timerTickClip == null) return;
        timerTickingAudioSource.PlayOneShot(timerTickClip, .5f);
    }
    public void PlayCountDownClip()
    {
        if (countDownClip == null) return;
        timerTickingAudioSource.PlayOneShot(countDownClip, .5f);
    }
     public void PlayCountDownGoClip()
    {
        if (countDownGoClip == null) return;
        timerTickingAudioSource.PlayOneShot(countDownGoClip, .5f);
    }

    // public void PlayNormalTimerTickClip()
    // {p
    //     if (normalTimeTickClip == null) return;
    //     timerTickingAudioSource.PlayOneShot(normalTimeTickClip, .005f);
    // }
    public void PlayMoveToNextQuestionSound()
    {
        if (moveToNextQuestionClip == null) return;
        mainSFXAudioSource.PlayOneShot(moveToNextQuestionClip, 2);
    }
    public void PlayGameEndedClip()
    {
        if (gameEndClip == null) return;
        AudioSource.PlayClipAtPoint(gameEndClip, Vector3.zero, 5f);
    }
     public void PlayReachedHighScoreClip()
    {
        if (reachedHighScoreClip == null) return;
        AudioSource.PlayClipAtPoint(reachedHighScoreClip, Vector3.zero, 2);
    }
    public void PlayTimerEndingBgClip()
    {
        if (timerTickingBgClip == null) return;
        AudioSource.PlayClipAtPoint(timerTickingBgClip, Vector3.zero, .25f);
    }

    // public void StopTimerEndingBgClip()
    // {
    //       if (timerTickingBgClip == null) return;
    //     ClearAudioSource(timerTickingBGAudioSource);
    // }
    public void PlayClapSound()
    {
        if (clapClip == null) return;
        PlayAudio(timerTickingAudioSource,clapClip,.1f);
    }
     public void StopClapSound()
    {
        if (clapClip == null) return;
        ClearAudioSource(timerTickingAudioSource);
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