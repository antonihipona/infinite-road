using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
  public delegate void AdFinish();
  public event AdFinish OnAdFinish;

  public delegate void AdStarted();
  public event AdStarted OnAdStarted;

  public static AdsManager Instance;

  void Awake()
  {
    if (Instance == null)
    {
      Instance = this;
      Advertisement.Initialize("4295393");
      Advertisement.AddListener(Instance);
    }
    else
    {
      Destroy(this);
    }
  }

  public void PlayRewardedAd()
  {
    if (Advertisement.IsReady("Rewarded_Android"))
    {
      if (OnAdStarted != null)
      {
        OnAdStarted();
      }
      Advertisement.Show("Rewarded_Android");
    }
    else
    {
      Debug.Log("Rewarded ad not ready!");
    }
  }

  public void OnUnityAdsReady(string placementId)
  {
    Debug.Log("Ads are ready!");
  }

  public void OnUnityAdsDidError(string message)
  {
    Debug.Log("Error: " + message);
  }

  public void OnUnityAdsDidStart(string placementId)
  {
    Debug.Log("Video started!");
  }

  public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
  {
    if (placementId == "Rewarded_Android" && showResult == ShowResult.Finished)
    {
      if (OnAdFinish != null)
      {
        OnAdFinish();
      }
    }
  }
}
