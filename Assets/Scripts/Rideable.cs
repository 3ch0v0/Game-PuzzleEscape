using System.Diagnostics;
using UnityEngine;

public class Rideable : MonoBehaviour
{
    [SerializeField]
    public enum PlatformType
    {
        Tile,
        MovingPlatform
    }
    [SerializeField] PlatformType platformType=PlatformType.Tile;
    void OnCollisionEnter2D(Collision2D collision)
    {
        
        switch (platformType)
        {

            case PlatformType.Tile:
                collision.transform.SetParent(transform.parent);
                collision.transform.rotation = Quaternion.identity;
                
                break;
            case PlatformType.MovingPlatform:
                //if (collision.transform.position.y > transform.position.y)
                //{
                    collision.transform.SetParent(this.transform);
                //}
                break;
        }
       
    }
    
}
