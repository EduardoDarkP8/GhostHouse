using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Survivors : MonoBehaviour
{

    public Rigidbody rigidbody;
    public float velocity;
    float x, y;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
    }
	private void FixedUpdate()
	{
        Move();
        Rotate();
	}
	void Move() 
    {
            rigidbody.velocity = transform.forward * velocity * y;
    }
    void Rotate() 
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0,transform.rotation.y+90*x,0),2f);;
    }

}
