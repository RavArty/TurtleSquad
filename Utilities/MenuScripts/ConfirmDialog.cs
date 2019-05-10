using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
public class ConfirmDialog : MonoBehaviour
{
		/// <summary>
		/// The animator of the confirm dialog.
		/// </summary>
		public Animator animator;

		/// <summary>
		/// The black area animator.
		/// </summary>
		public Animator blackAreaAnimator;

		void Start ()
		{
				if (animator == null) {
						animator = GetComponent<Animator> ();
				}

				if (blackAreaAnimator == null) {
						blackAreaAnimator = GameObject.Find ("BlackArea").GetComponent<Animator> ();
				}
		}

		/// <summary>
		/// Show the dialog.
		/// </summary>
		public void Show ()
		{
				blackAreaAnimator.SetTrigger ("Running");
				animator.SetBool ("Off", false);
				animator.SetTrigger ("On");
		}

		/// <summary>
		/// Hide the dialog.
		/// </summary>
		public void Hide ()
		{
				blackAreaAnimator.SetBool ("Running", false);
				animator.SetBool ("On", false);
				animator.SetTrigger ("Off");
		}

		/// <summary>
		/// Reset the animation parameters.
		/// </summary>
		private void ResetAnimationParameters ()
		{
				if (animator == null) {
						return;
				}
				animator.SetBool ("On", false);
				animator.SetBool ("Off", false);
		}
}