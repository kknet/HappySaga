using UnityEngine;
using System.Collections;

namespace com.zonglv.minigame.game.bow
{
    /// <summary>
    /// 边界，当子弹打到边界时的处理
    /// </summary>
    public class DestroyerSprite : MonoBehaviour
    {


        void OnTriggerEnter2D(Collider2D col)
        {
            //destroyers are located in the borders of the screen
            //if something collides with them, the'll destroy it
            string tag = col.gameObject.tag;
            if (tag == "Bird" || tag == "Pig" || tag == "Brick")
            {
                Destroy(col.gameObject);
            }
        }
    }
}
