// FreezeMoving.cs is part of 2D Physics Draw asset for Uunity 3D
// Made by tianguosong
// date:2019.9.19

using UnityEngine;

namespace com.zonglv.minigame.draw
{
    // this class is related to the freezeWhileMoving variable
    // it enables or disables the movement of drawn objects while drawing another
    public class FreezeSpriteMoving : MonoBehaviour
    {

        private Rigidbody2D rigiThis;
        private GameObject drawingManager;

        void Start()
        {
            rigiThis = this.GetComponent<Rigidbody2D>();
            drawingManager = GameObject.Find("DrawingSprite");
        }

        public static bool freeze = false;

        void Update()
        {
            if (drawingManager.GetComponent<DrawingSprite>().freezeWhileDrawing == true)
            {
                if (freeze == true)
                    rigiThis.bodyType = RigidbodyType2D.Static;
                else
                    rigiThis.bodyType = RigidbodyType2D.Dynamic;
            }

        }
    }
}
