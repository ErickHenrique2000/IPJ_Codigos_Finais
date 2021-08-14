using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private int         vida;
    public  static  float   ladoTiro;
    public  int         vidaMax;
    public  float       rangeDeteccao;
    public  LayerMask   layerPlayer;
    private bool        playerEncontrado;
    public  float       velocidade, periodo, periodoMudancaCor, periodoAtaque;
    public  Rigidbody2D rb;
    private float       tempoCorrido = 0, lado = 1, tempoCor = 0, tempoAtaque = 0;
    private GameObject  player;
    public  string[]    listaTags;
    public  GameObject  powerPrefab;

    /// <Start>
    /// carrega a vida com a vida maxima
    /// captura o rigid body do boss
    /// encontra o player
    /// </Start>
    void Start()
    {
        vida = vidaMax;
        rb = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        this.tag = "White";
    }

    /// <FixedUpdate>
    /// "inteligencia artificial" do boss
    /// caso o player não tenha sido encontrado fica dando voltas
    /// apos encontrar o player ele começa a verificar se é o momento de trocar a cor dele e caso seja realiza a troca da cor e tag
    /// apos isso armazena a distancia em que está do player e ferifica conforme a distancia se o player está a direita ou a esquerda e deixa o boss virado para o player
    /// caso essa distancia esteja muito perto significa que o boss está muito proximo do player e para isso precisa escapar dando um teleporte para atras do player
    /// caso a distancia seja menor que 8 o boss tenta se afastar para "kitar" o player
    /// caso seja o momento de realizar um tiro ele realiza o tiro em direção ao player
    /// </FixedUpdate>
    private void FixedUpdate()
    {
        playerEncontrado = Physics2D.OverlapCircle(transform.position, rangeDeteccao, layerPlayer);
        if (!playerEncontrado) {
            if (tempoCorrido > periodo) {
                rb.transform.localScale = new Vector3(rb.transform.localScale.x * -1, 1, 1);
                tempoCorrido = 0;
                lado *= -1;
            }

            rb.position = new Vector2(rb.position.x + (lado * (velocidade * Time.fixedDeltaTime)), rb.position.y);

            tempoCorrido += Time.fixedDeltaTime;
        } else {
            if (tempoCor > periodoMudancaCor) {
                tempoCor = 0;
                int indxNovaCor;
                indxNovaCor = Random.RandomRange(0, 4);

                gameObject.tag = listaTags[indxNovaCor];
                if(gameObject.tag == "White") {
                    gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                }else if (gameObject.tag == "Yellow") {
                    gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 0, 1);
                } else if (gameObject.tag == "Pink") {
                    gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0, 1, 1);
                } else {
                    gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 1, 1, 1);
                }
            } else {
                tempoCor+= Time.fixedDeltaTime;
            }

            float distancia = player.transform.position.x - rb.transform.position.x;

            if(rb.transform.localScale.x < 0) {
                rb.transform.localScale = new Vector3(rb.transform.localScale.x * -1, 1, 1);
            }

            if(distancia < 0) {
                rb.transform.localScale = new Vector3(rb.transform.localScale.x * -1, 1, 1);
            }

            if (Mathf.Abs(distancia) < 2) {
                if(distancia < 0) {
                    rb.transform.position = new Vector3(player.transform.position.x - 3, rb.transform.position.y, -1);
                    lado = -1;
                }else {
                    rb.transform.position = new Vector3(player.transform.position.x + 3, rb.transform.position.y, -1);
                }
            }
            ladoTiro = transform.localScale.x * -1;
            if (Mathf.Abs(distancia) < 8) {
                rb.transform.position = new Vector3(transform.position.x + (ladoTiro * velocidade * Time.fixedDeltaTime), transform.position.y,-1);
            }

            if (tempoAtaque > periodoAtaque) {
                tempoAtaque = 0;
                Instantiate(powerPrefab, new Vector3(rb.position.x, rb.position.y - 0.7f, 0), Quaternion.identity);
            } else {
                tempoAtaque += Time.fixedDeltaTime;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (gameObject.CompareTag(collision.gameObject.tag)) {
            Destroy(collision.gameObject);
            vida -= 10;
            if (vida == 0) {
                DialogueTrigger dt = this.GetComponent<DialogueTrigger>();
                if(dt != null) {
                    dt.TriggerDialogue(true, true, false);
                }
                Destroy(this.gameObject);
            }
        }
    }

    /// <OnDrawGizmos>
    /// desenha a área em que o player vai ser detectado (apenas para ficar algo mais visual enquanto o boss é configurado na unity)
    /// </OnDrawGizmos>
    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, rangeDeteccao);
    }
}
