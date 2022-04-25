using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SelectCarUIManager : MonoBehaviour
{
  public TMP_Text CoinText;
  public TMP_Text CarName;
  public TMP_Text OwnedText;
  public TMP_Text PriceText;

  public GameObject SelectButton;
  public GameObject BuyButton;

  public Car CurrentCar;
  public Transform CarSpawnPosition;

  void Start()
  {
    CoinText.text = GameManager.Instance.PlayerGold.ToString();
    SetCurrentCar(GameManager.Instance.CurrentCar);
  }

  private void Update()
  {
    if (CarSpawnPosition != null)
      CarSpawnPosition.rotation *= Quaternion.Euler(0, 0.5f, 0);
  }

  private void SetCurrentCar(int id)
  {
    bool isCarOwned = false;
    bool isCarSelected = GameManager.Instance.CurrentCar == id;
    foreach (int carOwned in GameManager.Instance.CarsOwned)
    {
      if (id == carOwned)
      {
        isCarOwned = true;
        break;
      }
    }

    foreach (GameObject carGO in GameManager.Instance.GameCars)
    {
      Car car = carGO.GetComponent<Car>();
      if (id == car.ID)
      {
        CurrentCar = car.GetComponent<Car>();
        break;
      }
    }

    CarName.text = CurrentCar.Name;
    if (isCarOwned)
    {
      OwnedText.text = "Owned";
      OwnedText.color = new Color(0.2f, 0.8f, 0.2f, 1);
      SelectButton.SetActive(true);
      BuyButton.SetActive(false);
      PriceText.text = "";
    }
    else
    {
      OwnedText.text = "Not Owned";
      OwnedText.color = new Color(0.8f, 0.2f, 0.2f, 1);
      SelectButton.SetActive(false);
      BuyButton.SetActive(true);
      PriceText.text = "Price: " + CurrentCar.Price.ToString();
    }

    if (isCarSelected)
    {
      OwnedText.text = "Selected";
      OwnedText.color = new Color(0.2f, 0.2f, 0.8f, 1);
      SelectButton.SetActive(false);
      BuyButton.SetActive(false);
    }

    foreach (Transform child in CarSpawnPosition)
    {
      GameObject.Destroy(child.gameObject);
    }

    Instantiate(CurrentCar.gameObject, new Vector3(0, 0.75f, 0), Quaternion.identity * Quaternion.Euler(0, 180, 0), CarSpawnPosition);
  }

  public void LoadMainMenuScene()
  {
    SceneManager.LoadSceneAsync("Main Menu", LoadSceneMode.Single);
  }

  public void SelectNextCar()
  {
    int nextID = CurrentCar.ID + 1;
    if (nextID > GameManager.Instance.GameCars.Length)
    {
      nextID = 1;
    }
    SetCurrentCar(nextID);
  }

  public void SelectPreviousCar()
  {
    int previousID = CurrentCar.ID - 1;
    if (previousID < 1)
    {
      previousID = GameManager.Instance.GameCars.Length;
    }
    SetCurrentCar(previousID);
  }

  public void BuyCar()
  {
    if (CurrentCar == null) return;
    bool isCarOwned = false;
    foreach (int carOwned in GameManager.Instance.CarsOwned)
    {
      if (CurrentCar.ID == carOwned)
      {
        isCarOwned = true;
        break;
      }
    }
    if (isCarOwned) return;
    if (CurrentCar.Price > GameManager.Instance.PlayerGold) return;
    GameManager.Instance.PlayerGold -= CurrentCar.Price;
    List<int> cars = new List<int>(GameManager.Instance.CarsOwned);
    cars.Add(CurrentCar.ID);

    GameManager.Instance.CarsOwned = cars.ToArray();

    SaveSystem.SaveGame();
    CoinText.text = GameManager.Instance.PlayerGold.ToString();
    SetCurrentCar(CurrentCar.ID);
  }

  public void SelectCar()
  {
    bool isCarOwned = false;
    foreach (int carOwned in GameManager.Instance.CarsOwned)
    {
      if (CurrentCar.ID == carOwned)
      {
        isCarOwned = true;
        break;
      }
    }
    if (!isCarOwned) return;
    GameManager.Instance.CurrentCar = CurrentCar.ID;
    SaveSystem.SaveGame();
    SetCurrentCar(CurrentCar.ID);
  }
}
