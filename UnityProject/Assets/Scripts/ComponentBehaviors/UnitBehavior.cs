﻿using UnityEngine;
using System.Collections;
using Laborers;
using Laborers.Tasks;
using Laborers.Behaviors.Units;

public class UnitBehavior : MonoBehaviour
{
		protected WorkPlanTask _lastTask;
		protected WorkPlanTask _currentTask;
		protected UnitTaskAnimationType _lastAnimationType;
		protected UnitTaskAnimationType _currentAnimationType;
		public Unit Unit;

		protected GameObject _graphics;
		protected Animator _animator;
		public float StepSpeed = 0.01f;
		public float BuildForce = 0.2f;
		
		public float SoundInterval = 4;
		public float ElapsedSoundInterval = 4;
		public ToolType ToolType;

		public UnitBehavior ()
		{
				Unit = new Unit ();
		}
		// Use this for initialization
		void Start ()
		{
				_graphics = transform.GetChild (0).gameObject;
				_animator = _graphics.GetComponent<Animator> ();
				Unit.StepSpeed = StepSpeed;
				Unit.BaseBuildForce = BuildForce;
				Unit.Position = new Position (this.transform.position.x, this.transform.position.y, this.transform.position.z);
		}

		// Update is called once per frame
		void Update ()
		{
				BuildForce = Unit.GetBaseBuildForce ();
				Unit.ToolType = ToolType;
				Unit.Update ();
				transform.position = new Vector3 (Unit.Position.X, Unit.Position.Y, Unit.Position.Z);
				//Labels.Add("Unit position", Transform.Position.ToString());
				if (Unit.CurrentWorkPlan != null && Unit.CurrentWorkPlan.CurrentTask != null) {
						var task = Unit.CurrentWorkPlan.CurrentTask;
						if (task != _currentTask) {
								_lastTask = _currentTask;
								_currentTask = task;
								_lastAnimationType = _currentAnimationType;
								_currentAnimationType = task.AnimationType;
						}
				} else {
						_lastTask = _currentTask;
						_currentTask = null;
						_lastAnimationType = _currentAnimationType;
						_currentAnimationType = UnitTaskAnimationType.Idle;
				}
		
				if (_lastAnimationType != _currentAnimationType) {
						Debug.Log (string.Format ("Changing Animation. Last Anim: {0}. Curr Anim: {1}", _lastAnimationType, _currentAnimationType));
						_animator.SetInteger ("State", (int)_currentAnimationType);
						audio.Stop ();
						_lastAnimationType = _currentAnimationType;
				}
//        Material unitMaterial = transform.GetChild(0).renderer.material;
//
//        switch (_currentAnimationType)
//        {
//            case UnitTaskAnimationType.Idle:
//                unitMaterial.color = Color.blue;
//                break;
//            case UnitTaskAnimationType.Walking:
//                unitMaterial.color = Color.cyan;
//                break;
//            case UnitTaskAnimationType.Building:
//                unitMaterial.color = Color.red;
//                break;
				//        }
				switch (_currentAnimationType) {
				case UnitTaskAnimationType.Building:
						if (Time.time >= ElapsedSoundInterval) { // time to play?
								audio.Play (); // yes: play the sound...
								ElapsedSoundInterval += SoundInterval; // and define the 	next time
						}
						break;
				default:
						break;
				}
		}
}
