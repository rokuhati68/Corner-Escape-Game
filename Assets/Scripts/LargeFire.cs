using UnityEngine;

public class LargeFire : FireBase
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("OutSide"))
        {
            ObjectPool.Instance.ReturnToPool(this, "Large");
        }
    }

}