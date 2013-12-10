using UnityEngine;
using System.Collections;

namespace Assets.Code
{
	public class Player : MonoBehaviour 
	{
		public Camera camera;

		public Destroyable destroyable;

		private PlayerCamera _camera;
		private PlayerController _controller;
		private PlayerGUI _playerGUI;

		public float Health 
		{
			get { return destroyable.Health; }
		}

		public float MaxHealth
		{
			get { return destroyable.MaxHealth; }
		}

		public void Awake()
		{
			_camera = new PlayerCamera( this, camera );
			_controller = new PlayerController( this );
			_playerGUI = new PlayerGUI( this, _controller );
		}

		public void Update()
		{
			_controller.Update();
			_playerGUI.Update();
			_camera.Update();
		}

		public void OnGUI()
		{
			_playerGUI.OnGUI();
		}
	}
}
