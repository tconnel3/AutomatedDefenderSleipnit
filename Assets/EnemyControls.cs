using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControls : MonoBehaviour
{
	public Rigidbody2D pComponent;
	public Vector2 velocity = new Vector2(6,6);
	public Rigidbody2D eSelf;
	public Vector2 position;
	public float xMove;
	public float yMove;
	public AudioClip destroyClip = null;
	public AudioSource eSource = null;
	
	public PlayerControls pControls;
	
    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Enemy";
		pComponent = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
		eSelf = GetComponent<Rigidbody2D>();
		
		eSource = GetComponent<AudioSource>();
		eSource.clip = destroyClip; 
		
    }

    // Update is called once per frame
    void Update()
    {
		
		if (eSelf.position.x < pComponent.position.x)
		{
			xMove = 1;
		}
		else
		{
			xMove = -1;
		}
		
		if (eSelf.position.y < pComponent.position.y)
		{
			yMove = 1;
		}
		else
		{
			yMove = -1;
		}

		Vector3 eMove = new Vector3(3 * xMove, 3 * yMove, 0);
		eMove *= Time.deltaTime;
		
		transform.Translate(eMove);
    }
	
	private void OnCollisionEnter2D(Collision2D collision)
	{
		
		if (collision.gameObject.tag == "Bullet")
		{
			
			pControls.nextLevel();
			pControls.damageSound();
			Destroy(collision.gameObject);
		
			Destroy(gameObject);
		}
		
		if (collision.gameObject.tag == "Player")
		{
			pControls.damageSound();
			pControls.nextLevel();
			pControls.takeDamage();			
			Destroy(gameObject);
		}
	}
	
}
