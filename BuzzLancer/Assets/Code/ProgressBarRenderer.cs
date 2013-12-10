using UnityEngine;

namespace Assets.Code
{
	public class ProgressBarRenderer : MonoBehaviour
	{
		private ProgressBar _bar;

		public void Init( ProgressBar bar )
		{
			_bar = bar;
		}

		public void OnGUI()
		{
			var oldColour = GUI.color;

			GUI.color = _bar.BackgroundColour;
			GUI.DrawTexture( new Rect( _bar.Position.x, _bar.Position.y, _bar.Size.x, _bar.Size.y ), GameResources.Square );

			GUI.color = _bar.ForegroundColour;
			GUI.DrawTexture( new Rect( _bar.Position.x, _bar.Position.y, ( _bar.Value * _bar.Size.x ) / _bar.MaxValue, _bar.Size.y ), GameResources.Square );

			GUI.color = oldColour;
		}
	}
}

