using UnityEngine;

public class PowerController : MonoBehaviour
{
    public  float   direcao, forcaTiro, passoOndulacao, grauOndulacao;
    private Rigidbody2D rb;
    private float   contadorAngulo = 0, tempoVivo;   
    private SpriteRenderer  sr;

    /// <Start>
    /// pega a dire��o em que o player est� olhando para poder lan�ar o poder do jogador
    /// captura o rigidbody
    /// adiciona uma for�a no poder na dire��o que deve ir
    /// </Start>
    void Start()
    {
        direcao = PlayerController.Direcao;
        if(direcao == 0) {
            direcao = 1;
        }

        rb = this.GetComponent<Rigidbody2D>();
        rb.AddForce(Vector3.right * direcao * forcaTiro);
    }

    /// <SetCollor>
    /// Recebe as cores r, g, b
    /// pega o elemento spriterenderer
    /// coloca as variaveis rgb na cor do spriterenderer
    /// verifica quais s�o as cores e seta a tag do poder para ser a da cor
    /// </SetCollor>

    public void SetCollor(float r, float g, float b) {
        sr = this.GetComponent<SpriteRenderer>();
        sr.color = new Color(r, g, b, 1);

        if (r == 1) {
            if(g == 1) {
                if(b == 1) {
                    gameObject.tag = "White";
                } else {
                    gameObject.tag = "Yellow";
                }
            } else {
                if (b == 1) {
                    gameObject.tag = "Pink";
                } else {
                    gameObject.tag = "Red";
                }
            }
        } else {
            if (g == 1) {
                if (b == 1) {
                    gameObject.tag = "Cyan";
                } else {
                    gameObject.tag = "Green";
                }
            } else {
                if (b == 1) {
                    gameObject.tag = "Blue";
                } else {
                    gameObject.tag = "Black";
                }
            }
        }
    }

    /// <FixedUpdate>
    /// calcula o acrescimo que o Y ira receber
    /// aumenta o contador angulo com o passo que esse deve avan�ar a cada update
    /// atualiza a posi��o em y com o acrescimo divido pelo grau da ondula��o
    /// atualiza o tempo vivo e caso seja maior que 5 segundo destroi o objeto
    /// </FixedUpdate>

    private void FixedUpdate() {
        float acrescimoY = Mathf.Sin(contadorAngulo);
        contadorAngulo += passoOndulacao;

        rb.position = new Vector3(rb.position.x, rb.position.y + (acrescimoY / grauOndulacao), 0);

        tempoVivo += Time.fixedDeltaTime;
        if(tempoVivo >= 5f) {
            Destroy(gameObject);
        }
    }
}
