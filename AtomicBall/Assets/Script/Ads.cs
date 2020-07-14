using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class Ads : MonoBehaviour, IUnityAdsListener
{
    private string gameId = "3711427";
    string ID1 = "video";
    string ID2 = "rewardedVideo";
    bool testMode = false;

    void Start()
    {
        Advertisement.Initialize(gameId, testMode);
        Advertisement.AddListener(this);
    }

    public void showNormalAd()
    {
        SceneManager.LoadSceneAsync(0,LoadSceneMode.Single);
        if (Advertisement.IsReady(ID1)) Advertisement.Show(ID1);
        else SceneManager.LoadScene(0);
    }

    public void showRewardedAd()
    {
        SceneManager.LoadSceneAsync(1,LoadSceneMode.Single);
        if (Advertisement.IsReady(ID2)) Advertisement.Show(ID2);
        else SceneManager.LoadScene(1);
    }

    public void OnUnityAdsDidFinish(string placementID, ShowResult showResult)
    {
        /*if (placementID.Equals(ID2))
        {
            if (showResult == ShowResult.Finished)
            {
                SceneManager.LoadScene(1);
            }
            else if (showResult == ShowResult.Skipped)
            {
                SceneManager.LoadScene(1);
            }
            else if (showResult == ShowResult.Failed)
            {
                SceneManager.LoadScene(1);
            }
        }
        else if (placementID.Equals(ID1))
        {
            if (showResult == ShowResult.Finished)
            {
                SceneManager.LoadScene(0);
            }
            else if (showResult == ShowResult.Skipped)
            {
                SceneManager.LoadScene(0);
            }
            else if (showResult == ShowResult.Failed)
            {
                SceneManager.LoadScene(0);
            }
        }*/
    }

    public void OnUnityAdsReady(string ID)
    {
        /*if (ID == placementID)
        {
            Advertisement.Show(placementID);
        }*/
    }

    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }  
}