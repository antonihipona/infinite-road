using UnityEngine;

public class Wrench : MonoBehaviour
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
        pc.Heal(2f);
        AudioManager.Instance.PlayRepair();
        Destroy(gameObject);
      }
    }
  }
}
