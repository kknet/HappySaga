using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

/**
 * Author :     XiaoYing
 * Version:     1.0
 * Date：       2018-9-29
 * Description：关卡编辑数据的读写修改操作。
 * Change History: 
 */
namespace com.zonglv.minigame.editor {

    public enum ItemType {
        HairBallA,//毛球A
        HairBallB,//毛球B
        Hedgehog,//刺猬
        Lumberjack,//伐木工
        Accelerator,//加速器
        Helmet,//安全帽
        Fire,//火焰
        Lighting,//闪电
        MushRoom,//蘑菇
        Squre,//正方形
        Rectangle,//长方形
        Arc,//圆弧
        Triangle,//直角三角形
        Trapezoid,//梯形
        IsoscelesTriangle,//等腰三角形
        Grassland//草地
    }

    public class LevelEditorData : MonoBehaviour {
        [HideInInspector]
        public List<LineData> mLinedata = new List<LineData> ();
        private List<string> mData = new List<string> ();
        private string mLevelPath;
        [HideInInspector]
        public Dictionary<int, LevelData> levelInfo = new Dictionary<int, LevelData> ();

        private void Start() {
            mLevelPath = SetPath ("LevelData.csv");
           // ReadCSVData ();
        }

        public void WriteCSVData() {
            if (mData == null) {
                Debug.Log ("The mData is null !!!!!!!!!");
                return;
            }
            using (StreamWriter sw = new StreamWriter (mLevelPath, false)) {
                sw.WriteLine ("LevelID,MapId,BackgroudId,LimitLength,RoleAnimationName,IdleAnimationName,WinAnimationName,LoseAnimationName,RoleX,RoleY,StarScore,Item1(type|imageName|scaleX|scaleY|positionX|positionY|rotationX|rotationY|isPhysical),Item2,Item3,Item4,Item5,Item6");
                for (int i = 1; i < mData.Count; i++) {
                    sw.WriteLine (mData[i]);
                }
            }
            LevelEditorManager.instance.ShowHint ("保存成功！ ");
        }

        /// <summary>
        /// 每个线的第一行记录了画线Transform的位置
        /// 第二行记录选择角度
        /// 后面记录的是线包含的点的信息，一行是一个点
        /// </summary>
        public void SaveLineData(string levelID) {
            string path = SetPath ("Line" + levelID + ".csv");
            if (!File.Exists (path)) {
                File.Create (path);
            }
            if (LevelEditorManager.instance.mLineGroup.childCount ==0) {
                return;
            }
            using (StreamWriter sw = new StreamWriter (path, false)) {
                sw.WriteLine ("LineID,X,Y");
                for (int i = 0; i < LevelEditorManager.instance.mLineGroup.childCount; i++) {
                    LineRenderer lineRender = LevelEditorManager.instance.mLineGroup.GetChild (i).GetComponent<LineRenderer> ();
                    int id = i + 1;
                    for (int j = 0; j < lineRender.positionCount; j++) {
                        float x = lineRender.GetPosition (j).x;
                        float y = lineRender.GetPosition (j).y;
                        if (j == 0) {
                            x = lineRender.transform.localPosition.x;
                            y = lineRender.transform.localPosition.y;
                        }
                        if (j == 1) {
                            x = lineRender.transform.localEulerAngles.z;
                            y = 0;
                        }
                        if (j == lineRender.positionCount - 1 && i == LevelEditorManager.instance.mLineGroup.childCount-1) {
                            sw.WriteLine (id.ToString () + "," + x.ToString () + "," + y.ToString () );
                        } else {
                            sw.WriteLine (id.ToString () + "," + x.ToString () + "," + y.ToString () + ",");
                        }
                    }
                }
            }
            LevelEditorManager.instance.ShowHint ("保存成功！ ");

        }

        public void ReadLineData(string levelID) {
            string path = SetPath ("Line" + levelID + ".csv");
            string[] loadData = LoadFileLines (path);
            if (loadData == null) {
                Debug.Log ("The LineData is null !!!!!!!!!");
                return;
            }

            int id = 0;
            for (int i = 1; i < loadData.Length; i++) {
                string[] info = loadData[i].Split (',');
                if (id != int.Parse (info[0])) {
                    LineData data = new LineData ();
                    id = int.Parse (info[0]);
                    data.lineID = id;
                    Vector3 point = Vector3.zero;
                    point.x = float.Parse (info[1]);
                    point.y = float.Parse (info[2]);
                    data.points.Add (point);
                    mLinedata.Add (data);
                } else {
                    Vector3 point = Vector3.zero;
                    point.x = float.Parse (info[1]);
                    point.y = float.Parse (info[2]);
                    mLinedata[id - 1].points.Add (point);
                }

            }
        }

        //这里肯定要改的
        public void ChangeDataInfo(int levelID, LevelData data) {
            string level = "";
            level += data.levelID.ToString () + ",";
            level += data.mapID.ToString () + ",";
            level += data.backgroudID.ToString () + ",";
            level += data.limitLength.ToString () + ",";
            level += (data.starScore[0].ToString () + "+" + data.starScore[1].ToString () + "+" + data.starScore[2].ToString () + "+" + data.starScore[3].ToString () + ",");
            for (int i = 0; i < data.items.Count; i++) {
                ItemInfo ld = data.items[i];
                level += (ld.type.ToString () + "+" + ld.scaleX.ToString () + "+"
                    + ld.scaleY.ToString () + "+" + ld.positionX.ToString () + "+" + ld.positionY.ToString () + "+"
                    + ld.rotationZ.ToString () + "+" + ld.isPhysical.ToString ());
                if (i != data.items.Count - 1) {
                    level += ",";
                }
            }
            if (levelID < mData.Count) {
                mData[levelID - 1] = level;
            } else {
                mData.Add (level);
            }

        }

        public void ReadCSVData() {
            string[] loadData = LoadFileLines (mLevelPath);
            if (loadData == null) {
                Debug.Log ("The Data is null !!!!!!!!!");
                return;
            }
            mData = loadData.ToList<string> ();
            for (int i = 1; i < loadData.Length; i++) {
                string[] info = loadData[i].Split (',');
                if (info.Length > 4) {
                    LevelData data = new LevelData ();
                    List<int> scores = new List<int> () { };
                    List<ItemInfo> itemInfos = new List<ItemInfo> ();
                    data.levelID = int.Parse (info[0]);
                    data.mapID = int.Parse (info[1]);
                    data.backgroudID = int.Parse (info[2]);
                    data.limitLength = int.Parse (info[3]);
                    data.roleAnimationName = info[4];
                    data.idleAnimationName = info[5];
                    data.winAnimationName = info[6];
                    data.loseAnimationName = info[7];
                    data.roleX = float.Parse (info[8]);
                    data.roleY = float.Parse (info[9]);

                    string[] score = info[10].Split ('+');
                    scores.Add (int.Parse (score[0]));
                    scores.Add (int.Parse (score[1]));
                    scores.Add (int.Parse (score[2]));
                    scores.Add (int.Parse (score[3]));

                    for (int j = 11; j < info.Length; j++) {
                        ItemInfo item = new ItemInfo ();
                        string[] item_info = info[j].Split ('+');
                        item.type = int.Parse (item_info[0]);
                        item.scaleX = float.Parse (item_info[1]);
                        item.scaleY = float.Parse (item_info[2]);
                        item.positionX = float.Parse (item_info[3]);
                        item.positionY = float.Parse (item_info[4]);
                        item.rotationZ = float.Parse (item_info[5]);
                        item.isPhysical = bool.Parse (item_info[6]);
                        itemInfos.Add (item);

                    }

                    data.starScore = scores;
                    data.items = itemInfos;
                    levelInfo.Add (data.levelID, data);

                }
            }
        }

        public string[] LoadFileLines(string path) {
            if (Application.platform == RuntimePlatform.WindowsEditor) {
                return File.ReadAllLines (path);
            } else if (Application.platform == RuntimePlatform.Android) {
                WWW www = new WWW (path);
                while (!www.isDone) { }
                return www.text.Split (new string[] { "\r\n" }, StringSplitOptions.None);
            }
            return null;
        }

        private string SetPath(string path) {
            path = Application.streamingAssetsPath + "/" + path;
            return path;
        }
    }

    public class LineData {
        public int lineID;
        public List<Vector3> points = new List<Vector3> ();
    }

    public class LevelData {
        public int levelID;
        public int mapID;
        public int backgroudID;
        public int limitLength;
        public string roleAnimationName;
        public string idleAnimationName;
        public string winAnimationName;
        public string loseAnimationName;
        public float roleX;
        public float roleY;
        public List<int> starScore = new List<int> ();
        public List<ItemInfo> items = new List<ItemInfo> ();
    }



    public class ItemInfo {
        public int type;
        public float scaleX;
        public float scaleY;
        public float positionX;
        public float positionY;
        public float rotationZ;
        public bool isPhysical;
    }
}
