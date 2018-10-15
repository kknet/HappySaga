using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
namespace com.zonglv.minigame.common {

    public class PopBase : MonoBehaviour {
        public enum PopType {
            ScaleFromCenter,
            TopToCenter,
            LeftToRight
        }
        //弹窗的3种方式
        public PopType popType = PopType.ScaleFromCenter;
        public Transform content;
        public Action CloseComplete;
        public Action PreClose;
        public Action<PopBase> onDialogOpened;
        public Action<PopBase> onDialogClosed;

        protected virtual void PreShow() {

        }
 

        public  void ShowPop() {
            PreShow ();
            content.DOKill ();
            if (popType == PopType.ScaleFromCenter) {
                content.localPosition = Vector3.one;
                content.DOScale (1f, 1f);
            } else if (popType == PopType.TopToCenter) {
                content.localPosition = new Vector3 (content.localPosition.x, 1280f);
                content.DOLocalMoveY (0, 1f);
            } else if (popType == PopType.LeftToRight) {
                content.localPosition = new Vector3 (-1280f, content.localPosition.y);
                content.DOLocalMoveX (0, 1f);
            }
        }
        public void Hide() {

        }

        public virtual void Close() {
            if (PreClose != null) {
                PreClose ();
            }
            content.DOKill ();
            if (popType == PopType.ScaleFromCenter) {
                content.DOScale (0f, 1f).OnComplete (() => {
                    CloseEvent ();
                });
            } else if (popType == PopType.TopToCenter) {
                content.DOLocalMoveY (0, 1280f).OnComplete (() => {
                    CloseEvent ();
                });
            } else if (popType == PopType.LeftToRight) {
                content.DOLocalMoveX (1280, 1f).OnComplete (() => {
                    CloseEvent ();
                });
            }
        }

        private void CloseEvent() {
            if (CloseComplete != null) {
                CloseComplete ();
            }
            Destroy (this.gameObject);
        }
    }
}
