using UnityEngine;

public class DamageVolume : MonoBehaviour
{
  public PlayerController playerController;
  
  public float damagePerSecond = 1;
  private bool _takeDamage = false;

  private void Update()
  {
    if (_takeDamage)
    {
      playerController.TakeDamage(damagePerSecond * Time.deltaTime, 1);
    }
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.layer == 7)
    {
      PlayerController pc = other.GetComponentInParent<PlayerController>();
      if (pc != null)
      {
        _takeDamage = true;
      }
    }
  }

  private void OnTriggerExit(Collider other)
  {
    if (other.gameObject.layer == 7)
    {
      PlayerController pc = other.GetComponentInParent<PlayerController>();
      if (pc != null)
      {
        _takeDamage = false;
      }
    }
  }

}
