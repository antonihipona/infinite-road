using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleAudio : MonoBehaviour
{
  [SerializeField] private Sprite _audioUnmuted;
  [SerializeField] private Sprite _audioMuted;

  private Image _image;

  private void Start()
  {
    _image = GetComponent<Image>();
    ChangeSprite();
  }

  public void Toggle()
  {
    if (AudioManager.Instance.Muted)
    {
      AudioManager.Instance.Muted = false;
      AudioManager.Instance.Unpause();
    }
    else
    {
      AudioManager.Instance.Muted = true;
      AudioManager.Instance.Pause();
    }
    ChangeSprite();
    SaveSystem.SaveGame();
  }

  private void ChangeSprite()
  {
    if (AudioManager.Instance.Muted)
    {
      _image.sprite = _audioMuted;
    }
    else
    {
      _image.sprite = _audioUnmuted;
    }
  }
}
