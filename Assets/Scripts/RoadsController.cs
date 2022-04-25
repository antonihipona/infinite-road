using UnityEngine;

public class RoadsController : MonoBehaviour
{
  public GameObject LastRoad;

  public PlayerController PlayerController;


  [SerializeField]
  private Obstacle[] _obstaclesPool;
  
  public GameObject CoinPrefab;

  [SerializeField]
  private GameObject _roadPrefab;


  public void Move(Vector3 vel)
  {
    for (int i = 0; i < transform.childCount; i++)
    {
      var child = transform.GetChild(i);
      if (child != null) transform.position += vel;
    }
  }

  public GameObject GetRandomObstacle() {
    int rand = Random.Range(0, _obstaclesPool.Length);
    return _obstaclesPool[rand].gameObject;
  }
}
