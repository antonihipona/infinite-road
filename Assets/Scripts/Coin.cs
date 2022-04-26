using UnityEngine;

public class Coin : MonoBehaviour
{

  void Update()
  {
    transform.rotation *= Quaternion.Euler(0, 1, 0);
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.layer == 7)
    {
      PlayerController pc = other.GetComponentInParent<PlayerController>();
      if (pc != null)
      {
        pc.GameCoins += 1;
        AudioManager.Instance.PlayCoin();
        Destroy(gameObject);
      }
    }
  }
}
