using UnityEngine;

public class GameManager : MonoBehaviour
{
  public int PlayerGold;
  public int CurrentCar;
  public int[] CarsOwned;

  public static GameManager Instance;

  [SerializeField] public GameObject[] GameCars;

  private void Awake()
  {
    if (Instance == null)
    {
      Instance = this;
    }
    else
    {
      Destroy(this);
    }
    DontDestroyOnLoad(gameObject);
  }

  private void Start()
  {
    TryLoadGame();
  }

  public void TryLoadGame()
  {
    SaveData data = SaveSystem.LoadGame();
    if (data == null)
    {
      AudioManager.Instance.Muted = false;
      GameManager.Instance.PlayerGold = 100;
      GameManager.Instance.CurrentCar = 1;
      GameManager.Instance.CarsOwned = new int[] { 1 };
    }
    else
    {
      AudioManager.Instance.Muted = data.MusicMuted;
      GameManager.Instance.PlayerGold = data.Gold;
      GameManager.Instance.CurrentCar = data.CurrentCar == 0 ? 1 : data.CurrentCar;
      if (data.CarsOwned.Length == 0)
      {
        GameManager.Instance.CarsOwned = new int[] { 1 };
      }
      else
      {
        GameManager.Instance.CarsOwned = data.CarsOwned;
      }
    }
  }

  public GameObject GetCarPrefab(int id)
  {
    for (int i = 0; i < GameCars.Length; i++)
    {
      GameObject carGO = GameCars[i];
      Car car = carGO.GetComponent<Car>();
      if (car.ID == id) return carGO;
    }
    return null;
  }
}
