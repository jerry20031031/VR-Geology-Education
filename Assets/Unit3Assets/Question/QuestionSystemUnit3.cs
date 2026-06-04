using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Unit3Assets.Question
{
    public class QuestionSystemUnit3 : MonoBehaviour
    {
        public class Question

        {
            public string questionText; // ���D���e
            public string[] answers;    // ���׿ﶵ
            public int correctAnswerIndex; // ���T���ׯ���
        }

        [System.Serializable]
        public struct AnswerData
        {
            public string AnswerText; // ���D���e
            public Sprite image; // ���T���ׯ���
        }


        public GameObject missonUI;
        public List<Question> questions; // ���D�C��
        public List<AnswerData> answerDatas;
        public Text questionText;
        public Button[] answerButtons;
        public Image backgroundImage;
        public GameObject canvasObject;
        public GameObject feedbackPanel; // ���ܮح��O
        public Text feedbackText; // ���ܮؤ�r
         public Text feedbackText2;
         public Image feedbackImage; // ���ܮؤ�r
        public GameObject EndPanel;

        private int currentQuestionIndex = 0;

        void Start()
        {
            // ��l�ư��D�C��
            InitializeQuestions();
            LogSystemUnit3.instance.InitQuestionRecord();

            // �۰ʸj�w���s���I���ƥ�
            for (int i = 0; i < answerButtons.Length; i++)
            {
                int index = i; // �O�s���a�ܶq�A�קK���]���D
                answerButtons[i].onClick.AddListener(() => SelectAnswer(index));
            }

            ShowQuestion();
        }

        // ��l�ư��D�C��
        private void InitializeQuestions()
        {
            questions = new List<Question>
        {
            new Question
            {
                questionText = "臺灣是由哪兩大板塊組成的?",
                answers = new string[] { "太平洋板塊 & 歐亞板塊", "印度洋板塊 & 歐亞板塊", "歐亞板塊 & 菲律賓海板塊" },
                correctAnswerIndex = 2 // ���T���סG���v����
            },
            new Question
            {
                questionText = "歐亞板塊隱沒入菲律賓海板塊的位置是？",
                answers = new string[] { "東南部", "南部", "西北部" },
                correctAnswerIndex = 1 // ���T���סG���S�O��
            },
            new Question
            {
                questionText = "臺灣西側海峽的名稱為何？這一地區的地質活動特徵是什麼？",
                answers = new string[] { "東海，火山活動頻繁", "澎湖海峽，地震頻繁", "臺灣海峽，地質相對穩定" },
                correctAnswerIndex = 2 // ���T���סG���s�a
            },
              new Question
            {
                questionText = "指出下列哪一個地區最有可能是位於地震活躍帶上",
                answers = new string[] { "澎湖縣", "金門縣", "花蓮縣" },
                correctAnswerIndex = 2 // ���T���סG���s�a
            },
                 new Question
            {
                questionText = "臺灣西部平原主要由哪些岩石構成？這些岩石的形成與什麼地質作用有關？",
                answers = new string[] { "變質岩，受板塊擠壓影響", "沉積岩，與河流沉積作用和板塊碰撞山脈隆起有關", "火成岩，受火山噴發影響" },
                correctAnswerIndex = 1 // ���T���סG���s�a
            }
        };
        }

        public void ShowQuestion()
        {
            // �T�O�I���l�����
            backgroundImage.gameObject.SetActive(true);

            // �ˬd���D�O�_�s�b
            if (currentQuestionIndex >= questions.Count)
            {
                Debug.LogWarning("���D���޶W�X�d��I");
                return;
            }

            // �]�m���D��r
            questionText.text = questions[currentQuestionIndex].questionText;
            questionText.color = Color.black;

            // �]�m���׫��s
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
            Debug.Log($"���s�Q�I���A��ܪ����ׯ��ޡG{index}");
            Question currentQuestion = questions[currentQuestionIndex];
            string logMessage = $"問題: {currentQuestion.questionText} | 選擇: {currentQuestion.answers[index]+1}";
            LogSystemUnit3.LogAction(logMessage);

            bool isCorrect = index == questions[currentQuestionIndex].correctAnswerIndex; // �P�_�O�_���T
            if (isCorrect)
            {
                Debug.Log("恭喜答對");
                answerButtons[index].GetComponent<Image>().color = Color.green; // ���s�ܺ��
                ShowFeedback("恭喜答對", Color.green);
                LogSystemUnit3.instance.UpdateQuestionRowByIndex(currentQuestionIndex, currentQuestion.answers[index], true); // 更新問題行
            }
            else
            {
                Debug.Log("答案錯誤");
                answerButtons[index].GetComponent<Image>().color = Color.red; // ���s�ܬ���
                ShowFeedback("答案錯誤", Color.red);
                LogSystemUnit3.instance.UpdateQuestionRowByIndex(currentQuestionIndex, currentQuestion.answers[index], false); // 更新問題行
            }


        }
        public void NextQuestion()
        {
            foreach (Button btn in answerButtons)
            {
                btn.GetComponent<Image>().color = Color.blue;
            }
            foreach (Button btn in answerButtons)
            {
                btn.interactable =true;
            }
            currentQuestionIndex++;
            if (currentQuestionIndex < questions.Count)
            {
                ShowQuestion();
            }
            else
            {  
                EndPanel.SetActive(true);
                canvasObject.SetActive(false);
                LogSystemUnit3.instance.ExportAllToExcel();
            }
        }
        public void ShowFeedback(string message, Color textColor)
        {
            feedbackText2.text = answerDatas[currentQuestionIndex].AnswerText; // ���ܮؤ�r
            feedbackImage.sprite = answerDatas[currentQuestionIndex].image; // ���ܮؤ�r
            feedbackPanel.SetActive(true); // ��ܭ��O
            feedbackText.text = message;  // �]�m��r
            feedbackText.color = textColor; // �]�m�C��\
            foreach (Button btn in answerButtons)
            {
                btn.interactable = false;
            }


        }
    }
}