using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Author :     XiaoYing
 * Version:     1.0
 * Date：       2018-9-29
 * Description：
 * Change History:
 */
namespace  com.zonglv.minigame.editor {

    public class EditorFreezeMoving : MonoBehaviour {


        private Rigidbody2D rigiThis;
        private GameObject drawingManager;

        void Start() {
            rigiThis = this.GetComponent<Rigidbody2D> ();
            drawingManager = GameObject.Find ("EditorSprite");
        }

        public static bool freeze = false;

        void Update() {
            if (drawingManager.GetComponent<EditorDrawing> ().freezeWhileDrawing == true) {
                if (freeze == true)
                    rigiThis.bodyType = RigidbodyType2D.Static;
                else
                    rigiThis.bodyType = RigidbodyType2D.Dynamic;
            }

        }
    } 
}
