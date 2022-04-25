
[System.Serializable]
public class SaveData
{
    public int Gold;
    public int CurrentCar;
    public bool MusicMuted;
    public int[] CarsOwned;
    
    public SaveData(int gold, int currentCar, bool musicMuted, int[] carsOwned) {
      Gold = gold;
      CurrentCar = currentCar;
      MusicMuted = musicMuted;
      CarsOwned = carsOwned;
    }
}
