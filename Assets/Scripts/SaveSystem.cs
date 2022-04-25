using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
  private static string _savePath = "/game.data";

  public static void SaveGame()
  {
    Debug.Log($"Saving data [gold: {GameManager.Instance.PlayerGold}, currentCar: {GameManager.Instance.CurrentCar}, muted: {AudioManager.Instance.Muted}, cars: {GameManager.Instance.CarsOwned}");
    SaveSystem.SaveGame(GameManager.Instance.PlayerGold, GameManager.Instance.CurrentCar, AudioManager.Instance.Muted, GameManager.Instance.CarsOwned);
  }

  public static void SaveGame(int gold, int currentCar, bool musicMuted, int[] carsOwned)
  {
    BinaryFormatter formatter = new BinaryFormatter();
    string path = Application.persistentDataPath + _savePath;
    FileStream stream = new FileStream(path, FileMode.Create);

    SaveData data = new SaveData(gold, currentCar, musicMuted, carsOwned);

    formatter.Serialize(stream, data);
    stream.Close();
  }

  public static SaveData LoadGame()
  {
    string path = Application.persistentDataPath + _savePath;
    if (File.Exists(path))
    {
      BinaryFormatter formatter = new BinaryFormatter();
      FileStream stream = new FileStream(path, FileMode.Open);

      SaveData data = formatter.Deserialize(stream) as SaveData;
      stream.Close();
      return data;
    }
    else
    {
      return null;
    }
  }
}
