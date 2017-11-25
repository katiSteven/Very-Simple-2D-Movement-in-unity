using UnityEngine;
using System.Collections;
using System.Text;

public class OutputGuy : MonoBehaviour {


	public float Speed = 0;
	public float Jump_Force_yAxis = 0;

	private bool isJumping = false;
	private bool foundFooting = false;
	private bool pulledDown = true;
	private Rigidbody2D rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}
	// adds force to jump upwards
	void GoUp(){
		print("going up");
		rb.AddForce(new Vector2(0,Jump_Force_yAxis+12.5f), ForceMode2D.Impulse);
		isJumping = false;
		pulledDown = true;
	}

	// Constant gravity to pull down the player when NOT in contact with the collider of the 'platform GameObject'
	void goDown(){
		print("going down");
		rb.AddForce(new Vector2(0,-1), ForceMode2D.Impulse);
		isJumping = false;
		pulledDown = true;
	}

	//checking if the object is in contact with the 'platform GameObject'
	void OnCollisionStay2D(Collision2D col){
		GameObject obj = col.gameObject;
		if (obj.GetComponent<BoxCollider2D> ().transform.position.y <= this.gameObject.transform.position.y) {
			if (obj.GetComponent<Platform> ()) {
				foundFooting = true;
				pulledDown = false;
				return;
			} else {
				foundFooting = false;
				pulledDown = true;
			}
		} else {
			return;
		}
	}

	void OnCollisionExit2D(){
		foundFooting = false;
		pulledDown = true;
	}

	// Update is called once per frame
	void Update (){

		// Enabling Jump bool when Space is pressed
		if (Input.GetKeyDown (KeyCode.Space)) {
			isJumping = true;
		} else {
			isJumping = false;
		}

		//Enabling OR disabling jump bool conditions
		if(isJumping && foundFooting && !pulledDown){
			GoUp();
		}
		//Enabling OR disabling gravity bool conditions
		if(!isJumping && !foundFooting && pulledDown){

			goDown();
		}

		//Right movement thorough Right arrow key
		if (Input.GetKey (KeyCode.RightArrow)) {
			Vector3 vehicleposition = new Vector3 (transform.position.x * Time.deltaTime, transform.position.y, transform.position.z);
			vehicleposition.x = transform.position.x + Speed / 100;
			transform.position = vehicleposition;

		}
		
		//Right movement thorough Left arrow key
		if (Input.GetKey (KeyCode.LeftArrow)) {
			Vector3 vehicleposition = new Vector3 (transform.position.x * Time.deltaTime, transform.position.y, transform.position.z);
			vehicleposition.x = transform.position.x - Speed / 100;
			transform.position = vehicleposition;
		}
	}
}