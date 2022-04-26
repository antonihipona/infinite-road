using UnityEngine;

public class Road : MonoBehaviour
{
  public GameObject NextRoad;

  [HideInInspector]
  public GameObject Obstacle;

  [HideInInspector]
  public GameObject Coin;


  private RoadsController _roadsController;
  public Vector3 ObstacleOffset;
  public Vector3 CoinOffset;

  private void Start()
  {
    _roadsController = GetComponentInParent<RoadsController>();
    AdsManager.Instance.OnAdFinish += ClearObstacle;
  }

  private void OnDisable()
  {
    AdsManager.Instance.OnAdFinish -= ClearObstacle;
  }

  private void Update()
  {
    if (Obstacle != null) Obstacle.transform.position = transform.position + ObstacleOffset;
    if (Coin != null) Coin.transform.position = transform.position + CoinOffset;
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.layer == 7)
    {
      GenerateObstacleAndCoin();
      GenerateNextRoad();
    }
  }

  private void GenerateNextRoad()
  {
    Vector3 newRoadPosition = transform.position;
    newRoadPosition.z += 40;
    _roadsController.LastRoad.transform.position = newRoadPosition;
    _roadsController.LastRoad = _roadsController.LastRoad.GetComponent<Road>().NextRoad;
  }

  private void GenerateObstacleAndCoin()
  {
    // Generate obstacle
    if (_roadsController.LastRoad.GetComponent<Road>().Obstacle != null)
    {
      Destroy(_roadsController.LastRoad.GetComponent<Road>().Obstacle);
    }
    _roadsController.LastRoad.GetComponent<Road>().ObstacleOffset = GetNewObstaclePosition();
    GameObject newObstacle = _roadsController.GetRandomObstacle();
    Quaternion randomRot = Quaternion.identity * Quaternion.Euler(0, Random.Range(0, 360), 0);
    _roadsController.LastRoad.GetComponent<Road>().Obstacle = Instantiate(newObstacle, _roadsController.LastRoad.transform.position + _roadsController.LastRoad.GetComponent<Road>().ObstacleOffset, randomRot);
  
    // Generate coin
    if (_roadsController.LastRoad.GetComponent<Road>().Coin != null)
    {
      Destroy(_roadsController.LastRoad.GetComponent<Road>().Coin);
    }

    float chance = _roadsController.PlayerController.GameScore / 500;
    bool spawn = Random.Range(0f, 1f) < chance;
    if (spawn) {
      // Repair chance
      bool repairChance = Random.Range(0f, 1f) < 0.1f;
        _roadsController.LastRoad.GetComponent<Road>().CoinOffset = GetNewCoinPosition(_roadsController.LastRoad.GetComponent<Road>().ObstacleOffset);
      if (repairChance) {
        _roadsController.LastRoad.GetComponent<Road>().Coin = Instantiate(_roadsController.WrenchPrefab, _roadsController.LastRoad.transform.position + _roadsController.LastRoad.GetComponent<Road>().CoinOffset, Quaternion.identity);
      } else {
        _roadsController.LastRoad.GetComponent<Road>().Coin = Instantiate(_roadsController.CoinPrefab, _roadsController.LastRoad.transform.position + _roadsController.LastRoad.GetComponent<Road>().CoinOffset, Quaternion.identity);
      }
    }
  }

  private Vector3 GetNewObstaclePosition()
  {
    bool left = Random.Range(0f, 1f) < 0.5;
    Vector3 pos = new Vector3(Random.Range(-2.5f, 2.5f), 0, Random.Range(-1f, 1));
    return pos;
  }

  private Vector3 GetNewCoinPosition(Vector3 obstaclePosition) {
    bool obstacleLeft = obstaclePosition.x < 0;
    float x = obstacleLeft ? Random.Range(1, 2.5f) : Random.Range(-2.5f, -1);
    Vector3 pos = new Vector3(x, 0, Random.Range(-1f, 1));
    return pos;
  }

  private void ClearObstacle() {
    if (Obstacle != null) {
      Destroy(Obstacle);
    }
  }
}
