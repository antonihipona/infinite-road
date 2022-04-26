using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AddCoins : MonoBehaviour
{
  [SerializeField]
  private TextMeshProUGUI text;

  public void AddHundredCoins()
  {
    GameManager.Instance.PlayerGold += 100;
    SaveSystem.SaveGame();
    text.text = GameManager.Instance.PlayerGold.ToString();
  }
}
