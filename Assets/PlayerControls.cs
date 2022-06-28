using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControls : MonoBehaviour
{
    // Start is called before the first frame update
	public Rigidbody2D bSelf;
	public GameObject BulletPlaceholder;
	public bool allowFire = true;
	public int enemyCount = 0;
	public int playerHealth = 3;
	public int currLevel = 0;
	public AudioClip fireClip = null;
	public AudioClip destroyClip = null;
	public AudioSource aSource = null;
	public AudioSource eSource = null;
	
    void Start()
    {
        gameObject.tag = "Player";
		bSelf = GetComponent<Rigidbody2D>();
		enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
		
		if (enemyCount == 4)
		{
			currLevel = 1;
		}
		else
		{
			currLevel = 2;
		}
		
		
		AudioSource[] gameClips = GetComponents<AudioSource>();
		aSource = gameClips[0];
		aSource.clip = fireClip; 
		
		eSource = gameClips[1];
		eSource.clip = destroyClip;

    }
	
	public Vector2 velocity = new Vector2(10,10);
    // Update is called once per frame
    void Update()
    {
        float xIn = Input.GetAxis("Horizontal");
		float yIn = Input.GetAxis("Vertical");
		
		Vector3 move = new Vector3(velocity.x * xIn, velocity.y * yIn, 0);
		move *= Time.deltaTime;
		
		transform.Translate(move);
		
		
		
		//Fire weapon
		//Each if statement handles one of the 8 directions and insures the weapon is only fired in that direction
		//The co-routine is a timer to prevent the player from being able to fire their weapon every frame
		if(Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.DownArrow) && allowFire == true)
		{
			Quaternion bulletDirection = new Quaternion();
			bulletDirection.Set(0, 0, 0, 1);
			Instantiate(BulletPlaceholder, transform.position, bulletDirection);
			aSource.Play();
			StartCoroutine(fireWait());
		}
		if(Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.DownArrow) && allowFire == true)
		{
			Vector3 bulletRotation;
			Quaternion bulletDirection = new Quaternion();
			bulletRotation = new Vector3(0,0,315);
			bulletDirection.eulerAngles = bulletRotation;
			Instantiate(BulletPlaceholder, transform.position, bulletDirection);
			aSource.Play();
			StartCoroutine(fireWait());
		}	
		if(Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && allowFire == true)
		{
			Vector3 bulletRotation;
			Quaternion bulletDirection = new Quaternion();
			bulletRotation = new Vector3(0,0,270);
			bulletDirection.eulerAngles = bulletRotation;
			Instantiate(BulletPlaceholder, transform.position, bulletDirection);
			aSource.Play();
			StartCoroutine(fireWait());
		}
		if(Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.DownArrow) && allowFire == true)
		{
			Vector3 bulletRotation;
			Quaternion bulletDirection = new Quaternion();
			bulletRotation = new Vector3(0,0,225);
			bulletDirection.eulerAngles = bulletRotation;
			Instantiate(BulletPlaceholder, transform.position, bulletDirection);
			aSource.Play();
			StartCoroutine(fireWait());
		}
		if(Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.UpArrow) && allowFire == true)
		{
			Vector3 bulletRotation;
			Quaternion bulletDirection = new Quaternion();
			bulletRotation = new Vector3(0,0,180);
			bulletDirection.eulerAngles = bulletRotation;
			Instantiate(BulletPlaceholder, transform.position, bulletDirection);
			aSource.Play();
			StartCoroutine(fireWait());
		}
		if(!Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.DownArrow) && allowFire == true)
		{
			Vector3 bulletRotation;
			Quaternion bulletDirection = new Quaternion();
			bulletRotation = new Vector3(0,0,135);
			bulletDirection.eulerAngles = bulletRotation;
			Instantiate(BulletPlaceholder, transform.position, bulletDirection);
			aSource.Play();
			StartCoroutine(fireWait());
		}
		if(Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.DownArrow) && allowFire == true)
		{
			Vector3 bulletRotation;
			Quaternion bulletDirection = new Quaternion();
			bulletRotation = new Vector3(0,0,90);
			bulletDirection.eulerAngles = bulletRotation;
			Instantiate(BulletPlaceholder, transform.position, bulletDirection);
			aSource.Play();
			StartCoroutine(fireWait());
		}
		if(!Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.DownArrow) && allowFire == true)
		{
			Vector3 bulletRotation;
			Quaternion bulletDirection = new Quaternion();
			bulletRotation = new Vector3(0,0,45);
			bulletDirection.eulerAngles = bulletRotation;
			Instantiate(BulletPlaceholder, transform.position, bulletDirection);
			aSource.Play();
			StartCoroutine(fireWait());
		}
		
		
		//Create a half second delay before the player can fire again
		IEnumerator fireWait()
		{
			allowFire = false;
			yield return new WaitForSeconds(0.5f);
			allowFire = true;
		}
		
    }
	
	public void nextLevel()
	{
		//This is called by EnemyControls when they are destroyed
		
		enemyCount--;
		if (enemyCount == 0 && playerHealth > 0)
		{
			SceneManager.LoadScene("LevelTwo");
		}
		if (enemyCount == -33 && playerHealth > 0)
		{
			SceneManager.LoadScene("WinScreen");
		}
	}
	
	public void takeDamage()
	{
		//Debug.Log(playerHealth);
		playerHealth--;
		if (playerHealth == 0)
		{
			SceneManager.LoadScene("GameOver");
		}
	}
	
	public void damageSound()
	{
		//eSource.Play();
	}
}











