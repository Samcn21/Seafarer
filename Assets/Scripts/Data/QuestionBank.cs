using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class QuestionBank : MonoBehaviour 
{
    private List<List<string>> questionBank = new List<List<string>>();
    [SerializeField]
    private int _countOfQuestions;

    [SerializeField]
    private int[] _questionsInBank;

    [SerializeField]
    private int _randomQuestionNumber;

    [SerializeField]
    private List<int> _result;

	void Start () 
    {
        questionBank.Add(new List<string> 
        {
            "1",
            "Hvorfor var Italien i senmiddelalderen Europas økonomiske og kulturelle kraftcenter? ",
            "Italien var fremme i kunst og litteratur i forhold til de andre europæi ske lande. ",
            "Industrialiseringen ",
            "Der var krydderier og luksusvarer og især bomuld som blev importeret fra Asien. Der var en stor tekstilindustri i Italien. ",
            "C"
        });

        questionBank.Add(new List<string> 
        {
            "2",
            "Hvad blev der produceret i Norditalien? ",
            "Tekstil. Man lærte at fremstille bomuldsstoffer af araberne. ",
            "Papir",
            "Dyre vine ",
            "A"
        });

        questionBank.Add(new List<string> 
        {
            "3",
            "Hvad importerede norditalienerne fra udlandet? ",
            "Korn ",
            "Svinekød ",
            "Bomuld fra Asien",
            "C"
        });

        questionBank.Add(new List<string> 
        {
            "4",
            "Hvilken rolle spillede krydderier for de rige europæere? Hvorfor? ",
            "Krydderierne blev brugt til at helbrede sygdomme. ",
            "Man havde ikke køleskabe/frysere dengang og ofte blev kødet dårligt. Og for at fjerne lugten og give kødet smag var krydderier meget eftertragtede. Desuden var det prestigefyldt, da det var kosbart.  ",
            "Der var så meget af krydderier, at det kostede næsten ingenting. ",
            "B"
        });

        questionBank.Add(new List<string> 
        {
            "5",
            "Hvorfor var det portugisere og senere spaniere og ikke italienere, som foretog de første opdagelsesrejser? ",
            "Sømændene havde i årevis fanget fisk fra Atlanterhavet og havde udviklet deres skibe og fået erfaring på de store have. ",
            "De var modigere mennesker ",
            "De var mere eventyrlystne",
            "A"
        });

        questionBank.Add(new List<string> 
        {
            "6",
            "Hvilken viden fik portugiserne fra araberne, som de udviklede til en tabel, der viste, hvor deres skibe befandt sig på havene? ",
            "De fik kompasset som viste skibenes kurs ",
            "De lærte matematik og om solens og stjernernes position på himlen. ",
            "De fik kuglerammer som de brugte til at regne skibenes position ",
            "B"
        });

        questionBank.Add(new List<string> 
        {
            "7",
            "Hvilket verdensbillede havde de fleste europæere i senmiddelalderen? ",
            "Alle troede på at verden var rund. ",
            "Man troede at verden var flad. ",
            "De flest troede den var flad, men der var også en del, som mente, at den var rund. ",
            "B"
        });

        questionBank.Add(new List<string> 
        {
            "8",
            "Hvad troede man, der ville ske, hvis søfolk sejlede for langt i én retning? ",
            "at man ville falde i afgrunden. ",
            "At man kom i helvede",
            "At man kom i Skærsilden, hvor man først skulle renses for sine synder, inden man kom videre i paradis. ",
            "A"
        });

        questionBank.Add(new List<string> 
        {
            "9",
            "Man havde bl.a. overtroiske forestillinger om, hvad der var i havenes afgrunde? Hvilke? ",
            "Det var helvede, hvor man blev pint. ",
            "Fantastiske væsener som enhjørning og fabeldyr. ",
            "Der var fyldt med giftige slanger",
            "B"
        });

        questionBank.Add(new List<string> 
        {
            "10",
            "Hvad for nogle rigdomme var man ude efter? Nævn dem. ",
            "Slaver ",
            "Man var efter de indfødtes våben.",
            "Guld, sølv og krydderier som peber, ingefær, kanel , nellike m.m.",
            "C"
        });

        //finds the number of questions in the question bank
        _countOfQuestions = questionBank.Count;
        _questionsInBank = new int[_countOfQuestions];

        for (int i = 0; i < _countOfQuestions; i++)
        {
            _questionsInBank[i] = i + 1;
        }
	}

    public int GetCountOfQuestion() 
    {
        return _countOfQuestions;
    }

    public List<string> GetOneQuestion(GameData.TeamCountry country, out int _questionNumber) 
    {
        //finding the player (the country) that requested for a question betwwen all players
        foreach (GameObject player in GameManager.Instance.allPlayers)
        {
            //this is the player who requested a question
            if (player.GetComponent<PlayerController>().GetMyTeam() == country)
            { 
                //TODO:
                //1. check if the country is alone or not
                //if it is alone then check the list of questions that they've been asked for 
                //else check the list of questions that they and their alliance have been asked for
                //2. sum the country and their alliance questions
                //3. substract the summary of asked questions from question bank;
                //4. the result will be questions that never been asked from the country and its alliance if they have
                //5. find a random question from the refined list 
                //6. if there is no new question, give one of the olds randomly

                if (player.GetComponent<PlayerController>().IsAlone())
                {
                    List<int> total = player.GetComponent<PlayerController>().GetTotalQuestions(country);
                    _result = _questionsInBank.Where(x => !total.Contains(x)).ToList();

                    foreach (int i in total)
                    {
                        //Debug.Log("total = " + i);
                    }

                    foreach (int i in _result)
                    {
                        //Debug.Log("result = " + i);
                    }

                    if (_result.Count != 0)
                    {
                        
                        //pick a random number from the questions that the player never answered.
                        _randomQuestionNumber = Random.Range(1, _result.Count + 1);

                        //Debug.Log("result = " + _result.Count + "randomNum = " + _randomQuestionNumber);
                    }
                    else
                    {
                       
                        //means we ran out of questions and pick a random number from the qwhole uestions.
                        //TODO: make an alarm to report afterwards. plus we need to prevent total questions of players added by same number cause the question is not new anymore
                        _randomQuestionNumber = Random.Range(1, _questionsInBank.Length + 1);

                        //Debug.Log(_questionsInBank.Length + " Length is  0 " + _randomQuestionNumber);

                    }


                }
                else
                {
                    //TODO: check the list of questions that player and their alliance have been asked for
                }
            }
        }

            foreach (List<string> question in questionBank)
            {
                if (question[0] == _result[_randomQuestionNumber - 1].ToString())
                {
                    Debug.Log("question number: " + question[0]);
                    _questionNumber = _result[_randomQuestionNumber - 1];
                    return question;
                    
                }
            }

        Debug.Log("question returnd null, why?!");

        _questionNumber = _randomQuestionNumber;
        return null;
    }

    public void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            int rndQuestion;
            foreach (string item in GetOneQuestion(GameData.TeamCountry.Denmark, out rndQuestion))
            {
                Debug.Log(item + " - " + rndQuestion);
            }
        }
    }
}
