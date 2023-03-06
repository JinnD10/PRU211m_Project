using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionGun : MonoBehaviour
{
	// Start is called before the first frame update

	private int Atk;
	private int Range;
	private int Gold;
	private int Level;
	private int Reload;

	public GameObject Explosion_bullet;
	public GameObject Explosion_gun;
	public UnityEngine.Transform Spot;
	public float counter = 0;

	void Start()
	{
		Atk = 5;
		Range = 10;
		Reload = 2;
		Gold = 30;
		Level = 1;
	}

	// Update is called once per frame
	void Update()
	{
		counter = counter + Time.deltaTime;
		Rotate();
	}
	private void Rotate()
	{
		Collider2D[] hit = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), Range);
		if (hit[0].tag == "Enemy" && hit.Length >= 1)
		{


			Vector2 lookDir = hit[0].transform.position - transform.position;
			float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0, 0, angle);
			if ((int)counter % Reload == 0 && counter > Reload)
			{
				GameObject obj = Instantiate<GameObject>(Explosion_bullet, Spot.position, Explosion_gun.transform.rotation);
				obj.GetComponent<ExplosionBullet>().gun(Atk, hit[0].gameObject);
				counter = 0;
			}
		}

	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.white;
		Gizmos.DrawSphere(transform.position, Range);
	}
}
