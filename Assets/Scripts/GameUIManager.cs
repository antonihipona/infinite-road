using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{

  [SerializeField] private GameObject _endScreen;

  [SerializeField] Button _continueButton;

  private void Start()
  {
    AdsManager.Instance.OnAdFinish += CloseEndScreen;
    _continueButton.onClick.AddListener(OpenAd);
  }

  private void OnDisable()
  {
    AdsManager.Instance.OnAdFinish -= CloseEndScreen;
    _continueButton.onClick.RemoveListener(OpenAd);
  }

  public void GoToMainMenu()
  {
    AudioManager.Instance.Stop();
    AudioManager.Instance.PlayUIClick();
    SceneManager.LoadSceneAsync("Main Menu", LoadSceneMode.Single);
  }

  public void OpenEndScreen()
  {
    _endScreen.SetActive(true);
  }

  public void CloseEndScreen()
  {
    AudioManager.Instance.Unpause();
    _endScreen.SetActive(false);
    _continueButton.gameObject.SetActive(false);
  }

  public void OpenAd()
  {
    AudioManager.Instance.Pause();
    AdsManager.Instance.PlayRewardedAd();
  }
}
