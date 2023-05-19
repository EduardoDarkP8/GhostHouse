using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghosts : MonoBehaviour
{
    public float velocidade = 5;
    int tokenDash = 1;
    float moverZ, recargaToken, tempoRecarga = 2.5f;
    Rigidbody rigbody;
    Collider collision;
    float x, y;
    float rotate;
    void Start()
    {
        rigbody = GetComponent<Rigidbody>();
        collision = GetComponent<Collider>();
    }

    void Update()
    {


        if (Input.GetButtonDown("Jump"))
        {
            if(tokenDash == 1)
            {
                rigbody.velocity.Equals(0);
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
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        rotate = Mathf.Atan2(x, y) * Mathf.Rad2Deg;
    }
	private void FixedUpdate()
	{
        Move();
	}
	void Move()
    {
        if (x != 0 || y != 0)
        {
            rigbody.velocity = transform.forward * velocidade;
            Quaternion target = Quaternion.Euler(0, rotate, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, target, 5f);

        }
        else
        {
            rigbody.velocity.Equals(0);
        }
    }
    IEnumerator DesacelerarDash(Vector3 direcao)
    {
        
        yield return new WaitForSeconds(.27f);
        rigbody.AddForce(direcao * -50000);
        collision.isTrigger = false;
    }

    
    
    IEnumerator DiminuirVelocidade() 
    {
        velocidade /= 4;
        yield return new WaitForSeconds(2);
        velocidade *= 4;
    }
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag ==  "Sal") 
        {
            Destroy(other.gameObject);
            StartCoroutine(DiminuirVelocidade());
        }
	}
}
