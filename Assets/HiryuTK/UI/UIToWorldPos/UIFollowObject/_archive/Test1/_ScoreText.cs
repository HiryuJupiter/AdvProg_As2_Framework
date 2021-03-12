//using System.Collections;
//using UnityEngine;
//using UnityEngine.UI;

//public class _ScoreText : MonoBehaviour
//{
//	const float UnfadedDuration = 5.5f;
//	const float FadeSpeed = 1f;
//	const float MoveSpeed = 1f;

//	Text uiText;
//	RectTransform rectTrans;

//	//Cache
//	Color startingColor;
//	Vector2 canvasSize;
//	Vector2 uiOffset;

//	public IEnumerator Play (string text, RectTransform canvasRect, Vector2 targetPosition)
//	{
//		//Reference
//		rectTrans = GetComponent<RectTransform>();
//		uiText = GetComponent<Text>();

//		//Cache
//		canvasSize = new Vector2(canvasRect.sizeDelta.x, canvasRect.sizeDelta.y);
//		uiOffset = new Vector2(canvasSize.x * 0.5f, canvasSize.y * 0.5f);
//		startingColor = uiText.color;

//		//Initialize
//		uiText.text = text;

//		Vector2 viewPos = Camera.main.WorldToViewportPoint(targetPosition);
//		Vector2 screenPos = new Vector2(viewPos.x * canvasSize.x - uiOffset.x,
//			viewPos.y * canvasSize.y - uiOffset.y);

//		rectTrans.localPosition = screenPos;

//        float a = 1f;

//        for (float i = 0; i < UnfadedDuration; i += Time.deltaTime)
//        {
//			MoveUp();
//			yield return null;
//        }

//		while (a > 0f)
//        {
//			MoveUp();

//			//Fade out
//			a -= FadeSpeed * Time.deltaTime;
//			startingColor.a = a;
//			uiText.color = startingColor;
//			yield return null;
//		}
//		Destroy(gameObject);
//	}

//	void MoveUp ()
//    {
//		//uiText.rectTransform.position += Vector3.up * MoveSpeed * Time.deltaTime;
//	}
//}

//////Face camera
////transform.LookAt(transform.position + camera.rotation * Vector3.forward,
////camera.rotation * Vector3.up);