using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager1 : MonoBehaviour
{
    private Queue<string> sentences;
    public  Text nameText;
    public  Text dialogo;
    public  GameObject  painelDialogo;
    private bool        fimDeJogo, playerWin;

    /// <Awake>
    /// Inicia a Queue
    /// </Awake>
    void Awake() {
        sentences = new Queue<string>();
    }

    /// <StartDialogue>
    /// carrega o nome, zera a sequencia e carrega ela com as frases
    /// chama a proxima frase
    /// </StartDialogue>
    public void StartDialogue(Dialogue dialogue, bool fimDeJogo, bool playerWin) {
        this.fimDeJogo = fimDeJogo;
        this.playerWin = playerWin;
        painelDialogo.SetActive(true);
        nameText.text = dialogue.name;
        Time.timeScale = 0.0f;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    /// <DisplayNextSentence>
    /// Caso não exista proxima frase acaba o dialogo
    /// pega a proxima frase e coloca na tela
    /// </DisplayNextSentence>
    public void DisplayNextSentence() {
        if (sentences.Count == 0) {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogo.text = sentence;
    }

    /// <EndDialogue>
    /// Encerra o dialogo desativando o dialogo
    /// </EndDialogue>
    public void EndDialogue() {
        Time.timeScale = 1.0f;
        painelDialogo.SetActive(false);
        if(fimDeJogo == true) {
            if (playerWin) {
                SceneManager.LoadScene("WinGame");
            } else {
                SceneManager.LoadScene("EndGame");
            }
        }
    }
}
