using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float speed;
	public Text countText;
	public Text winText;
	public Text timeText;

	private Rigidbody rb;
	private int count;
	private int numberPickUps;
	private float startTime;
	private GameObject btn;
	private Button target;

	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
		count = 0;
		numberPickUps = GetNumberOfPickUps ();
		SetCountText ();
		winText.text = "";
		startTime = Time.time;

		btn = GameObject.FindGameObjectWithTag ("Next Level Button");
		target = btn.GetComponent<Button> ();
		target.interactable = false;
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);


		rb.AddForce (movement * speed);

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("Pick Up")) 
		{
			other.gameObject.SetActive (false);
			count = count + 1;
			SetCountText ();
		}
	}

	void SetCountText()
	{
		countText.text = "Count: " + count.ToString ();
		if (count >= numberPickUps) 
		{
			winText.text = "You Win!";
			SetTimeText ();
		}
	}

	int GetNumberOfPickUps()
	{
		int num = 0;
		num = GameObject.FindGameObjectsWithTag ("Pick Up").Length;
		return num;
	}

	void SetTimeText()
	{
		float guiTime = Time.time - startTime;
		timeText.text = "Completed in " + guiTime.ToString () + " seconds";
	}
		
}