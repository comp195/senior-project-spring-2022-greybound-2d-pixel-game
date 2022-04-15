using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;
    public GameObject DialogueMenuUI;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public static bool GameIsPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        DialogueMenuUI.SetActive(true);

        Time.timeScale = 0f;
        GameIsPaused = true;

        nameText.text = dialogue.name;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
        //Debug.Log("Starting conervation with " + dialogue.name);
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
        Debug.Log(sentence);
    }

    public void EndDialogue()
    {
        DialogueMenuUI.SetActive(false);

        Time.timeScale = 1f;
        GameIsPaused = false;

        Debug.Log("End of conversation");
    }
}
