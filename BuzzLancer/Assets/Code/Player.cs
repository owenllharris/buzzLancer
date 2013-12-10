using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Code
{
	public class Player : MonoBehaviour 
	{
		public Camera camera;

		public Destroyable destroyable;
		public BasicWeapon basicWeapon;

		private PlayerCamera _camera;
		private PlayerController _controller;
		private PlayerGUI _playerGUI;
		private PlayerWeapons _weapons;

		private IEnumerable<BasicWeaponMount> _mounts;

		public float Health { get { return destroyable.Health; } }
		public float MaxHealth { get { return destroyable.MaxHealth; } }

		public void Awake()
		{
			_mounts = GetComponentsInChildren<BasicWeaponMount>();

			_camera = new PlayerCamera( this, camera );
			_controller = new PlayerController( this );
			_playerGUI = new PlayerGUI( this, _controller );
			_weapons = new PlayerWeapons( this, camera, _controller, _mounts );

			Equip( basicWeapon );
		}

		public void Equip( BasicWeapon weapon )
		{
			foreach ( var mount in _mounts )
				mount.Equip( weapon );
		}

		public void Update()
		{
			_controller.Update();
			_playerGUI.Update();
			_camera.Update();
			_weapons.Update();
		}

		public void OnGUI()
		{
			_playerGUI.OnGUI();
		}
	}
}
