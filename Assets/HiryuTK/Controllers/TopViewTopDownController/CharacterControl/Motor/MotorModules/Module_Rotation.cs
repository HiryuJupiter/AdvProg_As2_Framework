using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HiryuTK.TopDownController
{
	public class Module_Rotation: ModuleBase
	{
		public Module_Rotation(PlayerTopDown3DController motor, PlayerFeedbacks feedback) : base(motor, feedback) { }

		public override void TickUpdate()
		{
			UpdateRotation();
		}

		void UpdateRotation()
		{
			//Assign rotation to gameObjecct
			Quaternion rot = Quaternion.Euler(0f, 0f, 
				GameInput_TopDownController.MoveX * settings.SteerSpeed * Time.deltaTime);
			player.Rb.rotation = rot;
		}
	}
}