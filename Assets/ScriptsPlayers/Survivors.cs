using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Survivors : MonoBehaviour
{

    Rigidbody rigbody;
    public float velocity;
    float x, y;
    Quaternion targetRotation;
    float time;
    float rotateTime;
    float rotate;
    bool inArmario;
    float delay = 1.5f;
    float timeOut;
    float saltDelay = 1.5f;
    float saltTimeOut;
    public GameObject salt;
    public Transform local;
    // Start is called before the first frame update
    void Start()
    {
        rigbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        rotate = Mathf.Atan2(x, y) * Mathf.Rad2Deg;
		if (timeOut > delay && Input.GetButtonUp("Fire1")) 
        {
            LeftDoArmario();
            timeOut = 0;
        }
		if (inArmario) 
        {
            timeOut += Time.deltaTime;
        }
		if (saltTimeOut > saltDelay && Input.GetButtonDown("Fire2")) 
        {
                Salt();   
        }
        saltTimeOut += Time.deltaTime;

    }
	private void FixedUpdate()
	{
        Move();
        
	}
	void Move() 
    {
        if (x!=0 || y!=0) 
        {
            rigbody.velocity = transform.forward * velocity;
            Quaternion target = Quaternion.Euler(0, rotate, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, target, 5f);

        }
		else 
        {
            velocity.Equals(0);
        }
    }
    void Salt() 
    {
        Instantiate(salt, local.position, local.rotation);
        saltTimeOut = 0;
    }
    void EntreArmario(GameObject gm) 
    {
        if (!inArmario) {
            gameObject.GetComponent<Collider>().enabled = false;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            transform.position = gm.transform.position;
            transform.rotation = gm.transform.rotation;
            inArmario = true;
        }
    }
    void LeftDoArmario() 
    {
		if (inArmario) 
        {
            gameObject.GetComponent<Collider>().enabled = true;
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            transform.position = transform.position + transform.forward*2;
            transform.rotation = transform.rotation;
            inArmario = false;

        }
    }
	private void OnTriggerStay(Collider other)
	{

        if (other.gameObject.tag == "Armario" && Input.GetButtonUp("Fire1")) 
        {
            EntreArmario(other.gameObject);
        }
	}
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Armario" && Input.GetButtonUp("Fire1"))
        {
            EntreArmario(other.gameObject);
        }
    }
}
