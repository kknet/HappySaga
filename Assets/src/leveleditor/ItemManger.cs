using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Author :     XiaoYing
 * Version:     1.0
 * Date：       2018-9-29
 * Description：控制物体的克隆，摧毁，拖拽，缩放和旋转
 * Change History: 
 */

namespace com.zonglv.minigame.editor {
    public class ItemManger : MonoBehaviour {
        [HideInInspector]
        public Transform mCurrentSelect;
        public List<GameObject> items = new List<GameObject> ();
        private float mRotation = 1f;
        private float mScaleValue = 0.1f;
        private float mCurrentScale = 1f;
        private bool mScaleW = false;
        private bool mScaleH = false;
        private bool mIsRotate = false;
        private bool mIsReverseRotate = false;
        public static ItemManger instance;

        private void Awake() {
            instance = this;
        }
        public void TypeButtonClick(int id) {
            CloneItem (id);
        }

        public GameObject CloneItem(int id) {
            GameObject clone = Instantiate (items[id], items[id].transform.parent);
            clone.transform.localScale = Vector3.one;
            clone.gameObject.SetActive (true);
            clone.GetComponent<ItemOperation> ().type = id;
            return clone;

        }
        public void ResetSelectItem(Transform trans) {
            if (mCurrentSelect == trans) {
                return;
            }
            mCurrentSelect = trans;
            mCurrentScale = trans.localScale.x;
        }
        private void Update() {
            if (mCurrentSelect == null || LevelEditorManager.instance.mDraw.mCanDrawLine) {
                return;
            }
            if (Input.GetMouseButtonDown (1)) {
                DestroyImmediate (mCurrentSelect.gameObject);
                mCurrentSelect = null;
            }
            //物体的旋转
            if (Input.GetKeyDown (KeyCode.A)) {
                mIsRotate = true;

            }
            if (Input.GetKeyUp (KeyCode.A)) {
                mIsRotate = false;
            }
            if (Input.GetKeyDown (KeyCode.D)) {
                mIsReverseRotate = true;

            }
            if (Input.GetKeyUp (KeyCode.D)) {
                mIsReverseRotate = false;
            }
            if (mIsRotate) {
                mCurrentSelect.Rotate (transform.rotation.x, transform.rotation.y, transform.rotation.z + mRotation);
            }
            if (mIsReverseRotate) {
                mCurrentSelect.Rotate (transform.rotation.x, transform.rotation.y, transform.rotation.z - mRotation);
            }
            //物体的缩放
            if (Input.GetKeyDown (KeyCode.W)) {
                mScaleW = true;
            } else if (Input.GetKeyDown (KeyCode.H)) {
                mScaleH = true;
            }
            if (Input.GetKeyUp (KeyCode.W)) {
                mScaleW = false;
            } else if (Input.GetKeyUp (KeyCode.H)) {
                mScaleH = false;
            }

            if (Input.GetAxis ("Mouse ScrollWheel") < 0) {
                mCurrentScale += mScaleValue;
            }
            if (Input.GetAxis ("Mouse ScrollWheel") > 0) {
                mCurrentScale -= mScaleValue;
                if (mCurrentScale <= 0) {
                    return;
                }
            }

            if (mScaleW) {
                mCurrentSelect.localScale = new Vector3 (mCurrentScale, mCurrentSelect.localScale.y, 1);
            } else if (mScaleH) {
                mCurrentSelect.localScale = new Vector3 (mCurrentSelect.localScale.x, mCurrentScale, 1);
            } else {
                 mCurrentSelect.localScale = new Vector3 (mCurrentScale, mCurrentScale, 1);
            }
           

            if (Input.GetKeyUp (KeyCode.P)) {
                mCurrentSelect.GetComponent<ItemOperation> ().SetPhysical ();
            }
        }
    }
}
