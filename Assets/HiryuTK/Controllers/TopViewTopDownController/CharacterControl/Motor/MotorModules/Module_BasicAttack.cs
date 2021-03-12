using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HiryuTK.TopDownController
{
	public class Module_BasicAttack : ModuleBase
	{
		const float AttackDuration = 0.5f;
		public Module_BasicAttack(PlayerTopDown3DController motor, PlayerFeedbacks feedback) : base(motor, feedback) { }

		public override void TickUpdate()
		{
			if (Input.GetMouseButtonDown(0) && !CursorManager.IsMouseOverUI)
			{
				//feedback.Animator.PlayAttack();
				status.SetAttackAnimationTimer(player, AttackDuration);
			}
		}
	}
}