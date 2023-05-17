using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Survivors : MonoBehaviour
{

    public Rigidbody rigidbody;
    public float velocity;
    float x, y;
    Quaternion targetRotation;
    float time;
    float rotateTime;
    float rotate;
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
        rotate = Mathf.Atan2(x, y) * Mathf.Rad2Deg;
    }
	private void FixedUpdate()
	{
        Move();
        print(transform.rotation.y);
	}
	void Move() 
    {
        if (x!=0 || y!=0) 
        {
            rigidbody.velocity = transform.forward * velocity;
            Quaternion target = Quaternion.Euler(0, rotate, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, target, 5f);
            print(Mathf.Atan2(y, x));
        }
    }

}
