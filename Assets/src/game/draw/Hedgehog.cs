using UnityEngine;


namespace com.zonglv.minigame.game.draw
{
    /// <summary>
    /// 刺蝟的控制类
    /// </summary>
    public class Hedgehog : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<CircleCollider2D>().sharedMaterial = null;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log(collision.gameObject.name);
            if(collision.gameObject.name=="fire")
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                Destroy(collision.gameObject);
            }

            if(collision.gameObject.name== "electric")
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                Destroy(collision.gameObject);
            }

            if(collision.gameObject.name=="spring")
            {
                GetComponent<CircleCollider2D>().sharedMaterial = Resources.Load("mat/Jump") as PhysicsMaterial2D;
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            Debug.Log(collision.gameObject.name);
            GetComponent<CircleCollider2D>().sharedMaterial = null;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("this is OnTriggerEnter" + collision.name);
            GetComponent<CircleCollider2D>().sharedMaterial = Resources.Load("mat/Jump") as PhysicsMaterial2D; //PhysicsMaterial2D
        }
    }
}
