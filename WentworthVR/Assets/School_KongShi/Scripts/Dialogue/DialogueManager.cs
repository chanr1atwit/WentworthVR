using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    private Queue<string> dialogueQueue;
    private readonly float typingSpeed = 0.01f;

    // Action events
    public event Action StartTalking = delegate { };
    public event Action StopTalking = delegate { };

    // Gameobject fields for visible dialogue box
    [SerializeField] private Animator dialogueAnim;
    [SerializeField] private Text nameText;
    [SerializeField] private Text sentenceField;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        dialogueQueue = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        StartTalking?.Invoke();
        dialogueAnim.SetBool("DialogueActive", true);
        nameText.text = dialogue.speakerName;
        dialogueQueue.Clear();
        foreach (string s in dialogue.sentences)
        {
            dialogueQueue.Enqueue(s);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        StopAllCoroutines();
        if (dialogueQueue.Count == 0)
        {
            dialogueAnim.SetBool("DialogueActive", false);
            StopTalking?.Invoke();
            return;
        }
        string nextSentence = dialogueQueue.Dequeue();
        StartCoroutine(nameof(TypeWords), nextSentence);
    }

    private IEnumerator TypeWords(string sentence)
    {
        sentenceField.text = "";
        foreach (char c in sentence)
        {
            sentenceField.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}