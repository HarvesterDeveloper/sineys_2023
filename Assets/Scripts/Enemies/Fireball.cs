using UnityEngine;

public class Fireball : MonoBehaviour
{
	public Vector2 MoveDirection;
	private float lifeTime;
	//private Rigidbody2D rb;
	
	private void Start()
	{
		//rb = GetComponent<Rigidbody2D>();
	}
	
    private void OnCollisionEnter2D(Collision2D collision)
    {
		Debug.Log(collision.gameObject.name);
		
		PlayerController temp = null;
		collision.gameObject.TryGetComponent<PlayerController>(out temp);
		
		if (temp != null)
		{
			temp.TellDamage(5f);
			Destroy(this.gameObject);
		}
    }
	
	private void Update()
	{
		lifeTime += Time.deltaTime;
		
		transform.position = transform.position + new Vector3(MoveDirection.x, MoveDirection.y, 0f) * 5f * Time.deltaTime;
		
		//rb.AddForce(MoveDirection * 500f * Time.deltaTime);
		
		if (lifeTime >= 5f)
		{
			Destroy(this.gameObject);
		}
	}
}
