using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HiryuTK.TopDownController
{
	public class Module_Rotation: ModuleBase
	{
		float rotAmount;
		public Module_Rotation(PlayerTopDown3DController motor, PlayerFeedbacks feedback) : base(motor, feedback) { }

		public override void TickUpdate()
		{
			UpdateRotation();
		}

		void UpdateRotation()
		{
			//Assign rotation to gameObjecct
			//Quaternion rot = Quaternion.Euler(0f, 
			//GameInput_TopDownController.MoveX * settings.SteerSpeed * Time.deltaTime, 0f);
			rotAmount = Mathf.Lerp(rotAmount, GameInput_TopDownController.MoveX, settings.RotationAccleration * Time.deltaTime);
			status.rotation -= rotAmount * settings.RotationSpeed * Time.deltaTime;
			player.Rb.rotation = status.rotation;
		}
	}
}