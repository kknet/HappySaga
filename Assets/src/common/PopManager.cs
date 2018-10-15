using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum PopType {

}
public enum PopShow {
    DONT_SHOW_IF_OTHERS_SHOWING,
    REPLACE_CURRENT,
    STACK,
    SHOW_PREVIOUS,
    OVER_CURRENT
};
namespace com.zonglv.minigame.common {

    public class PopManager : MonoBehaviour {
        public static PopManager instance;
        [HideInInspector]
        public PopBase current;
        [HideInInspector]
        public PopBase[] basePops;
        public Action onPopOpened;
        public Action onPopClosed;
        public Stack<PopBase> pops = new Stack<PopBase> ();
        private void Awake() {
            instance = this;
        }



        public PopBase GetPop(PopType type) {
            PopBase pop = basePops[(int)type];
            return (PopBase)Instantiate (pop, transform.position, transform.rotation);
        }

        public void ShowPop(PopBase pop, PopShow option = PopShow.REPLACE_CURRENT) {
            if (current != null) {
                if (option == PopShow.DONT_SHOW_IF_OTHERS_SHOWING) {
                    Destroy (pop.gameObject);
                }else if(option== PopShow.REPLACE_CURRENT) {
                    current.Close ();
                }else if (option == PopShow.STACK) {
                    current.Hide ();
                }
            }
            current = pop;
            if (option != PopShow.SHOW_PREVIOUS) {
                current.onDialogClosed += OnOneDialogClosed;
                current.onDialogOpened += OnOneDialogOpened;
                pops.Push (current);
            }
            current.ShowPop ();
            if (onPopOpened != null) {
                onPopOpened ();
            }
        }
        private void OnOneDialogOpened(PopBase dialog) {

        }

        public void CloseCurrentPop() {
            if (current != null) {
                current.Close ();
            }
        }

        public bool IsPopShowing() {
            return current != null;
        }

        private void OnOneDialogClosed(PopBase dialog) {
            if (current == dialog) {
                current = null;
                pops.Pop ();
                if (onPopClosed != null && pops.Count == 0)
                    onPopClosed ();

                if (pops.Count > 0) {
                    ShowPop (pops.Peek (),PopShow. SHOW_PREVIOUS);
                }
            }
        }
    }
}
