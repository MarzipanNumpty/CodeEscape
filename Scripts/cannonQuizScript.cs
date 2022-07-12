using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cannonQuizScript : MonoBehaviour
{
    [SerializeField]
    int[] answers;
    [SerializeField]
    [TextArea]
    string[] Questions;
    public int currentQuestion;
    [SerializeField]
    Text questionText;
    [SerializeField]
    GameObject quizCanvas;
    public bool quizActive;
    bool[] rightWrong = new bool[] { false, false };
    [SerializeField]
    GameObject blueDragon;
    blueDragonScript bdScript;
    bool checkingAnswer;
    [System.Serializable]
    public class textList
    {
        public List<string> stringAnswers;
    }
    public List<textList> allAnswers = new List<textList>();
    [SerializeField]
    Text[] textAnswers;
    float maxTimer = 5.0f;
    float actualTimer;
    bool timerStarted;
    [SerializeField]
    GameObject cam;
    [SerializeField] GameObject resultsScreenCanvas;
    [SerializeField] GameObject tablePrefab;
    [SerializeField] GameObject scrollViewContent;
    [SerializeField] Text correctNumText;
    [SerializeField] Text totalNumText;
    int tablePositioning = 660;
    int playerAnswer;
    int correctQuestions;
    int totalQuestions;
    [SerializeField] GameObject fireball;
    [SerializeField] Transform fireballTransform;
    [SerializeField] GameObject[] cannonBallSpawnPos;
    [SerializeField] GameObject cannonBall;
    [SerializeField] GameObject bigQuestionText;
    void Start()
    {
        bigQuestionText.SetActive(false);
        quizCanvas.SetActive(false);
        questionText.text = Questions[currentQuestion];
        for(int i = 0; i < textAnswers.Length; i++)
        {
            textAnswers[i].text = allAnswers[currentQuestion].stringAnswers[i];
        }
        bdScript = blueDragon.GetComponent<blueDragonScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(quizActive && !checkingAnswer)
        {
            rightWrong[0] = false;
            rightWrong[1] = false;
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                cannonOne();
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                cannonTwo();
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                cannonThree();
            }
        }
        if(actualTimer > 0 && timerStarted)
        {
            actualTimer -= Time.deltaTime;
        }
        else if(actualTimer <= 0 && timerStarted)
        {
            timerStarted = false;
            nextQuestion();
        }
    }

    public void cannonOne()
    {
        playerAnswer = 1;
        if (answers[currentQuestion] == 1)
        {
            rightWrong[0] = true;
        }
        else
        {
            rightWrong[1] = true;
        }
        checkingAnswer = true;
        checkResults();
    }

    public void cannonTwo()
    {
        playerAnswer = 2;
        if (answers[currentQuestion] == 2)
        {
            rightWrong[0] = true;
        }
        else
        {
            rightWrong[1] = true;
        }
        checkingAnswer = true;
        checkResults();
    }
    
    public void cannonThree()
    {
        playerAnswer = 3;
        if (answers[currentQuestion] == 3)
        {
            rightWrong[0] = true;
        }
        else
        {
            rightWrong[1] = true;
        }
        checkingAnswer = true;
        checkResults();
    }

    public void StartQuiz()
    {
        cam.SetActive(true);
        quizCanvas.SetActive(true);
        quizActive = true;
    }

    void nextQuestion()
    {
        if(currentQuestion + 1 < Questions.Length && totalQuestions - correctQuestions < 4)
        {
            if(currentQuestion == 6)
            {
                bigQuestionText.SetActive(true);
            }
            quizCanvas.SetActive(true);
            currentQuestion++;
            questionText.text = Questions[currentQuestion];
            for (int i = 0; i < textAnswers.Length; i++)
            {
                textAnswers[i].text = allAnswers[currentQuestion].stringAnswers[i];
            }
        }
        else
        {
            resultsScreenCanvas.SetActive(true);
            quizCanvas.SetActive(false);
        }
    }

    void checkResults()
    {
        if(rightWrong[0])
        {
            correctQuestions++;
            GameObject cb = Instantiate(cannonBall, cannonBallSpawnPos[answers[currentQuestion] - 1].transform.position, Quaternion.identity);
            cb.GetComponent<projectileScript>().activateMovement(fireballTransform, cannonBallSpawnPos[1].transform, false);
        }
        else
        {
            bdScript.removeHealth(1.0f);
            GameObject fb = Instantiate(fireball, fireballTransform.position, Quaternion.identity);
            fb.GetComponent<projectileScript>().activateMovement(cam.transform, cannonBallSpawnPos[1].transform, true);
        }
        actualTimer = maxTimer;
        timerStarted = true;
        quizCanvas.SetActive(false);
        totalQuestions++;
        totalNumText.text = totalQuestions.ToString();
        correctNumText.text = correctQuestions.ToString();
        checkingAnswer = false;
        GameObject currentCanvas = Instantiate(tablePrefab, scrollViewContent.transform, scrollViewContent.transform);
        currentCanvas.transform.SetParent(scrollViewContent.transform);
        //currentCanvas.transform.position = new Vector3(0, tablePositioning, 0);
        currentCanvas.transform.localPosition = new Vector2(currentCanvas.transform.localPosition.x, currentCanvas.transform.localPosition.y + tablePositioning);
        currentCanvas.transform.localScale = new Vector3(1, 1, 1);
        tablePositioning -= 150;
        currentCanvas.transform.GetChild(0).GetComponent<Text>().text = Questions[currentQuestion];
        currentCanvas.transform.GetChild(1).GetComponent<Text>().text = allAnswers[currentQuestion].stringAnswers[playerAnswer - 1];
        currentCanvas.transform.GetChild(2).GetComponent<Text>().text = allAnswers[currentQuestion].stringAnswers[answers[currentQuestion] - 1];
    }
}
