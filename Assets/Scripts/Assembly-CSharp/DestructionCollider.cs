public class DestructionCollider : UnityEngine.MonoBehaviour
{
	public void OnCollisionEnter(UnityEngine.Collision collision)
	{
		if (collision.gameObject.layer == UnityEngine.LayerMask.NameToLayer("Destructible"))
		{
			PerlinShake.instance.PlayShakeBig();
			SoundControl.instance.PlaySledCrash();
			DestructibleObj component = collision.gameObject.GetComponent<DestructibleObj>();
			if (component)
			{
				collision.gameObject.GetComponent<DestructibleObj>().OnDestruct();
			}
			else
			{
				UnityEngine.Debug.Log("collision.gameObject " + collision.gameObject);
			}
		}
		else if (collision.gameObject.layer == UnityEngine.LayerMask.NameToLayer("Gift"))
		{
			DestructibleObj component = collision.gameObject.GetComponent<DestructibleObj>();
			if (component)
			{
				collision.gameObject.GetComponent<DestructibleObj>().OnDestruct();
			}
			collision.gameObject.GetComponent<Gift>().OnDie();
			GameControl.gifts++;
			GameControl.giftsThisGame++;
		}
	}

}
