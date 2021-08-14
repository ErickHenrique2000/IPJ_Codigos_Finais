using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public  Dialogue    dialogue;
    public  Dialogue    boss;
    public  bool        iniciaComOJogo;

    /// <Start>
    /// Chama a função triggerDialogue para iniciar o dialogo
    /// </Start>
    void Start()
    {
        if (iniciaComOJogo) {
            TriggerDialogue(false, false, false);
        }
    }

    /// <TriggerDialogue>
    /// Encontra o DialogueMannager e inicia o dialogo
    /// </TriggerDialogue>
    public void TriggerDialogue(bool fim, bool playerWin, bool bossWin) {
        if (bossWin) {
            FindObjectOfType<DialogueManager1>().StartDialogue(boss, fim, playerWin);
        } else {
            FindObjectOfType<DialogueManager1>().StartDialogue(dialogue, fim, playerWin);
        }
    }
}
