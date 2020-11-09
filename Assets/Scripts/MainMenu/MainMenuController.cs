using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject RecordScreen;
    public TextMeshProUGUI RecordText;

    public void OnRecordClicked()
    {
        //RecordText.text = PlayerPrefs.GetString("Record");
        RecordScreen.SetActive(true);
    }

    public void OnRecordCloseClicked()
    {
        Debug.Log("it entered");
        RecordScreen.SetActive(false);
    }

    public void OnStartClicked()
    {
        //If we change index of scenes, it still work
        SceneManager.LoadScene("GameScene");
    }
}
