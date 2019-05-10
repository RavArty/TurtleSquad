using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class WinDialog : MonoBehaviour
{
		/// <summary>
		/// Number of stars for the WinDialog.
		/// </summary>
		public TableLevel.StarsNumber starsNumber = TableLevel.StarsNumber.ZERO;

		/// <summary>
		/// Star sound effect.
		/// </summary>
		public AudioClip starSoundEffect;

		/// <summary>
		/// Win dialog animator.
		/// </summary>
		public Animator winDialogAnimator;

		/// <summary>
		/// First star fading animator.
		/// </summary>
		public Animator firstStarFading;

		/// <summary>
		/// Second star fading animator.
		/// </summary>
		public Animator secondStarFading;

		/// <summary>
		/// Third star fading animator.
		/// </summary>
		public Animator thirdStarFading;

		public Text totalProgress;
		private int  curValue;
		public float duration = 2;

		private bool activeFade = false;

		// Use this for initialization
		void Awake ()
		{

	//	Debug.Log ("start animator");
				///Setting up the references
				if (winDialogAnimator == null) {
						winDialogAnimator = GetComponent<Animator> ();
		//	Debug.Log ("found animator");
				}

				if (firstStarFading == null) {
						firstStarFading = GameObject.Find ("FirstStarFading").GetComponent<Animator> ();
				}

				if (secondStarFading == null) {
						secondStarFading = GameObject.Find ("SecondStarFading").GetComponent<Animator> ();
				}

				if (thirdStarFading == null) {
						thirdStarFading = GameObject.Find ("ThirdStarFading").GetComponent<Animator> ();
				}

		#if UNITY_IOS
		System.Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
		#endif
		}

	void Start(){
	//	lastInterval = Time.realtimeSinceStartup;
	}
	 
		/// <summary>
		/// When the GameObject becomes visible
		/// </summary>
		void OnEnable ()
		{
				///Hide the Win Dialog
				Hide ();
		}

		/// <summary>
		/// Show the Win Dialog.
		/// </summary>
		public void Show ()
		{
//		Debug.Log ("Show dialog");
				if (winDialogAnimator == null) {
						return;
				}
	//	winDialogAnimator.SetBool ("Running", true);
		winDialogAnimator.SetTrigger ("Running");

		}

		/// <summary>
		/// Hide the Win Dialog.
		/// </summary>
		public void Hide ()
		{
				StopAllCoroutines ();
				winDialogAnimator.SetBool ("Running", false);
		//		firstStarFading.SetBool ("Running", false);
		//		secondStarFading.SetBool ("Running", false);
		//		thirdStarFading.SetBool ("Running", false);
	
		}

		void Update(){
		if(activeFade){
	//		StartCoroutine(FadeStars2());
		}
	//		lastInterval = Time.realtimeSinceStartup;
	//	Debug.Log ("Time.realtimeSinceStartup: " + Time.realtimeSinceStartup);
		}
		
		/// <summary>
		/// Fade stars Coroutine.
		/// </summary>
		/// <returns>The stars.</returns>
	//	public IEnumerator FadeStars2 ()
	//	{
	//		activeFade = true;
	//
	//	}
	public void fireCountTo(int current, int target){
		StartCoroutine (CountToFrom (current, target));
	}

	IEnumerator CountToFrom(int current, int target){
	//	Debug.Log ("started fun");
		for (float timer = 0; timer < duration; timer += Time.unscaledDeltaTime) {
			float progress = timer / duration;
			curValue = (int)Mathf.Lerp (current, target, progress);
			totalProgress.text = curValue + "";
			yield return null;

		}
//		Debug.Log ("started fun 2");
		curValue = TotalData.totalData.totalCoins;
		totalProgress.text = curValue + "";
	}

	public IEnumerator CountTo(){
//		Debug.Log ("started fun");
		for (float timer = 0; timer < duration; timer += Time.unscaledDeltaTime) {
			float progress = timer / duration;
			curValue = (int)Mathf.Lerp (0, TotalData.totalData.totalCoins, progress);
			totalProgress.text = curValue + "";
			yield return null;

		}
	//	Debug.Log ("started fun 2");
		curValue = TotalData.totalData.totalCoins;
		totalProgress.text = curValue + "";
	}

	public IEnumerator FadeStars ()
	{
		Debug.Log ("Fade starts executed");

			float delayBetweenStars = 0.5f;
			if (starsNumber == TableLevel.StarsNumber.ONE) {//Fade with One Star
				MusicSound.instance.audioSources[2].clip = starSoundEffect;
				MusicSound.instance.audioSources[2].Play();
			//	AudioSource.PlayClipAtPoint (starSoundEffect, Vector3.zero, MusicSound.instance.audioSources [1].volume);
				firstStarFading.SetTrigger ("Running");
			} else if (starsNumber == TableLevel.StarsNumber.TWO) {//Fade with Two Star
				MusicSound.instance.audioSources[2].clip = starSoundEffect;
				MusicSound.instance.audioSources[2].Play();
			//	AudioSource.PlayClipAtPoint (starSoundEffect, Vector3.zero, MusicSound.instance.audioSources [1].volume);
				firstStarFading.SetTrigger ("Running");
		//	Debug.Log ("first star fading");
				yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(delayBetweenStars));
			//	yield return new WaitForSeconds (delayBetweenStars);
				MusicSound.instance.audioSources[2].clip = starSoundEffect;
				MusicSound.instance.audioSources[2].Play();
			//	AudioSource.PlayClipAtPoint (starSoundEffect, Vector3.zero, MusicSound.instance.audioSources [1].volume);
				secondStarFading.SetTrigger ("Running");
			} else if (starsNumber == TableLevel.StarsNumber.THREE) {//Fade with Three Star
				MusicSound.instance.audioSources[2].clip = starSoundEffect;
				MusicSound.instance.audioSources[2].Play();
				Debug.Log ("Fade starts executed 3");
			//	AudioSource.PlayClipAtPoint (starSoundEffect, Vector3.zero, MusicSound.instance.audioSources [1].volume);
				firstStarFading.SetTrigger ("Running");
	//		firstStarFading.SetBool ("Running", true);
				Debug.Log ("Fade starts executed 3.1");
			yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(delayBetweenStars));
			//	yield return new WaitForSeconds (delayBetweenStars);
				MusicSound.instance.audioSources[2].clip = starSoundEffect;
				MusicSound.instance.audioSources[2].Play();
			//	AudioSource.PlayClipAtPoint (starSoundEffect, Vector3.zero, MusicSound.instance.audioSources [1].volume);
				secondStarFading.SetTrigger ("Running");
	//		secondStarFading.SetBool ("Running", true);
			yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(delayBetweenStars));
			//	yield return new WaitForSeconds (delayBetweenStars);
				MusicSound.instance.audioSources[2].clip = starSoundEffect;
				MusicSound.instance.audioSources[2].Play();
			//	AudioSource.PlayClipAtPoint (starSoundEffect, Vector3.zero, MusicSound.instance.audioSources [1].volume);
				thirdStarFading.SetTrigger ("Running");
			Debug.Log ("Fade starts executed end");
			}
		
			yield return 0;
	
			

	}
	
}