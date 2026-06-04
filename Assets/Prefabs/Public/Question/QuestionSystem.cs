using System;        // ← DateTime、TimeSpan 都在這個命名空間
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class QuestionSystem: MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        public string questionText;
        public string[] answers;
        public int correctAnswerIndex;
        public string explanation;   // ★新增：詳解
    }

    public List<Question> questions;
    public Text questionText;
    public Button[] answerButtons;
    public Image backgroundImage;
    public GameObject canvasObject;
    public GameObject feedbackPanel;
    public Text feedbackText;
    public Text explanationText;          // ★新增：指向詳解的 UI Text
    public Animator feedbackAnimator;
    
    public TaskSystemUnit3 taskSystemUnit3; // 連結到 TaskSystemUnit3 的物件

    private int currentQuestionIndex = 0;

    void Start()
    {
        InitializeQuestions();

        // 綁定按鈕事件
        for (int i = 0; i < answerButtons.Length; i++)
        {
            int index = i; // 必須這樣避免閉包問題
            answerButtons[i].onClick.AddListener(() => SelectAnswer(index));
        }

        ShowQuestion();
    }

    private void InitializeQuestions()
    {
        questions = new List<Question>
        {
            new Question
            {
                questionText = "我們知道軟流圈通常溫度處在1300攝氏度，因此會在哪種板塊構造下方",
                answers = new string[] { "岩石圈", "大陸地殼", "海洋地殼", "都不是" },
                correctAnswerIndex = 0,
                explanation = "軟流圈屬於地函上部，直接承載在它上方的是岩石圈。"
            },
            new Question
            {
                questionText = "當板塊正在發生碰撞，請問最有可能是哪種板塊邊界",
                answers = new string[] { "聚合型邊界", "張裂型邊界", "錯動型邊界" },
                correctAnswerIndex = 0,
                explanation = "兩板塊互相擠壓、聚合，故屬於聚合型（Convergent）邊界。"
            },
            new Question
            {
                questionText = "當發現岩漿往一個方向去聚集且移動很慢，請問這是哪種邊界類型",
                answers = new string[] { "聚合型邊界", "張裂型邊界", "錯動型邊界" },
                correctAnswerIndex = 0,
                explanation = "聚合型邊界的隱沒帶易產生黏稠岩漿，流動緩慢且向上聚集。"
            },
            new Question
            {
                questionText = "美國加州的聖安德魯斯斷層屬於甚麼板塊邊界",
                answers = new string[] { "聚合型邊界", "張裂型邊界", "錯動型邊界" },
                correctAnswerIndex = 2,
                explanation = "聖安德魯斯斷層是北美板塊與太平洋板塊的錯動（Transform）邊界。"
            },
            new Question
            {
                questionText = "如果今天觀測到中源地震發生，震源深200公里，請問是甚麼板塊邊界",
                answers = new string[] { "聚合型邊界", "張裂型邊界", "錯動型邊界" },
                correctAnswerIndex = 0,
                explanation = "深於70 km 的中深層地震多發生於隱沒帶，屬聚合型邊界。"
            }
        };
    }

    public void ShowQuestion()
    {
        backgroundImage.gameObject.SetActive(true);

        if (currentQuestionIndex >= questions.Count)
        {
            Debug.LogWarning("問題數已超過上限");
            return;
        }

        // 顯示題目
        questionText.text = questions[currentQuestionIndex].questionText;

        // 顯示選項
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
        bool isCorrect = index == questions[currentQuestionIndex].correctAnswerIndex;

        // ====== 新增作答紀錄到 SessionLogger ======
        string qId     = $"Q{currentQuestionIndex + 1}";
        string qText   = questions[currentQuestionIndex].questionText;
        string answer  = questions[currentQuestionIndex].answers[index];
        string correct = questions[currentQuestionIndex].answers[questions[currentQuestionIndex].correctAnswerIndex];
        SessionLogger.LogQuestion(qId, qText, answer, correct, isCorrect);
        SessionLogger.LogAction($"問題: {qText} | 選擇: {answer}");

        // 顏色顯示
        if (isCorrect)
        {
            answerButtons[index].GetComponent<Image>().color = Color.green;
            ShowFeedback("答對了！", Color.green);
        }
        else
        {
            answerButtons[index].GetComponent<Image>().color = Color.red;
            ShowFeedback("答錯了！", Color.red);
        }

        StartCoroutine(NextQuestionAfterDelay(3f));
    }

    private IEnumerator NextQuestionAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        feedbackPanel.SetActive(false);

        foreach (Button btn in answerButtons)
        {
            btn.GetComponent<Image>().color = Color.white;
        }

        currentQuestionIndex++;
        if (currentQuestionIndex < questions.Count)
        {
            ShowQuestion();
        }
        else
        {
            Debug.Log("已完成所有問題");
            taskSystemUnit3.ForceCompleteTask("完成最後試題");
            canvasObject.SetActive(false);

            // ★★ 額外加碼：自動存檔
            //string dir = "/Users/eric/Desktop/My project/";
            //string path = dir + $"{DateTime.Now:yyyyMMdd_HHmmss}.csv";
            //Directory.CreateDirectory(dir);
            //SessionLogger.Instance.SaveToFile(path);
            //Debug.Log($"[Test] 檔案輸出於：{path}");
        }
    }

    public void ShowFeedback(string message, Color textColor)
    {
        feedbackPanel.SetActive(true);

        feedbackText.text  = message;
        feedbackText.color = textColor;

        // ★顯示詳解
        if (explanationText != null)
        {
            explanationText.text  = questions[currentQuestionIndex].explanation;
            explanationText.color = Color.white;
        }

        feedbackAnimator.SetBool("IsVisible", true);
        StartCoroutine(HideFeedbackAfterDelay(5f));
    }

    private IEnumerator HideFeedbackAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        feedbackAnimator.SetBool("IsVisible", false);
    }
}
