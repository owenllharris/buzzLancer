using UnityEngine;

namespace Assets.Code
{
	public static class GameExtentions
	{
		public static T FindComponent<T>(this GameObject that) where T : Component
		{
			var component = that.GetComponent<T>();

			if( component != null )
				return component;

			var proxy = that.GetComponent<ColliderProxy>();
			if( proxy == null )
				return null;

			if ( proxy.ProxyFor == null )
			{
				Debug.LogWarning("ColliderProxy didn't specify what it was a proxy for", that);
				return null;
			}

			return proxy.ProxyFor.GetComponent<T>();
		}
	}
}

