using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
  public GameObject HealthFill;

  [SerializeField]
  private PlayerController _playerController;
  
  void LateUpdate()
  {
    if (_playerController != null) {
      float healthRatio = _playerController.CurrentHealth / _playerController.MaxHealth;
      if (healthRatio <= 0) healthRatio = 0;
      Vector3 scale = new Vector3(healthRatio, 1, 1);
      HealthFill.transform.localScale = scale;
    }
  }
}
