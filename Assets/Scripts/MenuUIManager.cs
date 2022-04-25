using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuUIManager : MonoBehaviour
{
  public TMP_Text CoinText;
  public GameObject ResetGameDataButton;

  private void Start()
  {
    CoinText.text = GameManager.Instance.PlayerGold.ToString();
  }

  public void QuitGame()
  {
#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#endif
    Application.Quit();
  }

  public void LoadGameScene()
  {
    SceneManager.LoadSceneAsync("Game", LoadSceneMode.Single);
  }

  public void LoadSelectCarScene()
  {
    SceneManager.LoadSceneAsync("Select Car", LoadSceneMode.Single);
  }

  public void ResetGameData()
  {
    AudioManager.Instance.Muted = false;
    GameManager.Instance.PlayerGold = 100;
    GameManager.Instance.CurrentCar = 1;
    GameManager.Instance.CarsOwned = new int[] { 1 };
    SaveSystem.SaveGame();
  }

}
