using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghosts : MonoBehaviour
{
    public float velocidade = 6, velocRotate = 50;
    int tokenDash = 1;
    float moverZ, recargaToken, tempoRecarga = 2.5f;
    Quaternion rotacaoDireita, rotacaoEsquerda;
    Rigidbody rigbody;
    Collider collision;
    bool rotacionando = false;
    void Start()
    {
        rigbody = GetComponent<Rigidbody>();
        collision = GetComponent<Collider>();
    }

    void Update()
    {
        moverZ = Input.GetAxis("Vertical") * velocidade * Time.deltaTime;
        transform.Translate(0, 0, moverZ);
        if (Input.GetKeyDown(KeyCode.D))
        {
            rotacaoDireita = Quaternion.Euler(0, transform.position.y + 90, 0);
            StartCoroutine(rotacionarDireita(rotacaoDireita, 0.25f));
        }
            
        else if (Input.GetKeyDown(KeyCode.A))
        {
            rotacaoEsquerda = Quaternion.Euler(0, transform.position.y -90, 0);
            StartCoroutine(rotacionarEsquerda(rotacaoEsquerda, 0.25f));
        }
            

        if (Input.GetButtonDown("Jump"))
        {
            if(tokenDash == 1)
            {
                Vector3 frente = transform.forward;
                rigbody.AddForce(frente * 50000);
                collision.isTrigger = true;
                tokenDash = 0;
                print("Dash usado");
                StartCoroutine(DesacelerarDash(frente));
            }
            else
            {
                print("Dash recarregando...");
            }
            
        }
        if (tokenDash == 0)
        {
            recargaToken += Time.deltaTime;
            if (recargaToken >= tempoRecarga)
            {
                tokenDash = 1;
                recargaToken = 0;
                print("Dash Pronto");
            }
        }

    }

    IEnumerator DesacelerarDash(Vector3 direcao)
    {
        
        yield return new WaitForSeconds(.27f);
        rigbody.AddForce(direcao * -50000);
        collision.isTrigger = false;
    }

    
    IEnumerator rotacionarDireita(Quaternion rotNova, float duracao)
    {
        if (rotacionando)
        {
            yield break;
        }
        rotacionando = true;

        Quaternion rotAtual = transform.rotation;

        float contador = 0;

        while (contador < duracao)
        {
            contador += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(rotAtual, rotNova, contador / duracao);
            yield return null;
        }
        rotacionando = false;
    }
    IEnumerator rotacionarEsquerda(Quaternion rotNova, float duracao)
    {
        if (rotacionando)
        {
            yield break;
        }
        rotacionando = true;

        Quaternion rotAtual = transform.rotation;

        float contador = 0;
        while (contador < duracao)
        {
            contador += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(rotAtual, rotNova, contador / duracao);
            yield return null;
        }
        rotacionando = false;
    }
}
