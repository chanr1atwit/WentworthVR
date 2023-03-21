using UnityEngine;

public class DialogueTrigger : Interactable, IInteractable
{
    [SerializeField] private Dialogue dialogue;

    public void Interact()
    {
        if (dialogue.speakerName == "")
        {
            dialogue.speakerName = gameObject.name;
        }
        if (dialogue.sentences.Count < 1)
        {
            Debug.LogWarning("Error in dialogue setup");
            return;
        }
        DialogueManager.instance.StartDialogue(dialogue);
    }
}
