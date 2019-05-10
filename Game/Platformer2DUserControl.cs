using UnityEngine;
//using UnitySampleAssets.CrossPlatformInput;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Green
{

    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D character;
        private bool jump;
		private float axisMovement = 0;	
		private float t = 0;
		public bool active = false;	
		public Sprite leftButtonActive;
		public Sprite rightButtonActive;
		public GameObject leftButton;
		public GameObject rightButton;
		private Rigidbody2D objRigidBody;

        private void Awake()
        {
            character = GetComponent<PlatformerCharacter2D>();
			objRigidBody = GetComponent<Rigidbody2D> ();
        }

        private void Update()
        {
            if(!jump)
            // Read the jump input in Update so button presses aren't missed.
            jump = Input.GetButtonDown("Jump");
        }

        private void FixedUpdate()
        {
            // Read the inputs.
            bool crouch = Input.GetKey(KeyCode.LeftControl);
            float h = Input.GetAxis("Horizontal");

			if(!transform.Find ("ActiveObject").gameObject.activeSelf){
				active = false;
				//	greenBall.GetComponent<SpriteRenderer>().sprite = inactiveSprite;
			}
			
			if(transform.Find ("ActiveObject").gameObject.activeSelf){
				active = true;
				//	greenBall.GetComponent<SpriteRenderer>().sprite = activeSprite;
			}
//----------------------------------------------------------------------------------------------------------
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)), Vector2.zero);
			if (Input.touchCount > 0) {
				
				var touch = Input.GetTouch(Input.touchCount - 1);
				
				switch (touch.phase) {
					
				case TouchPhase.Began:
					//	Debug.Log ("axisMovementBool Began: " + axisMovementBool);
					if(touch.position.x > (Screen.width/3)*2){
						if(active){
						t += Time.smoothDeltaTime;
						axisMovement = Mathf.Lerp( 0, 1, t);


							rightButton.GetComponent<Image>().overrideSprite = rightButtonActive;
							leftButton.GetComponent<Image>().overrideSprite = null;

						}
					}else if (touch.position.x < Screen.width/3){
						if(active){
						t += Time.smoothDeltaTime;
						axisMovement = -Mathf.Lerp( 0, 1, t);

							rightButton.GetComponent<Image>().overrideSprite = null;
							leftButton.GetComponent<Image>().overrideSprite = leftButtonActive;

							
						}
						
					}else if (touch.position.x > Screen.width/3 && touch.position.x < (Screen.width/3)*2){
						axisMovement = 0;
					}
					//	Debug.Log(EventSystem.current.);
					
					if(hit.collider != null){
						
						if(hit.collider.gameObject.tag == "ground_tree" || hit.collider.gameObject.tag == "blue"
						   || hit.collider.gameObject.tag == "stone" || hit.collider.gameObject.tag == "green"
						   || hit.collider.gameObject.tag == "UIElement" ){
							axisMovement = 0;
						//	axisMovementBool = true;
						}
					}
					
					break;
					
					
				case TouchPhase.Stationary:
					//------------------------------------------------------------------------------------------------------
				//	directionChosen = true;
					//	Debug.Log ("axisMovementBool Stationary: " + axisMovementBool);
					if(touch.position.x > (Screen.width/3)*2){
						
									
						
						if(active){
							t += Time.smoothDeltaTime;
							axisMovement = Mathf.Lerp( 0, 1, t);
							//		iTween.FadeTo(greenPlayer, iTween.Hash("alpha",0,"time",0.1f));
							//		iTween.FadeTo(greenBall, iTween.Hash("alpha",1,"time",0.1f));
							
							rightButton.GetComponent<Image>().overrideSprite = rightButtonActive;
							leftButton.GetComponent<Image>().overrideSprite = null;
							
						}
						
					}else if (touch.position.x < Screen.width/3){

						if(active){
							t += Time.smoothDeltaTime;
							axisMovement = -Mathf.Lerp( 0, 1, t);
							//		iTween.FadeTo(greenPlayer, iTween.Hash("alpha",0,"time",0.1f));
							//		iTween.FadeTo(greenBall, iTween.Hash("alpha",1,"time",0.1f));
							
							leftButton.GetComponent<Image>().overrideSprite = leftButtonActive;
							rightButton.GetComponent<Image>().overrideSprite = null;
							
						}
						
					}else if (touch.position.x > Screen.width/3 && touch.position.x < (Screen.width/3)*2){
						axisMovement = 0;
					}
					if(hit.collider != null){
						if(hit.collider.gameObject.tag == "ground_tree" || hit.collider.gameObject.tag == "blue"
						   || hit.collider.gameObject.tag == "stone"|| hit.collider.gameObject.tag == "green" || hit.collider.gameObject.tag == "UIElement"){
							axisMovement = 0;
						}
					}
					
					break;
					//----------------------------------------------------------------------------------------------------------
				case TouchPhase.Ended:
					
					rightButton.GetComponent<Image>().overrideSprite = null;
					leftButton.GetComponent<Image>().overrideSprite = null;
					axisMovement = 0;
				//	axisMovementBool = false;
					t=0;
					if(active){
						//		iTween.FadeTo(greenPlayer, iTween.Hash("alpha",1,"time",0.1f));
						//		iTween.FadeTo(greenBall, iTween.Hash("alpha",0,"time",0.1f));
					}
					if( touch.position.x > Screen.width/3 && touch.position.x < (Screen.width/3)*2 && active){
						//	Debug.Log ("fire");
						axisMovement = 0;
					
					}
					if(hit.collider != null){
					
						
					}
					break;
					
				}
				//-----------------------------------------------------------------------------------------------------------
				
			}
//----------------------------------------------------------------------------------------------------------

            // Pass all parameters to the character control script.
            character.Move(axisMovement, crouch, jump);
            jump = false;
        }
    }
}