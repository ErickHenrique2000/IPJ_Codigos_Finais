using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public  float       velocidade, jumpForce;
    public  float       lado;
    public  static  float   Direcao = 0;

    public  LayerMask   layerChao;

    public  GameObject  prefabPoder;
    public  LayerMask   layerBoss;

    public  float       vidaMaxima, vida;

    public  Animator    playerAnimator;

    private Rigidbody2D rb;
    private Transform   transformCheck;
    private bool        estaNoChao = false;
    private Image       barraDeVida;

    /// <Start>
    /// O start irá:
    /// Capturar o rigidbody do player
    /// Capturar o Transform do elemento GroundCheck que é filho do player
    /// Atualiar a vida que será utilizada em jogo com o valor da vida maxima
    /// </Start>
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        vida = vidaMaxima;
        transformCheck = GameObject.Find("GroundCheck").GetComponent<Transform>();
        barraDeVida = GameObject.Find("lifeBar").GetComponent<Image>();
    }

    /// <Update> 
    /// O update irá:
    /// Verificar se o jump foi precionado e caso tenha sido ira ver se o jogador está no chão, caso esteja realliza o pulo
    /// Verificar a vida, caso seja menor ou igual a 0 deve ser carregado a fase de EndGame
    /// Ver o lado em que o jogador está desejando ir, -1 para esquerda, 0 quando não preciona nada e 1 quando quer ir para a direita
    /// Atualizar a variavel direção que será usada pelo script do poder para saber para qual lado o poder deve ser lançado
    /// verificar se o botão R, o que lança o poder e verifica quais outros botões que controlam a cor do golpe estão precionado e utiliza essas informações para controlar a cor do golpe
    /// </Update>
    void Update()
    {
        if (Input.GetButtonDown("Jump")) {
            if(Physics2D.OverlapCircle(transformCheck.position, 0.15f, layerChao)) {
                rb.AddForce(Vector3.up * jumpForce);
                SoundEffectsController.Instancia.CriarSom("jump");
            }
        }

        if(lado != 0) {
            playerAnimator.SetBool("andando", true);
        } else {
            playerAnimator.SetBool("andando", false);
        }

        rb.transform.localScale = new Vector3(1, 1, 1);
        if(Direcao < 0) {
            rb.transform.localScale = new Vector3(-1, 1, 1);
        }

        barraDeVida.fillAmount = vida / vidaMaxima;

        if (vida <= 0) {
            DialogueTrigger dt = this.GetComponent<DialogueTrigger>();
            if (Physics2D.OverlapCircle(transform.position, 20, layerBoss)) {
                dt.TriggerDialogue(true, false, true);
            } else {
                dt.TriggerDialogue(true, false, false);
            }
           // SceneManager.LoadScene("EndGame");
        }

        lado = Input.GetAxisRaw("Horizontal");
        if(lado != 0) {
            Direcao = lado;
        }
        
        if (Input.GetKeyDown(KeyCode.R)) {
            GameObject objeto = Instantiate(prefabPoder, new Vector3(rb.position.x, rb.position.y, 0), Quaternion.identity);

            int r, g, b;
            r = g = b = 0;

            if (Input.GetKey(KeyCode.E)) {
                b = 1;
            }
            if (Input.GetKey(KeyCode.W)) {
                g = 1;
            }
            if (Input.GetKey(KeyCode.Q)) {
                r = 1;
            }

            SoundEffectsController.Instancia.CriarSom("poder");
            objeto.GetComponent<PowerController>().SetCollor(r, g, b);
        }
    }

    /// <FixedUpdate>
    /// Move o rigidbody conforme o lado que o personagem planeja ir
    /// </FixedUpdate>
    private void FixedUpdate() {
        rb.position = new Vector3(rb.position.x + (velocidade * Time.fixedDeltaTime * lado), rb.position.y, 0);
    }

    /// <OnTriggerEnter2D>
    /// se o trigger vier de um objeto bosspower perde 10 de vida e destroi o objeto que o atingiu
    /// </OnTriggerEnter2D>
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "BossPower") {
            Destroy(collision.gameObject);
            vida -= 10;
        }
    }
}
