using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject RecordScreen;
    public TextMeshProUGUI RecordText;

    public void OnRecordClicked()
    {
        float recordTime = PlayerPrefs.GetFloat("Record");
        RecordText.text = "Seu recorde é : " + Utils.GetFormattedTime(recordTime);
        RecordScreen.SetActive(true);
    }

    public void OnRecordCloseClicked()
    {
        RecordScreen.SetActive(false);
    }

    public void OnStartClicked()
    {
        AnalyticsController.I.SendGameStart();
    }
}
