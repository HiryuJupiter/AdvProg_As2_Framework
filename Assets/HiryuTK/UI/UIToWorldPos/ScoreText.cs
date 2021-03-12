using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace HiryuTK.UI.WorldPositionUIText
{
	public class ScoreText : RectFollowWorldBase, IUITextPoolable
	{
		const float UnfadedDuration = 1f;
		const float FadeSpeed = 2f;
		const float MoveSpeed = 100f;

		Text uiText;
		//Cache
		Color startingColor;
		UIScoreTextObjectPool pool;

		public void SetUp(UIScoreTextObjectPool pool, Canvas canvas)
		{
			base.SetUp(canvas);

			//Reference
			uiText = GetComponent<Text>();
			this.pool = pool;

			//Cache
			uiOffset = canvas.GetComponent<RectTransform>().sizeDelta * .5f;
			startingColor = uiText.color;
		}

		public void Activation(string text, Vector3 targetPosition)
		{
			//Set text attributes
			uiText.text = text;
			uiText.color = startingColor;

			//Set position

			MoveToTargetWorldPosition(targetPosition);
			StartCoroutine(Fade());
		}

		public IEnumerator Fade()
		{
			for (float i = 0; i < UnfadedDuration; i += Time.deltaTime)
			{
				MoveUp();
				yield return null;
			}

			Color c = startingColor;
			while (c.a > 0f)
			{
				MoveUp();

				//Fade out
				c.a -= FadeSpeed * Time.deltaTime;
				uiText.color = c;
				yield return null;
			}
			pool.Despawn(gameObject);
		}

		void MoveUp()
		{
			myRect.localPosition += Vector3.up * MoveSpeed * Time.deltaTime;
		}
	}
}

////Face camera
//transform.LookAt(transform.position + camera.rotation * Vector3.forward,
//camera.rotation * Vector3.up);