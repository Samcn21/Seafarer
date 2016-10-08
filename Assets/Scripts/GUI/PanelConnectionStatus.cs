using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanelConnectionStatus : PanelParent
{
    public GameObject panelConnectionStatus;
    public Text internetStatus;
    public Text GPSStatus;
    public Text networkConStatus;
    public Text networkStatus;
    public Text playerTeam;
    public Button testButton;
    public Text testText;


    void Awake()
    {
        if (!panelConnectionStatus)
            Debug.LogError("PanelConnectionStatus is not found!");

        if (!internetStatus)
            Debug.LogError("InternetStatus is not found!");

        if (!GPSStatus)
            Debug.LogError("GPSStatus is not found!");

        if (!networkStatus)
            Debug.LogError("NetworkStatus is not found!");

        if (!playerTeam)
            Debug.LogError("playerTeam is not found!");
    }

    public void ShowTest()
    {
        string text = string.Empty;
        //foreach (GameObject player in GameManager.Instance.GetAllPlayers())
        //{
        //    if (player.GetComponent<PlayerController>().GetMyTeam() == GameManager.Instance.GetMyPlayer())
        //    {
        //        foreach (GameData.TeamCountry ally in player.GetComponent<PlayerController>().Allies())
        //        {
        //            text += ally.ToString() + " ,";
        //        }
        //    }
        //}
        foreach (GameObject city in GameManager.Instance.allCities)
        {
            if (city.GetComponent<CityController>().GetCityDefenceStatus() == GameData.DefenceStatus.UnderSiege)
            {
                text += city.name + " ,";
            }
        }

        //GUIManager.Instance.PanelConnectionStatus.testText.text = GameManager.Instance.QuestionBank.GetRandomQuestionNumber().ToString();
        GUIManager.Instance.PanelConnectionStatus.testText.text = GameManager.Instance.TimeController.GetGamePlayDuration().ToString();
        RestartTheGame();
    }

    [PunRPC] //class: this
    public void RestartTheGame() 
    {
        StartCoroutine(Waiting(2));
    }

    IEnumerator Waiting(float wSec)
    {
        yield return new WaitForSeconds(wSec);
        PhotonNetwork.Disconnect();
        GameObject.Destroy(GameObject.FindGameObjectWithTag("GameController"));
        Application.LoadLevel(1);
    }

}