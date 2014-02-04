using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Code
{
	public class AsteroidManager : MonoBehaviour
	{
		
		private List<Asteroid> _asteroids;
		private Player _player;

		public Asteroid AsteroidPrefab;
		public int MaxAsteroids;
		public int MaxVisibleAsteroids;

		public void Awake()
		{
			_asteroids = new List<Asteroid>();
			_player = (Player) FindObjectOfType( typeof ( Player ) );
		}

		public void Start()
		{
			for( var i = 0; i < MaxAsteroids; i++ )
			{
				var asteroid = (Asteroid) Instantiate( AsteroidPrefab );
				asteroid.gameObject.name = "Asteroid " + i;
				asteroid.Deactivate();
				_asteroids.Add( asteroid );
			}
		}

		public void AsteroidDestroyed( Asteroid asteroid )
		{
			asteroid.Deactivate();

			if( asteroid.Level <= 3 )
				return;

			var toCreate = (int) Mathf.Ceil(asteroid.Level - 3) * Random.Range(1f, 2f);
			for ( var i = 0; toCreate > 0 && i < _asteroids.Count; i++ )
			{
				var inactiveAsteroid = _asteroids[i];
				if (inactiveAsteroid.IsActive)
					continue;

				ActivateSubAsteroid( inactiveAsteroid, asteroid.gameObject.transform.position);
				toCreate--;
			}
		}

		public void LateUpdate()
		{
			var totalVisible = 0;
			foreach ( var asteroid in _asteroids.Where( a => a.IsActive ) )
			{
				asteroid.UpdatePlayerPosition( _player.transform.position );

				if( asteroid.IsVisible )
					totalVisible++;
			}

			for ( var i = 0; i < _asteroids.Count; i++ )
			{
				var asteroid = _asteroids[i];

				if( asteroid.IsActive )
				{
					if ( asteroid.IsVisible )
						continue;

					if( asteroid.DistanceSquared < 50 * 50 )
						continue;
				}

				if (totalVisible < MaxVisibleAsteroids)
				{
					ActivateAsteroid( asteroid );
					totalVisible++;
				}
				else
					asteroid.Deactivate();
			}
		}

		public void ActivateAsteroid( Asteroid asteroid )
		{
			asteroid.Activate();

			var rotation = new Vector3(
				Random.Range(0f, 360f), 
				Random.Range(0f, 360f), 
				Random.Range(0f, 360f) );

			var direction = Random.insideUnitSphere;

			var scale = 
				Random.Range(0f, 1f) > 0.3f
					? new Vector3( Random.Range(25f, 90f), Random.Range(25f, 90f), Random.Range(25f, 90f) )
					: new Vector3( Random.Range(110f, 160f), Random.Range(110f, 160f), Random.Range(110f, 160f) );

			var velocioty = Random.Range( 15f, 25f );

			var position = Camera.main.ViewportToWorldPoint( new Vector3(
				Random.Range( -0.5f, 1.5f), 
				Random.Range( -0.5f, 1.5f), 
				(Random.Range(0f, 1f) * 300) + 150) );

			asteroid.Init( position, rotation, direction, scale, velocioty );
		}

		public void ActivateSubAsteroid( Asteroid asteroid, Vector3 spawnNextTo )
		{
			asteroid.Activate();
			
			var rotation = new Vector3(
				Random.Range(0f, 360f), 
				Random.Range(0f, 360f), 
				Random.Range(0f, 360f) );
			
			var direction = Random.insideUnitSphere;
			
			var scale = new Vector3( Random.Range(15f, 40f), Random.Range(15f, 40f), Random.Range(15f, 40f) );
			
			var velocioty = Random.Range( 20f, 35f );
			
			var position = spawnNextTo + direction * 5;
			
			asteroid.Init( position, rotation, direction, scale, velocioty );
		}
	}
}

