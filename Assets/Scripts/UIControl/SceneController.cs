using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
   public void SwitchScene(string targetScene)
    {
        Debug.Log(SceneManager.GetActiveScene().name + " switched to " +targetScene);
        SceneManager.LoadScene(targetScene);
    }

    public void SwitchSceneWithAd(string targetScene)
    {
        AdsController.I.ShowNonRewardedAd();
        Debug.Log(SceneManager.GetActiveScene().name + " switched to " + targetScene + " with ads");
        SceneManager.LoadScene(targetScene);
    }
}
