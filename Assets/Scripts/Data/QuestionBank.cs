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

    [SerializeField]
    private List<int> _total;

    [SerializeField]
    private GameData.TeamCountry _invited;

    [SerializeField]
    private GameData.TeamCountry _inviter;

    void Start()
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

       questionBank.Add(new List<string> 
       {
           "11",
           "Opdagelsesrejsende ville finde søvejen til bl.a. hvilket land? ",
           "USA ",
           "Indien",
           "Tyrkiet",
           "B"
       });

       questionBank.Add(new List<string> 
       {
           "12",
           "Hvorfor ville man finde søvejen til Indien?  ",
           " For at hente krydderier  ",
           " For at hente slaver ",
           " For at lære at indernes kultur at kende. ",
           "A"
       });

       questionBank.Add(new List<string> 
       {
           "13",
           "    Hvilke produkter var især eftertragtede fra Indien? ",
           " Silke",
           " Indiske kvinders sari-dragt  ",
           " Peber, ingefær, kanel, nellike mm",
           "C"
       });

       questionBank.Add(new List<string> 
       {
           "14",
           "Hvilke to lande ledte den europæiske ekspansion in 1400-1600 tallet? ",
           "Frankrig og England",
           "Holland og Spanien ",
           "Spanien og Portugal ",
           "C"
       });

       questionBank.Add(new List<string> 
       {
           "15",
           "Hvad karakteriserede den portugisiske ekspansion? ",
           "Portugeserne var primært drevet af jagten på guld",
           "Portugeserne var primært drevet af jagten på krydderi  ",
           "Portugeserne var primært drevet af jagten på olie",
           "B"
       });

       questionBank.Add(new List<string> 
       {
           "16",
           "Hvad karakteriserede den spanske ekspansion? ",
           "Spanierne var primært drevet af jagten på olie",
           "Spanierne var primært drevet af jagten på krydderi",
           "Spanierne var primært drevet af jagten på guld ",
           "C"
       });

       questionBank.Add(new List<string> 
       {
           "17",
           "Hvad karakteriserede den spanske ekspansion? ",
           "Spanierne plyndrede de kyster de besøgte og tog hurtigt af sted igen med erobrede slaver",
           "Spanierne kom til lande med en relativ tynd befolkning, som de ikke blot erobrede men også befolkede med udvandrere og negerslaver",
           "Spanierne etablerede kolonier langs kysten, der tjente som handelsstationer og militære støttepunkter for deres herredømme over de indfødte",
           "B"
       });

       questionBank.Add(new List<string> 
       {
           "18",
           "Den europæiske ekspansion i 1400-1600-tallet faldt sammen med renæssancen - det store åndelige og materielle opsving i Europa. Men hvad var den ydre anledning til ekspansionen? ",
           "Udviklingen af skibstyper som kunne transportere tilstrækkelig med mennesker og proviant over lange afstande",
           "Amerikas opdagelse 1492 og opdagelsen af søvejen til Indien 1498",
           "Amerikas opdagelse i 1492 og opdagelsen af kompasset som tillod præcis navigation over lange afstande",
           "B"
       });

       questionBank.Add(new List<string> 
       {
           "19",
           "Hvad har opdagelsen af Sydamerika betydet for det danske køkken idag?  ",
           "Fra Sydamerika har vi fået citroner og tomater  ",
           "Fra Sydamerika har vi fået kartofler og tomater",
           "Fra Sydamerika har vi fået kartofler og citroner",
           "C"
       });

       questionBank.Add(new List<string> 
       {
           "20",
           "Hvornår beherskede Aztekerne Mexico ",
           "1400-1500",
           "1500-1600",
           "1600-1700",
           "A"
       });

       questionBank.Add(new List<string> 
       {
           "21",
           "Hvad var Aztekerrigets nationalret?",
           "Tacos",
           "Tortillas",
           "Risotto",
           "B"
       });

       questionBank.Add(new List<string> 
       {
           "22",
           "Hvilke byer består Tripelalliancen af?",
           " Texcoco, Tenochtitlan og Mexico ",
           "Texcoco, Frederikssund og Pangpanga",
           "Texcoco, Tenochtitlan og Tlacopan",
           "C"
       });

       questionBank.Add(new List<string> 
       {
           "23",
           "Hvad er Aztekerne mest berømt for? ",
           "Deres musik.",
           "Deres menneskeofringer",
           "deres mad. ",
           "B"
       });

       questionBank.Add(new List<string> 
       {
           "24",
           "Hvad er aztekernes vigtigste fødevarer? ",
           "Æble, gulerødder og jordbær ",
           "Citroner, ærter og majs",
           "Majs, bønner og squash ",
           "C"
       });

       questionBank.Add(new List<string> 
       {
           "25",
           "Hvor kommer aztekerner fra ifølge legenderne?",
           "Texas ",
           "Aztlán ",
           "England",
           "B"
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

    public void SetRandomQuestionNumber(int randomNumber)
    {
        Debug.Log(GameManager.Instance.GetMyPlayerTeam());
        _randomQuestionNumber = randomNumber;
    }

    public int GetRandomQuestionNumber()
    {
        return _randomQuestionNumber;
    }

    public List<string> GetOneQuestion(GameData.TeamCountry inviter, GameData.TeamCountry invited, out int questionNumber)
    {
        _inviter = inviter;
        _invited = invited;

        //SendTheQuestionNumber(0, _invited);

        //singular attack
        if (invited == GameData.TeamCountry.___)
        {
            //finding the player (inviter) that requested for a question betwwen all players
            foreach (GameObject player in GameManager.Instance.allPlayers)
            {
                //this is the player who requested a question
                if (player.GetComponent<PlayerController>().GetMyTeam() == inviter)
                {
                    _total = player.GetComponent<PlayerController>().GetTotalQuestions(inviter);

                    if (_total.Count >= questionBank.Count)
                    {
                        //Debug.Log(_total.Count + ">=" + questionBank.Count);

                        //means we ran out of questions and pick a random number from the qwhole uestions.
                        //TODO: make an alarm to report afterwards. plus we need to prevent total questions of players added by same number cause the question is not new anymore
                        _randomQuestionNumber = Random.Range(1, _questionsInBank.Length + 1);
                    }
                    else
                    {
                        //Debug.Log(_total.Count + "<=" + questionBank.Count);

                        //pick a random number from the questions that the player never answered.
                        _result = _questionsInBank.Where(x => !_total.Contains(x)).ToList();
                        _randomQuestionNumber = Random.Range(1, _result.Count + 1);
                    }
                }
            }
        }
        else
        {
            //finding the players (inviter and invited) that requested for a question between all players
            foreach (GameObject player in GameManager.Instance.allPlayers)
            {
                if (player.GetComponent<PlayerController>().GetMyTeam() == inviter)
                {
                    _total.AddRange(player.GetComponent<PlayerController>().GetTotalQuestions(inviter));
                }
                if (player.GetComponent<PlayerController>().GetMyTeam() == invited)
                {
                    _total.AddRange(player.GetComponent<PlayerController>().GetTotalQuestions(invited));
                }

                if (_total.Count >= questionBank.Count)
                {
                    //TODO: find the team with less totalquestion.count and put it in total and take random
                    _randomQuestionNumber = Random.Range(1, _questionsInBank.Length + 1);
                    //TODO: in the end of attack we need to set from the city to 0
                }
                else
                {
                    _result = _questionsInBank.Where(x => !_total.Contains(x)).ToList();

                    _randomQuestionNumber = Random.Range(1, _result.Count + 1);
                    //GetComponent<PhotonView>().RPC("SetRandomQuestionNumber", PhotonTargets.All, _randomQuestionNumber);
                }
            }
        }

        foreach (List<string> question in questionBank)
        {
            if (_total.Count >= questionBank.Count)
            {
                Debug.Log("if");
                if (question[0] == _questionsInBank[_randomQuestionNumber - 1].ToString())
                {
                    Debug.Log("question number: " + question[0]);
                    questionNumber = _questionsInBank[_randomQuestionNumber - 1];
                    SendTheQuestionNumber(questionNumber, invited);
                    //GetComponent<PhotonView>().RPC("ClearLists", PhotonTargets.All, inviter, invited);
                    return question;
                }
            }
            else
            {
                Debug.Log("else");
                if (question[0] == _result[_randomQuestionNumber - 1].ToString())
                {
                    Debug.Log("question number: " + question[0]);
                    questionNumber = questionNumber = _result[_randomQuestionNumber - 1];
                    SendTheQuestionNumber(questionNumber, invited);
                    //GetComponent<PhotonView>().RPC("ClearLists", PhotonTargets.All, inviter, invited);
                    return question;
                }
            }
        }

        Debug.Log("question returnd null, why?!");

        questionNumber = _randomQuestionNumber;
        return null;
    }

    public void SendTheQuestionNumber(int questionNumber, GameData.TeamCountry invited)
    {
        _randomQuestionNumber = questionNumber;
        foreach (GameObject player in GameManager.Instance.GetAllPlayers())
        {
            player.GetComponent<PhotonView>().RPC("SetRandomQuestionNumber", PhotonTargets.All, questionNumber, invited);
        }
    }
    public List<string> GetInvitedQuestion(int questionNumber)
    {
        foreach (List<string> question in questionBank)
        {
            if (question[0] == questionNumber.ToString())
            {
                return question;
            }
        }
        return null;
    }


    [PunRPC]
    public void ClearLists(GameData.TeamCountry inviter, GameData.TeamCountry invited)
    {
        if (GameManager.Instance.GetMyPlayerTeam() == inviter | GameManager.Instance.GetMyPlayerTeam() == invited)
        {
            _total.Clear();
            _result.Clear();
        }
    }


    public void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            int rndQuestion;
            foreach (string item in GetOneQuestion(GameData.TeamCountry.Denmark, GameData.TeamCountry.___, out rndQuestion))
            {
                Debug.Log(item + " - " + rndQuestion);
            }
        }
    }
}
