using UnityEngine;

namespace Assets.Code
{
	public class PlayerCamera
	{
		private readonly Player _player;
		private readonly Camera _camera;

		public float MovmentDamp { get; set; }

		public PlayerCamera ( Player player, Camera camera)
		{
			MovmentDamp = 8;

			_player = player;
			_camera = camera;
		}

		public void Update()
		{
			var position = _player.transform.TransformPoint( 0, 0.5f, -5 );
			_camera.transform.position = Vector3.Lerp(
				_camera.transform.position, 
				position, 
				Time.deltaTime * MovmentDamp );

			_camera.transform.LookAt( _player.transform.TransformPoint( 0, 0, 50 ), _player.transform.up );

		}
	}
}

