using UnityEngine;

public class CameraController : MonoBehaviour
{
  private Vector3 _originalPos;
  private Vector3 _nextPos;
  private float _shakeTime = 0;
  private float _intensity;

  private void Start()
  {
    _originalPos = transform.position;
  }

  private void OnDisable() {
  }

  void LateUpdate()
  {
    if (_shakeTime > 0)
    {
      if (!_nextPos.Equals(_originalPos))
      {
        _nextPos = _originalPos;
      }
      else
      {
        float rand1 = Random.Range(0, 0.1f) * _intensity;
        float rand2 = Random.Range(0, 0.1f) * _intensity;

        _nextPos = new Vector3(_originalPos.x + rand1, _originalPos.y, _originalPos.z + rand2);
        transform.position = _nextPos;
      }
      _shakeTime -= Time.deltaTime;
    }
    else if (!transform.position.Equals(_originalPos))
    {
      transform.position = _originalPos;
    }
  }

  public void Shake(float time, float intensity)
  {
    _shakeTime = time > 0 ? time : 0;
    _intensity = intensity >= 1 ? intensity : 1;
  }

}
