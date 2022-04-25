using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
  [Header("Settings")]
  [SerializeField] private float _acceleration;

  [Header("Player Health")]
  public float CurrentHealth;
  public float MaxHealth;

  [Header("Points")]
  public float GameCoins = 0;
  public float GameScore = 0;

  [Header("UI Text")]
  public TMP_Text Coins;
  public TMP_Text Score;

  [SerializeField] private float _speed = 5f;
  [SerializeField] private float _maxSpeed = 20f;
  [SerializeField] private float _rotSpeed;
  [SerializeField] private float _maxRotSpeed;

  [Header("References")]
  public CameraController cameraController;
  [SerializeField] private RoadsController _roadsController;
  [SerializeField] private GameUIManager _gameUIManager;
  [SerializeField] private Transform _mesh;

  private TouchController _touchController;
  private bool _isDead = false;

  private void Start()
  {
    _touchController = GetComponent<TouchController>();
    AdsManager.Instance.OnAdStarted += ResetPosition;
    AdsManager.Instance.OnAdFinish += ResetStats;
    AudioManager.Instance.Play();
    Instantiate(GameManager.Instance.GetCarPrefab(GameManager.Instance.CurrentCar), _mesh);
  }

  private void LateUpdate()
  {
    Coins.text = $"{GameCoins}";
    Score.text = $"{Mathf.RoundToInt(GameScore)}";
  }

  private void OnDisable()
  {
    AdsManager.Instance.OnAdStarted -= ResetPosition;
    AdsManager.Instance.OnAdFinish -= ResetStats;
  }

  void Update()
  {
    if (!_isDead)
    {
      Move();
    }
    CheckDead();
  }

  private void Move()
  {
    var angle = Vector3.Angle(transform.forward, Vector3.right) - 90; // 90 to -90

    var lateralSpeedMultiplier = -angle / 90; // from 1 to -1 
    var lateralVel = Vector3.right * lateralSpeedMultiplier * _speed * Time.deltaTime;
    transform.position += lateralVel;

    var forwardSpeedMultiplier = 1 - Mathf.Abs(lateralSpeedMultiplier);
    var forwardVel = Vector3.back * forwardSpeedMultiplier * _speed * Time.deltaTime;
    _roadsController.Move(forwardVel / 8); //  divide by 8 because of the road scale


    if (_touchController.MovementType == MovementType.Left)
    {
      var nextRot = Quaternion.Slerp(transform.rotation, transform.rotation * Quaternion.Euler(0, -_rotSpeed, 0), Time.deltaTime);
      if ((nextRot.eulerAngles.y - 180) > 0)
      {
        if ((nextRot.eulerAngles.y - 180) > 135)
        {
          transform.rotation = nextRot;
        }
      }
      else
      {
        transform.rotation = nextRot;
      }
    }

    if (_touchController.MovementType == MovementType.Right)
    {
      var nextRot = Quaternion.Slerp(transform.rotation, transform.rotation * Quaternion.Euler(0, _rotSpeed, 0), Time.deltaTime);
      if ((nextRot.eulerAngles.y - 180) < 0)
      {
        if ((nextRot.eulerAngles.y - 180) < -135)
        {
          transform.rotation = nextRot;
        }
      }
      else
      {
        transform.rotation = nextRot;
      }
    }

    UpdateSpeed();
  }

  private void UpdateSpeed()
  {
    _speed += Time.deltaTime / _acceleration;
    if (_speed >= _maxSpeed) _speed = _maxSpeed;

    _rotSpeed += Time.deltaTime / _acceleration;
    if (_rotSpeed >= _maxRotSpeed) _rotSpeed = _maxRotSpeed;

    GameScore += Time.deltaTime * _speed;
  }

  private void CheckDead()
  {
    if (CurrentHealth <= 0)
    {
      Die();
      OpenEndScreen();
    }
  }

  private void Die()
  {
    if (_isDead) return;
    _isDead = true;
    GameManager.Instance.PlayerGold += Mathf.RoundToInt(GameCoins);
    SaveSystem.SaveGame();
  }

  private void OpenEndScreen()
  {
    _gameUIManager.OpenEndScreen();
  }

  public void ResetPosition()
  {
    transform.position = Vector3.zero;
    transform.rotation = Quaternion.identity;
  }

  public void ResetStats()
  {
    CurrentHealth = MaxHealth;
    _isDead = false;
  }

  public void TakeDamage(float damage, float intensity)
  {
    if (_isDead) return;
    CurrentHealth -= damage;
    cameraController.Shake(0.25f, intensity);
  }
}
