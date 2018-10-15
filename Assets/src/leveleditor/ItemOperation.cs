using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Author :     XiaoYing
 * Version:     1.0
 * Date：       2018-9-29
 * Description：物品信息类
 * Change History: 
 */

namespace com.zonglv.minigame.editor {
    public class ItemOperation : MonoBehaviour {
        public SpriteRenderer mSp;
        public bool mIsPhysical;
        public int type;

     
        public void OnMouseEnter() {
            ItemManger.instance.ResetSelectItem (transform) ;
        }

        public void OnMouseDown() {
          
        }

        public void OnMouseDrag() {
            if(LevelEditorManager.instance.mDraw.mCanDrawLine) {
                return;
            }
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
            this.transform.position = new Vector3 (currentPosition.x, currentPosition.y, 0);
        }

        public void OnMouseExit() {
            ItemManger.instance.mCurrentSelect = null;
        }

        public void SetInfo(ItemInfo info) {
            type = info.type;
            transform.localScale = new Vector3 (info.scaleX,info.scaleY,1);
            transform.localPosition = new Vector3 (info.positionX,info.positionY,0);
            transform.localEulerAngles = new Vector3 (0,0,info.rotationZ);
            mIsPhysical = info.isPhysical;
            SetPhysical ();
            
        }

        public ItemInfo SaveInfo() {
            ItemInfo iteminfo = new ItemInfo ();
            iteminfo.type = type;
            iteminfo.scaleX = transform.localScale.x;
            iteminfo.scaleY = transform.localScale.y;
            iteminfo.positionX = transform.localPosition.x;
            iteminfo.positionY =transform.localPosition.y;
            iteminfo.rotationZ = transform.localRotation.z;
            return iteminfo;
        }

        public void SetPhysical() {
            if (!mIsPhysical) {
                mIsPhysical = true;
                mSp.color = new Color (221f, 214f, 0f);
            } else {
                mIsPhysical = false;
                mSp.color = Color.white;
            }
        }
    }
}

