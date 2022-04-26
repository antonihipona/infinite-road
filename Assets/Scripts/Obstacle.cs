using UnityEngine;

public class Obstacle : MonoBehaviour
{
  [SerializeField]
  private float _damage;

  private void OnTriggerEnter(Collider other) {
    if (other.gameObject.layer == 7) {
      PlayerController pc = other.GetComponentInParent<PlayerController>();
      if (pc != null) {
        pc.TakeDamage(_damage, _damage > 1 ? _damage : 1, true);
        Destroy(gameObject);
      }
    }
  }
}
