using UnityEngine;

namespace Assets.Code
{
	public class PlayerGUI
	{
		private readonly PlayerController _controller;
		private readonly Player _player;

		private readonly ProgressBar _velocityProgressBar;
		private readonly ProgressBar _healthProgressbar;

		public float CurrentCursorSize { get; private set; }

		public PlayerGUI ( Player player, PlayerController controller )
		{
			_controller = controller;
			_player = player;

			_velocityProgressBar = new ProgressBar
			{
				Size = new Vector2( 250, 10 ),
				Position = new Vector2( 10, Screen.height - 10 - 10 ),
				BackgroundColour = new Color( 199 / 255f, 231 / 255f, 255 / 255f ), 
				ForegroundColour = new Color( 0 / 255f, 145 / 255f, 255 / 255f )
			};

			_healthProgressbar = new ProgressBar
			{
				Size = _velocityProgressBar.Size,
				Position = new Vector2( _velocityProgressBar.Position.x, _velocityProgressBar.Position.y - _velocityProgressBar.Size.y - 10 ),
				BackgroundColour = new Color( 255 / 255f, 199 / 255f, 208 / 255f), 
				ForegroundColour = new Color( 192 / 255f, 62 / 255f , 62 / 255f )
			};

			CurrentCursorSize = 20;
		}

		public void Update()
		{
			_velocityProgressBar.MaxValue = _controller.MaxVariableVelocity + _controller.AfterBurnerModifier + _controller.MinimumVelocity;
			_velocityProgressBar.Value = _controller.CurrentVelocity;

			_healthProgressbar.MaxValue = _player.MaxHealth;
			_healthProgressbar.Value = _player.Health;
		}

		public void OnGUI()
		{
			GUI.DrawTexture( new Rect(
				_controller.MousePosition.x - ( CurrentCursorSize / 2 ),
				Screen.height - _controller.MousePosition.y - ( CurrentCursorSize / 2 ), 
				CurrentCursorSize, 
				CurrentCursorSize),
			                GameResources.TargetReticle);

		}
	}
}

