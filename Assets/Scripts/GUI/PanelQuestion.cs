using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
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
    private Text infoMsg;

    [SerializeField]
    private Button OK;

    [SerializeField]
    private string _currectAnswer;

    [SerializeField]
    private string _invitedAnswer = string.Empty;

    [SerializeField]
    private string _inviterAnswer = string.Empty;

    [SerializeField]
    private int _questionNumber;

    [SerializeField]
    private bool _isAllianceQuestion = false;

    [SerializeField]
    private GameData.City _refCity;

    [SerializeField]
    private GameData.TeamCountry _inviter;

    [SerializeField]
    private GameData.TeamCountry _invited;

    [SerializeField]
    private GameObject inviterGO;

    [SerializeField]
    private GameObject invitedGO;

    [SerializeField]
    private GameObject refCityGO;

    [SerializeField]
    private List<string> theQuestion;

    private string msg = string.Empty;
    private bool _areBothAnswersCorrect = false;
    void Awake()
    {
        if (!panelQuestion)
            Debug.LogError("panelQuestion is not found!");
    }

    void OnGUI()
    {
        infoMsg.text = msg;

        //both parties answered the question and OK button will be activated
        if (!string.IsNullOrEmpty(_invitedAnswer) && (!string.IsNullOrEmpty(_inviterAnswer)))
        {
            if (_invitedAnswer != _inviterAnswer)
                msg = "your answer and your ally's answer don't match! The attack has failed. Please come back later!";
            ActivateOK(true);
        }
    }

    public override void ShowPanel()
    {
        base.ShowPanel();
        _isAllianceQuestion = false;
        msg = string.Empty;
        _areBothAnswersCorrect = false;
        ActivateOK(false);
    }

    private void ActivateOK(bool isActive)
    {
        if (isActive)
        {
            OK.GetComponentInChildren<Text>().text = "OK";
            OK.interactable = true;
            OK.GetComponent<Image>().enabled = true;

        }
        else
        {
            OK.GetComponentInChildren<Text>().text = string.Empty;
            OK.interactable = false;
            OK.GetComponent<Image>().enabled = false;
        }
    }
    public void OpenPanel(GameData.TeamCountry inviter, GameData.TeamCountry invited, GameData.City refCity)
    {
        ShowPanel();

        if (invited != GameData.TeamCountry.___)
            _isAllianceQuestion = true;

        _refCity = refCity;
        _inviter = inviter;
        _invited = invited;

        //only inviter comes here and ask the question!
        theQuestion = GameManager.Instance.QuestionBank.GetOneQuestion(inviter, invited, refCity, out _questionNumber);

        //return a question
        UnpackTheQuestion(theQuestion, inviter, GameManager.Instance.QuestionBank.GetRandomQuestionNumber());
    }

    public void OpenPanelInvited(GameData.TeamCountry inviter, GameData.TeamCountry invited, GameData.City refCity)
    {
        ShowPanel();
        _isAllianceQuestion = true;
        _refCity = refCity;
        _inviter = inviter;
        _invited = invited;
        //inviter already find the common question and sent to the network, Invited needs to get the question only
        theQuestion = GameManager.Instance.QuestionBank.GetInvitedQuestion(GameManager.Instance.QuestionBank.GetRandomQuestionNumber());

        UnpackTheQuestion(theQuestion, invited, GameManager.Instance.QuestionBank.GetRandomQuestionNumber());
        //city.GetComponent<PhotonView>().RPC("SetCityStatus", PhotonTargets.All, GameData.DefenceStatus.UnderAttack);
    }


    private void UnpackTheQuestion(List<string> theQuestion, GameData.TeamCountry country, int questionNumber)
    {
        for (int i = 0; i < theQuestion.Count; i++)
        {
            switch (i)
            {
                //first element of the list contains the question number
                case 0:
                    foreach (GameObject player in GameManager.Instance.GetAllPlayers())
                    {
                        //add the question to the total questions of player 
                        player.GetComponent<PhotonView>().RPC("AddToTotalQuestions", PhotonTargets.All, questionNumber, country);
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
    }

    [PunRPC]
    public void InviterInvitedAnswer(string answer, GameData.TeamCountry country)
    {
        if (country == _inviter)
            _inviterAnswer = answer;

        if (country == _invited)
            _invitedAnswer = answer;
    }

    public void AnswerQuestion(string answer)
    {
        if (GameManager.Instance.GetMyPlayerTeam() == _inviter)
            GetComponent<PhotonView>().RPC("InviterInvitedAnswer", PhotonTargets.All, answer, _inviter);

        if (GameManager.Instance.GetMyPlayerTeam() == _invited)
            GetComponent<PhotonView>().RPC("InviterInvitedAnswer", PhotonTargets.All, answer, _invited);

        if (!_isAllianceQuestion)
        {
            ChangeStatusSingleAttack(answer);
        }
        else
        {
            if (answer.ToLower() != _currectAnswer.ToLower())
            {
                ActivateAnswerButtons(false);
                ChangeStatusFailure();
                msg = "your answer is wrong! The attack has failed. Please come back later!";
                ActivateOK(true);
            }
            else //means my answer is correct but need to check if both allies have same answer
            {
                //if inviter still didn't answer
                if (GameManager.Instance.GetMyPlayerTeam() == _invited && string.IsNullOrEmpty(_inviterAnswer))
                {
                    ActivateAnswerButtons(false);
                    msg = _inviter + " hasn't answer to the question yet!";
                }
                else if (GameManager.Instance.GetMyPlayerTeam() == _inviter && string.IsNullOrEmpty(_invitedAnswer))
                {
                    ActivateAnswerButtons(false);
                    msg = _invited + " hasn't answer to the question yet!";
                }
                else if (_inviterAnswer.ToLower() != _invitedAnswer.ToLower()) //both answered and one is correct the other is incorrect
                {
                    ActivateAnswerButtons(false);
                    ChangeStatusFailure();
                    msg = "your ally answered incorrectly! The attack has failed. Please come back later!";
                    ActivateOK(true);
                }
                else if (_inviterAnswer.ToLower() == _invitedAnswer.ToLower()) //both answered and both are correct
                {
                    ActivateAnswerButtons(false);
                    ChangeStatusSuccess(answer);
                    _areBothAnswersCorrect = true;
                    msg = "you and your ally answered correctly. " + _refCity.ToString() + " is yours now!";
                    ActivateOK(true);
                }

            }
        }
    }

    public void Ok()
    {
        HidePanel();
        msg = string.Empty;
        ActivateOK(false);
        ActivateAnswerButtons(true);
        theQuestion.Clear();
        _questionNumber = 0;
        _currectAnswer = "";
        _isAllianceQuestion = false;
        InviterInvitedAnswer(string.Empty, _inviter);
        InviterInvitedAnswer(string.Empty, _invited);
        _inviter = GameData.TeamCountry.___;
        _invited = GameData.TeamCountry.___;
    }

    private void ActivateAnswerButtons(bool isActive)
    {
        if (isActive)
        {
            btnChoiceA.interactable = true;
            btnChoiceB.interactable = true;
            btnChoiceC.interactable = true;
        }
        else
        {
            btnChoiceA.interactable = false;
            btnChoiceB.interactable = false;
            btnChoiceC.interactable = false;
        }
    }

    private void ChangeStatusSuccess(string answer)
    {
        GetPlayersCity();

        //only defence status (free/under attack/ under siege) will change due to wrong answer
        refCityGO.GetComponent<PhotonView>().RPC("ChangeCityStatusAllies", PhotonTargets.All, _inviter, _invited, GameData.CityStatus.OccupiedByAllies, GameData.DefenceStatus.Free);

        //nothing from player would change due to wrong answer
        inviterGO.GetComponent<PhotonView>().RPC("ChangePlayerStatusAllies", PhotonTargets.All, _refCity, GameManager.Instance.QuestionBank.GetRandomQuestionNumber());
        invitedGO.GetComponent<PhotonView>().RPC("ChangePlayerStatusAllies", PhotonTargets.All, _refCity, GameManager.Instance.QuestionBank.GetRandomQuestionNumber());

        //change play mode (explore/attack) and isAlone
        inviterGO.GetComponent<PhotonView>().RPC("ChangePlayerStatusInvited", PhotonTargets.All, _invited, _inviter, true);
        invitedGO.GetComponent<PhotonView>().RPC("ChangePlayerStatusInvited", PhotonTargets.All, _invited, _inviter, true);

    }

    private void ChangeStatusFailure()
    {
        GetPlayersCity();

        //only defence status (free/under attack/ under siege) will change due to wrong answer
        refCityGO.GetComponent<PhotonView>().RPC("ChangeCityStatus", PhotonTargets.All, _inviter, GameData.CityStatus.OccupiedByOneCountry, GameData.DefenceStatus.Free, false);

        //change play mode (explore/attack) and isAlone
        inviterGO.GetComponent<PhotonView>().RPC("ChangePlayerStatusInvited", PhotonTargets.All, _invited, _inviter, true);
        invitedGO.GetComponent<PhotonView>().RPC("ChangePlayerStatusInvited", PhotonTargets.All, _invited, _inviter, true);
    }


    private void ChangeStatusSingleAttack(string answer)
    {

        GetPlayersCity();
        //GetComponent<PhotonView>().RPC("InviterInvitedAnswer", PhotonTargets.All, string.Empty, _inviter);

        //correct answer
        if (answer.ToLower() == _currectAnswer.ToLower())
        {
            //update city and player(s) based on correct answer on the network
            refCityGO.GetComponent<PhotonView>().RPC("ChangeCityStatus", PhotonTargets.All, _inviter, GameData.CityStatus.OccupiedByOneCountry, GameData.DefenceStatus.Free, true);
            inviterGO.GetComponent<PhotonView>().RPC("ChangePlayerStatus", PhotonTargets.All, _refCity, _questionNumber, true);
        }
        //incorrect answer
        else
        {
            //update city and player(s) based on incorrect answer on the network
            refCityGO.GetComponent<PhotonView>().RPC("ChangeCityStatus", PhotonTargets.All, _inviter, GameData.CityStatus.OccupiedByOneCountry, GameData.DefenceStatus.Free, false);
            inviterGO.GetComponent<PhotonView>().RPC("ChangePlayerStatus", PhotonTargets.All, _refCity, _questionNumber, false);
        }

        //change play mode to exploring again no matter answer was correct or incorrect
        inviterGO.GetComponent<PhotonView>().RPC("ChangePlayMode", PhotonTargets.All, GameData.TeamPlayMode.Exploring);
        HidePanel();
    }

    private void GetPlayersCity()
    {
        foreach (GameObject city in GameManager.Instance.allCities)
        {
            if (city.GetComponent<CityController>().GetCityName() == _refCity)
            {
                refCityGO = city;
            }
        }

        foreach (GameObject player in GameManager.Instance.GetAllPlayers())
        {
            if (player.GetComponent<PlayerController>().GetMyTeam() == _inviter)
            {
                inviterGO = player;
            }
        }

        foreach (GameObject player in GameManager.Instance.GetAllPlayers())
        {
            if (player.GetComponent<PlayerController>().GetMyTeam() == _invited)
            {
                invitedGO = player;
            }
        }
    }
}
