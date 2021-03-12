using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HiryuTK.TopDownController
{
	public class Module_Rotation: ModuleBase
	{
		const float AttackDuration = 0.5f;
		public Module_Rotation(PlayerTopDown3DController motor, PlayerFeedbacks feedback) : base(motor, feedback) { }

		public override void TickUpdate()
		{
			
		}
	}
}