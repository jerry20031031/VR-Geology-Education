using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace Duck{
public class QuestionSystemUnit1 : MonoBehaviour
{
    public class Question
    {
        public string questionText; // ???D???e
        public string[] answers;    // ?????
        public int correctAnswerIndex; // ???T???????
    }

    [System.Serializable]
    public struct AnswerData
    {
        public string AnswerText; // ???D???e
        public Sprite image; // ???T???????
    }


    public GameObject missonUI;
    public List<Question> questions; // ???D?C??
    public List<AnswerData> answerDatas;
    public Text questionText;
    public Button[] answerButtons;
    public Image backgroundImage;
    public GameObject canvasObject;
    public GameObject feedbackPanel; // ???????O
    public TextMeshProUGUI feedbackText; // ??????r
    public TextMeshProUGUI feedbackText2;
    public Image feedbackImage; // ??????r
    public GameObject EndPanel;
    public GameObject finishQuestion; // ???????O

    private int currentQuestionIndex = 0;

    void Start()
    {
        // ??l????D?C??
        InitializeQuestions();
        LogSystem.instance.InitQuestionRecord();

        // ???j?w???s???I?????
        for (int i = 0; i < answerButtons.Length; i++)
        {
            int index = i; // ?O?s???a??q?A??K???]???D
            answerButtons[i].onClick.AddListener(() => SelectAnswer(index));
        }

        ShowQuestion();
    }

    // ??l????D?C??
    private void InitializeQuestions()
    {
        questions = new List<Question>
     {
         new Question
         {
             questionText = "哪位科學家提出盤古大陸?",
             answers = new string[] { "威廉·倫琴", "查爾斯·達爾文", "韋格納", "海斯" },
             correctAnswerIndex = 2
         },
         new Question
         {
             questionText = "韋格納在提出「大陸漂移說」時，曾以哪些主要證據支持各大陸曾經連在一起的理論？",
             answers = new string[] { "化石分布、相似的海岸線、相對應山脈構造", "化石分布、海洋深度變化、沉積物方向", "相對應山脈構造、洋流方向、火山活動", "洋流方向、火山活動、海洋深度變化" },
             correctAnswerIndex = 0
         },
         new Question
         {
             questionText = "海底擴張中認為海底岩漿沿著海底的哪個位置上升?",
             answers = new string[] { "軟流圈", "海溝", "中洋脊", "沉積物" },
             correctAnswerIndex =2
         },
          new Question
         {
             questionText = "中洋脊噴發岩漿中的礦物，礦物冷卻後磁化並記錄當時地球磁場的海洋地殼往兩旁擴張，這個現象最後會記錄地球磁場的甚麼變化?",
             answers = new string[] { "地磁反轉", "地磁垂直變化", "地磁不變", "地磁旋轉" },
             correctAnswerIndex = 0
         },
           new Question
         {
             questionText = "海底擴張岩漿沿著海底的中洋脊上升，中洋脊岩漿冷卻後會形成新的甚麼地質?",
             answers = new string[] { "海洋地殼", "大陸地殼", "沉積物", "地核" },
             correctAnswerIndex = 0
         }
     };
    }


    public void ShowQuestion()
    {
        // ?T?O?I???l?????
        backgroundImage.gameObject.SetActive(true);

        // ??d???D?O?_?s?b
        if (currentQuestionIndex >= questions.Count)
        {
            Debug.LogWarning("???D????W?X?d??I");
            return;
        }

        // ?]?m???D??r
        questionText.text = questions[currentQuestionIndex].questionText;
        questionText.color = Color.black;

        // ?]?m??????s
        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (i < questions[currentQuestionIndex].answers.Length)
            {
                answerButtons[i].GetComponentInChildren<Text>().text = questions[currentQuestionIndex].answers[i];
                answerButtons[i].gameObject.SetActive(true);
            }
            else
            {
                answerButtons[i].gameObject.SetActive(false);
            }
        }
    }

    public void SelectAnswer(int index)
    {
        Debug.Log($"???s?Q?I???A???????????G{index}");
        Question currentQuestion = questions[currentQuestionIndex];
        string logMessage = $"問題: {currentQuestion.questionText} | 選擇: {currentQuestion.answers[index] + 1}";
        LogSystem.LogAction(logMessage);

        bool isCorrect = index == questions[currentQuestionIndex].correctAnswerIndex; // ?P?_?O?_???T
        if (isCorrect)
        {
            Debug.Log("恭喜答對");
            answerButtons[index].GetComponent<Image>().color = Color.green; // ???s????
            ShowFeedback("恭喜答對", Color.green);
            LogSystem.instance.UpdateQuestionRowByIndex(currentQuestionIndex, currentQuestion.answers[index], true); // 更新問題行
        }
        else
        {
            Debug.Log("答案錯誤");
            answerButtons[index].GetComponent<Image>().color = Color.red; // ???s?????
            ShowFeedback("答案錯誤", Color.red);
            LogSystem.instance.UpdateQuestionRowByIndex(currentQuestionIndex, currentQuestion.answers[index], false); // 更新問題行
        }


    }
    public void NextQuestion()
    {
        foreach (Button btn in answerButtons)
        {
            btn.GetComponent<Image>().color = Color.white;
        }
        foreach (Button btn in answerButtons)
        {
            btn.interactable = true;
        }
        currentQuestionIndex++;
        if (currentQuestionIndex < questions.Count)
        {
            ShowQuestion();
        }
        else
        {
            //missonUI.SetActive(false);
            //EndPanel.SetActive(true);
            canvasObject.SetActive(false);
            finishQuestion.SetActive(true);
            LogSystem.instance.ExportAllToExcel(); // 將所有問題行導出到Excel
        }
    }
    public void ShowFeedback(string message, Color textColor)
    {
        feedbackText2.text = answerDatas[currentQuestionIndex].AnswerText; // ??????r
        feedbackImage.sprite = answerDatas[currentQuestionIndex].image; // ??????r
        feedbackPanel.SetActive(true); // ?????O
        feedbackText.text = message;  // ?]?m??r
        feedbackText.color = textColor; // ?]?m?C??\
        foreach (Button btn in answerButtons)
        {
            btn.interactable = false;
        }


    }
}
}