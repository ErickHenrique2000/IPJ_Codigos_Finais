using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBossController : MonoBehaviour
{
    public float direcao, forcaTiro;
    private Rigidbody2D rb;
    private float  tempoVivo;

    /// <Start>
    /// Verifica a direção em que o boss está olhando
    /// Adiciona uma força nessa direção
    /// </Start>
    void Start()
    {
        direcao = PlayerController.Direcao;
        if (direcao == 0) {
            direcao = 1;
        }

        rb = this.GetComponent<Rigidbody2D>();
        rb.AddForce(Vector3.right * direcao * -1 * forcaTiro);
    }

    /// <FixedUpdate>
    /// Verifica se está vivo a mais de 5 segundo e caso esteja destroi o objeto
    /// </FixedUpdate>
    private void FixedUpdate() {
        rb.position = new Vector3(rb.position.x, rb.position.y, -1);

        tempoVivo += Time.fixedDeltaTime;
        if (tempoVivo >= 5f) {
            Destroy(gameObject);
        }
    }
}
