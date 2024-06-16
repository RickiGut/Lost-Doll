using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NpcQuiz : MonoBehaviour
{
    public GameObject quizPanel; 
    public GameObject questionWithABCD; 
    public TextMeshProUGUI questionText; 
    public Button[] answerButtons; 
    public TextMeshProUGUI resultText;

    private bool isPlayerNearby = false;
    private string correctAnswer;

    // Data kuis
    private string question = "Siapakah yang mengambil koin pertama yang didapatkan tuan Krabs?";
    private string[] answers = { "A. Squidward", "B. Spongebob", "C.Tuan Krabs", "D. Cacing Besar Alasksa" };
    private string correct = "A. Squidward";


    //Player
    public SpriteRenderer player;
    private SpriteRenderer npcQuiz;
    

    void Start()
    {
        quizPanel.SetActive(false); 
        correctAnswer = correct;
        npcQuiz = GetComponent<SpriteRenderer>();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            int index = i; 
            answerButtons[i].onClick.RemoveAllListeners(); 
            answerButtons[i].onClick.AddListener(() => CheckAnswer(answers[index]));
        }
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F))
        {
            if(transform.position.x < player.transform.position.x ){
                npcQuiz.flipX = false;
            }else if(transform.position.x > player.transform.position.x){
                npcQuiz.flipX = true;
            }
            ShowQuiz();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Deteksi saat pemain berada di dekat NPC
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Deteksi saat pemain meninggalkan area NPC
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            quizPanel.SetActive(false);
        }
    }

    void ShowQuiz()
    {
        quizPanel.SetActive(true); 
        questionWithABCD.SetActive(true);
        questionText.text = question;
        resultText.text = "";

        for (int i = 0; i < answerButtons.Length; i++)
        {
            int index = i; 
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = answers[i];
            answerButtons[i].onClick.RemoveAllListeners(); 
            answerButtons[i].onClick.AddListener(() => CheckAnswer(answers[index])); 
        }
    }

    void CheckAnswer(string selectedAnswer)
    {
        if (selectedAnswer == correctAnswer)
        {
           resultText.text = "Benar,kau bisa lanjut kedepan saja nanti ada garis finish DEKK";
        }
        else
        {
            resultText.text = "Salah!!! KAU BUKAN ORANG INDONESIA PERGI AJA KAU DARI SINI!!!";
        }
           questionWithABCD.SetActive(false);

        StartCoroutine(HideQuiz(3f));
        
    }

    IEnumerator HideQuiz(float del){
        yield return new WaitForSeconds(del);
        quizPanel.SetActive(false);
    }


}
