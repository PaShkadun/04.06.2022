using Unity.Collections;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
	public bool spreadShot = false;

	[Header("General")]
	public Transform gunBarrel;
	public ParticleSystem shotVFX;
	public AudioSource shotAudio;
	public float fireRate = .1f;
	public int spreadAmount = 20;

	[Header("Bullets")]
	public GameObject bulletPrefab;

	private GameObject[] _bullets;
	private int _shootCount = 0;
	private int index = 0;
	private int maxSingleShoot = 25;
	private int maxMultiShoot = 10;

	float timer;

	void Start()
	{
		var rotation = gunBarrel.rotation.eulerAngles;
		rotation.x = 0f;
		
		if (spreadShot)
		{
			_bullets = new GameObject[spreadAmount * spreadAmount * maxMultiShoot];

			for (var i = 0; i < spreadAmount; i++)
			{
				SpawnBulletSpread(rotation);
			}
		}
		else
		{
			_bullets = new GameObject[maxSingleShoot];
			
			for (var i = 0; i < maxSingleShoot; i++)
			{
				SpawnBullet(rotation);
			}
		}

		index = 0;
	}

	void Update()
	{
		timer += Time.deltaTime;

		if (Input.GetButton("Fire1") && timer >= fireRate)
		{
			Vector3 rotation = gunBarrel.rotation.eulerAngles;
			rotation.x = 0f;

			if (spreadShot)
			{

				if (++_shootCount > spreadAmount)
				{
					_shootCount = 1;
					index = 0;
				}
				
				Shoot(rotation);
			}
			else
			{
				if (++_shootCount > _bullets.Length)
				{
					_shootCount = 1;
					index = 0;
				}
				
				ShootOneBullet(rotation);
			}


			timer = 0f;

			if (shotVFX)
				shotVFX.Play();

			if (shotAudio)
				shotAudio.Play();
		}
	}

	void ShootOneBullet(Vector3 rotation)
	{
		_bullets[index].transform.position = gunBarrel.position;
		_bullets[index].transform.rotation = Quaternion.Euler(rotation);
		_bullets[index].SetActive(true);
		index++;
	}

	void Shoot(Vector3 rotation)
	{
		var max = spreadAmount / 2;
		var min = -max;
		var tempRot = rotation;
		
		for (var x = min; x < max; x++)
		{
			tempRot.x = (rotation.x + 3 * x) % 360;

			for (var y = min; y < max; y++, index++)
			{
				tempRot.y = (rotation.y + 3 * y) % 360;

				_bullets[index].transform.position = gunBarrel.position;
				_bullets[index].transform.rotation = Quaternion.Euler(tempRot);
				_bullets[index].SetActive(true);
			}
		}
	}

	void SpawnBullet(Vector3 rotation)
	{
		var bullet = Instantiate(bulletPrefab);
		bullet.SetActive(false);
		_bullets[index++] = bullet;
	}

	void SpawnBulletSpread(Vector3 rotation)
	{
		var max = spreadAmount / 2;
		var min = -max;

		for (var x = min; x < max; x++)
		{
			for (var y = min; y < max; y++, index++)
			{
				var bullet = Instantiate(bulletPrefab);
				bullet.SetActive(false);
				_bullets[index] = bullet;
			}
		}
	}

}

