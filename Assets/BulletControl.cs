using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Bullet";
    }

    // Update is called once per frame
    void Update()
    {
		transform.Translate(Vector3.up * 15 * Time.deltaTime);
    }
	
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Enemy")
		{
			Destroy(collision.gameObject);
		}
		
		if (collision.gameObject.tag == "Wall")
		{
			Destroy(gameObject);
		}
	}	
}
