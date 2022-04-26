using UnityEngine;

public class AudioManager : MonoBehaviour
{
  private AudioSource _mainAudioSource;

  [SerializeField] AudioSource _crashAudioSource;
  [SerializeField] AudioSource _coinAudioSource;
  [SerializeField] AudioSource _repairAudioSource;
  [SerializeField] AudioSource _uiClickAudioSource;

  public bool Muted;

  public static AudioManager Instance;


  void Awake()
  {
    if (Instance == null)
    {
      _mainAudioSource = GetComponent<AudioSource>();
      Instance = this;
    }
    else
    {
      Destroy(this);
    }
  }

  public void Play()
  {
    if (Muted) return;
    _mainAudioSource.Play();
  }

  public void Stop()
  {
    _mainAudioSource.Stop();
  }

  public void Unpause()
  {
    if (Muted) return;
    if (_mainAudioSource.isPlaying)
      _mainAudioSource.UnPause();
    else
      Play();
  }

  public void Pause()
  {
    _mainAudioSource.Pause();
  }

  public void PlayCrash() {
    if (Muted) return;
    _crashAudioSource.Play();
  }

  public void PlayCoin() {
    if (Muted) return;
    _coinAudioSource.Play();
  }

  public void PlayRepair() {
    if (Muted) return;
    _repairAudioSource.Play();
  }

  public void PlayUIClick() {
    if (Muted) return;
    _uiClickAudioSource.Play();
  }
}
