using UnityEngine; 
using System.Collections; 


public class LionMovement : MonoBehaviour { 
	//reference to the wheel joints
	WheelJoint2D[] wheelJoints; 
	//center of mass of the car
    //public Transform centerOfMass;
	//reference tot he motor joint
	 JointMotor2D motorBack;  
	 JointMotor2D motorBack2;   
	//horizontal movement keyboard input
	float dir = 0f; 
	//input for rotation of the car
	float torqueDir = 0f;
	//max fwd speed which the car can move at
	float maxFwdSpeed = -300;
	//max bwd speed
	float maxBwdSpeed = 300f;
	//the rate at which the car accelerates
	float accelerationRate = 500;
	//the rate at which car decelerates
	float decelerationRate = -100;
	//how soon the car stops on braking
	float brakeSpeed = 2500f;
	//acceleration due to gravity
	float gravity = 9.81f;
	//angle in which the car is at wrt the ground
	float slope = 0;
	//reference to the wheels
	public Transform rearWheel;
	public Transform frontWheel;
	private float t = 0;	
	public float axisMovement = 0;
	private float timeSpeed = 2;
	// Use this for initialization 
	void Start () { 
		
		//get the wheeljoint components
		wheelJoints = gameObject.GetComponents<WheelJoint2D>(); 
		motorBack = wheelJoints[0].motor; 
		motorBack2 = wheelJoints[1].motor; 
	}  

	//all physics based assignment done here
	void FixedUpdate(){
		
		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)), Vector2.zero);


		if (Input.touchCount > 0) {

			var touch = Input.GetTouch(Input.touchCount - 1);

			switch (touch.phase) {

			case TouchPhase.Stationary:



				if(touch.position.x > (Screen.width/2)){
					t += Time.smoothDeltaTime*timeSpeed;
					dir = Mathf.Lerp( 0, 1, t);

				}else if (touch.position.x < Screen.width/2){
					t += Time.smoothDeltaTime*timeSpeed;
					dir = -Mathf.Lerp( 0, 1, t);

				}


				break;



			//----------------------------------------------------------------------------------------------------------
			case TouchPhase.Ended:

				dir = 0;

				t=0;

				break;

			}
		}
		

		//determine the cars angle wrt the horizontal ground
		slope = transform.localEulerAngles.z;

		//convert the slope values greater than 180 to a negative value so as to add motor speed 
		//based on the slope angle
		if(slope>=180)
			slope = slope - 360;
		//horizontal movement input. same as torqueDir. Could have avoided it, but decided to 
		//use it since some of you might want to use the Vertical axis for the torqueDir
	    //	dir = Input.GetAxis("Horizontal"); 

		//explained in the post in detail
		//check if there is any input from the user
		if(dir!=0)
			//add speed accordingly
			motorBack2.motorSpeed = Mathf.Clamp(motorBack2.motorSpeed -(dir*accelerationRate - gravity*Mathf.Sin((slope * Mathf.PI)/180)*80 )*Time.deltaTime, maxFwdSpeed, maxBwdSpeed);

			motorBack.motorSpeed = Mathf.Clamp(motorBack.motorSpeed -(dir*accelerationRate - gravity*Mathf.Sin((slope * Mathf.PI)/180)*80 )*Time.deltaTime, maxFwdSpeed, maxBwdSpeed);
		if((dir==0 && motorBack.motorSpeed < 0 ) ||(dir==0 && motorBack.motorSpeed==0 && slope < 0)){
			//decelerate the car while adding the speed if the car is on an inclined plane
			motorBack.motorSpeed = Mathf.Clamp(motorBack.motorSpeed - (decelerationRate - gravity*Mathf.Sin((slope * Mathf.PI)/180)*80)*Time.deltaTime, maxFwdSpeed, 0);
			motorBack2.motorSpeed = Mathf.Clamp(motorBack2.motorSpeed - (decelerationRate - gravity*Mathf.Sin((slope * Mathf.PI)/180)*80)*Time.deltaTime, maxFwdSpeed, 0);
			transform.GetComponent<Rigidbody2D>().velocity = new Vector2(0,transform.GetComponent<Rigidbody2D>().velocity.y);
		}
		//if no input and car is moving backward or no input and car is stagnant and is on an inclined plane with positive slope
		else if((dir==0 && motorBack.motorSpeed > 0 )||(dir==0 && motorBack.motorSpeed==0 && slope > 0)){
			//decelerate the car while adding the speed if the car is on an inclined plane
			motorBack.motorSpeed = Mathf.Clamp(motorBack.motorSpeed -(-decelerationRate - gravity*Mathf.Sin((slope * Mathf.PI)/180)*80)*Time.deltaTime, 0, maxBwdSpeed);
			motorBack2.motorSpeed = Mathf.Clamp(motorBack2.motorSpeed -(-decelerationRate - gravity*Mathf.Sin((slope * Mathf.PI)/180)*80)*Time.deltaTime, 0, maxBwdSpeed);
			transform.GetComponent<Rigidbody2D>().velocity = new Vector2(0,transform.GetComponent<Rigidbody2D>().velocity.y);

		}



		//apply brakes to the car
		if (Input.GetKey(KeyCode.Space) && motorBack.motorSpeed > 0){
			motorBack.motorSpeed = Mathf.Clamp(motorBack.motorSpeed - brakeSpeed*Time.deltaTime, 0, maxBwdSpeed); 
		}
		else if(Input.GetKey(KeyCode.Space) && motorBack.motorSpeed < 0){ 
			motorBack.motorSpeed = Mathf.Clamp(motorBack.motorSpeed + brakeSpeed*Time.deltaTime, maxFwdSpeed, 0);
		}
		//connect the motor to the joint
		wheelJoints[0].motor = motorBack; 
		wheelJoints[1].motor = motorBack; 

	}

}