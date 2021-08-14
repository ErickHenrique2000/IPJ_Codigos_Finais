using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsController : MonoBehaviour
{

    public  static SoundEffectsController  Instancia;
    public  AudioClip[]             efeitos;
    public  AudioSource             audio;

    /// <Start>
    /// atribui a instancia atual a referencia estatica e caso já exista destroi essa instancia
    /// captura o audiosource
    /// </Start>
    void Start()
    {
        if(Instancia == null) {
            Instancia = this;
        } else {
            Destroy(this);
        }

        audio = this.GetComponent<AudioSource>();
    }

    /// <CriarSom>
    /// recebe a string som e toca o clip baseado em qual som foi requerido
    /// </CriarSom>
    public void CriarSom(string som) {
        switch (som) {
            case "jump":
                audio.clip = efeitos[0];
                audio.Play();
                break;
            case "dano":
                audio.clip = efeitos[1];
                audio.Play();
                break;
            case "poder":
                audio.clip = efeitos[2];
                audio.Play();
                break;
            case "danoInimigo":
                audio.clip = efeitos[3];
                audio.Play();
                break;
            default:
                break;
        }
    }
}
