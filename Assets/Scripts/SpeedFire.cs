using UnityEngine;

public class SpeedFire : FireBase
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("OutSide"))
        {
            ObjectPool.Instance.ReturnToPool(this, "Speed");
        }
    }

}