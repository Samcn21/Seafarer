using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PanelQuestion : PanelParent
{
    [SerializeField]
    private GameObject panelQuestion;

    [SerializeField]
    private Text question;

    [SerializeField]
    private Text choiceA;

    [SerializeField]
    private Text choiceB;

    [SerializeField]
    private Text choiceC;

    [SerializeField]
    private Button btnChoiceA;

    [SerializeField]
    private Button btnChoiceB;

    [SerializeField]
    private Button btnChoiceC;

    [SerializeField]
    private string _currectAnswer;

    [SerializeField]
    private int _questionNumber;

    [SerializeField]
    private GameData.City _refCity;

    [SerializeField]
    private List<GameData.TeamCountry> _askerAllies;

    [SerializeField]
    private GameData.TeamCountry _askerCountry;

    [SerializeField]
    private GameObject askerCountryGO;

    [SerializeField]
    private GameObject refCityGO;

    void Awake()
    {
        if (!panelQuestion)
            Debug.LogError("panelQuestion is not found!");
    }

    public void OpenPanel(GameData.TeamCountry askerCountry, GameData.City refCity) 
    {
        ShowPanel();
        _refCity = refCity;
        _askerCountry = askerCountry;

        //TODO: when country has an allience should send a refrence List<asker(s)country>!! 
        _askerAllies.Add(askerCountry);
        //_askerAllies.AddRange(askerCountry + allies);

        List<string> theQuestion = GameManager.Instance.QuestionBank.GetOneQuestion(askerCountry, out _questionNumber);

        //2. return a question 
        for (int i = 0; i < theQuestion.Count; i++)
        {
           switch (i)
           {
                   //first element of the list contains the question number
               case 0:
                   foreach (GameObject player in GameManager.Instance.GetAllPlayers())
                   {
                       //3. add the question to the total player and plus to their allience too
                       //TODO: for allience too!
                       if (player.GetComponent<PlayerController>().GetMyTeam() == askerCountry)
                       {
                           player.GetComponent<PlayerController>().AddToTotalQuestions(_questionNumber);
                       }
                   }
                   break;
               case 1:
                   question.text = theQuestion[i];
                   break;
               case 2:
                   choiceA.text = theQuestion[i];
                   break;
               case 3:
                   choiceB.text = theQuestion[i];
                   break;
               case 4:
                   choiceC.text = theQuestion[i];
                   break;
               case 5:
                   _currectAnswer = theQuestion[i];
                   Debug.Log(_currectAnswer);
                   break;
               default:
                   Debug.Log("this question has more than 6 elements!!!!");
                   break;
           }
        }


        
        //asks all players in the network to update their players active in the scene 
        //and find my player or alliances (later) to add the total question


        //city.GetComponent<PhotonView>().RPC("SetCityStatus", PhotonTargets.All, GameData.DefenceStatus.UnderAttack);
        
    }

    public void AnswerQuestion(string answer)
    {
        GUIManager.Instance.SetWaitForSeconds(0.2f);
        foreach (GameObject city in GameManager.Instance.allCities)
        {
            if (city.GetComponent<CityController>().GetCityName() == _refCity)
            {
                refCityGO = city;
            }
        }

        foreach (GameObject player in GameManager.Instance.GetAllPlayers())
        {
            if (player.GetComponent<PlayerController>().GetMyTeam() == _askerCountry)
            {
                askerCountryGO = player;
            }
        }

        //correct answer
        if (answer.ToLower() == _currectAnswer.ToLower())
        {
            //update city and player(s) based on correct answer on the network
            refCityGO.GetComponent<PhotonView>().RPC("ChangeCityStatus", PhotonTargets.All, _askerCountry, GameData.CityStatus.OccupiedByOneCountry, GameData.DefenceStatus.Free, true);
            askerCountryGO.GetComponent<PhotonView>().RPC("ChangePlayerStatus", PhotonTargets.All, _refCity, _questionNumber, true);
        }
        //incorrect answer
        else
        {
            //update city and player(s) based on incorrect answer on the network
            refCityGO.GetComponent<PhotonView>().RPC("ChangeCityStatus", PhotonTargets.All, _askerCountry, GameData.CityStatus.OccupiedByOneCountry, GameData.DefenceStatus.Free, false);
            askerCountryGO.GetComponent<PhotonView>().RPC("ChangePlayerStatus", PhotonTargets.All, _refCity, _questionNumber, false);
        }
        //change play mode to exploring again no matter answer was correct or incorrect
        askerCountryGO.GetComponent<PhotonView>().RPC("ChangePlayMode", PhotonTargets.All, GameData.TeamPlayMode.Exploring);
        HidePanel();
    }
}
