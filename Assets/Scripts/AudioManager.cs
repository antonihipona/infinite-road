using UnityEngine;

public class AudioManager : MonoBehaviour
{
  private AudioSource _audioSource;
  public bool Muted;

  public static AudioManager Instance;

  void Awake()
  {
    if (Instance == null)
    {
      _audioSource = GetComponent<AudioSource>();
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
    _audioSource.Play();
  }

  public void Stop()
  {
    _audioSource.Stop();
  }

  public void Unpause()
  {
    if (Muted) return;
    if (_audioSource.isPlaying)
      _audioSource.UnPause();
    else
      Play();
  }

  public void Pause()
  {
    _audioSource.Pause();
  }
}
