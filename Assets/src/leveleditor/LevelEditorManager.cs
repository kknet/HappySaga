using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


/**
 * Author :     XiaoYing
 * Version:     1.0
 * Date：       2018-9-29
 * Description：管理编辑器视图操作
 * Change History: 
 */

namespace com.zonglv.minigame.editor {
    public class LevelEditorManager : MonoBehaviour {
        public Text mHing;
        public Text mLineLength;
        public Transform mLineGroup;
        public SpriteRenderer mBackgourd;
        public InputField mLimit;
        public InputField mMapID;
        public InputField mLevelID;
        public Transform mItemGroup;
        public LevelEditorData mEditor;
        public EditorDrawing mDraw;
        public int mCurrentLineLength = 0;

        public List<Sprite> mBackgroundImages = new List<Sprite> ();
        public List<InputField> mStarScore = new List<InputField> ();
        private int mBackgroudID;

        public static LevelEditorManager instance;
        private void Start() {
            instance = this;
        }
        private void OnEnable() {
            mDraw.mCanDrawLine = false;
        }

        public void LoadData() {
            if (string.IsNullOrEmpty (mLevelID.text)) {
                ShowHint ("请输入关卡ID!");
                return;
            }
            int level = int.Parse (mLevelID.text);
            mEditor.ReadCSVData ();
            if (mEditor.levelInfo == null || level > mEditor.levelInfo.Count) {
                ShowHint ("不存在关卡" + mLevelID.text);
                return;
            }
           
            SetView (mEditor.levelInfo[level - 1]);
            mEditor.ReadLineData (mLevelID.text);
            if (mEditor.mLinedata.Count <= 0) {
                return;
            }
            for (int i = 0; i < mEditor.mLinedata.Count;i++) {
                mDraw.LoadOldLine (mEditor.mLinedata[i].points);
            }
            
        }

        public void SetView(LevelData data) {
            mMapID.text = data.mapID.ToString () ;
            for (int i = 0; i < mStarScore.Count; i++) {
                mStarScore[i].text = data.starScore[i].ToString ();
            }
            mLimit.text = data.limitLength.ToString();
            mBackgroudID = data.backgroudID;
            for(int j = 0; j < data.items.Count; j++) {
               GameObject obj= ItemManger.instance.CloneItem (data.items[j].type);
                obj.GetComponent<ItemOperation> ().SetInfo (data.items[j]);
            }
        }

        public void SetLength(int length) {
            int count = 0;
            for (int i = 0; i < mLineGroup.childCount; i++) {
                count += mLineGroup.GetChild (i).GetComponent<LineRenderer> ().positionCount;
            }
            mCurrentLineLength = count;
            mLineLength.text = string.Format ("当前画线的长度: {0}", count);
        }


        public void DrawHintLine() {
            mDraw.mCanDrawLine = true;
           
        }

        public void ClearLine() {
            mDraw.mCanDrawLine = false;
            if (mLineGroup.childCount > 0) {
                LineRenderer line = mLineGroup.GetChild (mLineGroup.childCount - 1).GetComponent<LineRenderer> ();
                mCurrentLineLength -= line.positionCount;
                mLineLength.text = string.Format ("当前画线的长度: {0}", mCurrentLineLength);
                DestroyImmediate (mLineGroup.GetChild (mLineGroup.childCount - 1).gameObject);
            }
        }
     
        public void SaveData() {
            LevelData data = new LevelData ();
            if (string.IsNullOrEmpty (mLevelID.text)) {
                ShowHint ("请输入关卡ID!");
                return;
            }
            if (string.IsNullOrEmpty (mMapID.text)) {
                ShowHint ("请输入地图ID!");
                return;
            }
            if (string.IsNullOrEmpty (mLimit.text)) {
                ShowHint ("请输入画线限制长度!");
                return;
            }
            List<int> score = new List<int> ();
            for (int i = 0; i < mStarScore.Count; i++) {
                if (string.IsNullOrEmpty (mStarScore[i].text)) {
                    Debug.Log (string.Format ("请设置{0}星的分数! ", i + 1));
                    return;
                } else {
                    score.Add (int.Parse (mStarScore[i].text));
                }
            }
            data.mapID = int.Parse (mMapID.text);
            data.levelID = int.Parse (mLevelID.text);
            data.limitLength = int.Parse (mLimit.text);
            data.backgroudID = mBackgroudID;
            data.starScore = score;

            for (int j = ItemManger.instance.items.Count; j < mItemGroup.childCount; j++) {
                data.items.Add (mItemGroup.GetChild (j).GetComponent<ItemOperation> ().SaveInfo ());
            }
            mEditor.ChangeDataInfo (data.levelID, data);
            mEditor.WriteCSVData ();
            mEditor.SaveLineData (mLevelID.text);
        }



        public void SelectBg(int id) {
            mBackgroudID = id;
            mBackgourd.sprite = mBackgroundImages[id];
        }

        public void ShowHint(string info) {
            mHing.DOKill ();
            mHing.text = info;
            mHing.DOFade (1f, 1f).OnComplete (() => {
                mHing.DOFade (0f, 1f).SetDelay (1f);
            });
        }

    }
}
