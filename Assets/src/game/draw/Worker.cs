using UnityEngine;
using System.Collections;
using System.IO;


using com.zonglv.minigame.manager;

/// <summary>
/// 伐木工的检测代码,所有的胜利条件都是挂在此物体上的并且将胜利之后的场景截图
/// </summary>
public class Worker : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// 被击中了之后的处理
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("ok,he was dead");

        StartCoroutine("SavePicFunction");
        
        Invoke("WorkerDone", 1.6f);
    }


    /// <summary>
    /// 弹出胜利界面的功能
    /// </summary>
    private void WorkerDone()
    {
        Destroy(gameObject);
        EventManager.Instance.TriggerMiniGameEvent("Victory");
        Time.timeScale = 0;
    }

    /// <summary>
    /// 用于存储图片
    /// </summary>
    IEnumerator SavePicFunction()
    {
        // 因为"WaitForEndOfFrame"在OnGUI之后执行		// 所以我们只在渲染完成之后才读取屏幕上的画面		
        yield return new WaitForEndOfFrame(); int width = Screen.width; int height = Screen.height;     // 创建一个屏幕大小的纹理，RGB24 位格（24位格没有透明通道，32位的有）		
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);       // 读取屏幕内容到我们自定义的纹理图片中		
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);        // 保存前面对纹理的修改		tex.Apply(); 		// 编码纹理为PNG格式		
        byte[] bytes = tex.EncodeToPNG();
        // 销毁无用的图片纹理		
        Destroy(tex);
        // 将字节保存成图片，这个路径只能在PC端对图片进行读写操作		
        //File.WriteAllBytes(Application.dataPath + "/onPcSavedScreen.png", bytes);		

        // 这个路径会将图片保存到手机的沙盒中，这样就可以在手机上对其进行读写操作了		
        File.WriteAllBytes(Application.persistentDataPath + "/onMobileSavedScreen1.png", bytes);
        //GameObject.Find("TestText").GetComponent<Text>().text = File.Exists(Application.persistentDataPath + "/onMobileSavedScreen.png").ToString();
        WWW www = new WWW(Application.persistentDataPath + "/onMobileSavedScreen1.png");
        yield return www;





        //GameObject.Find("TestText").GetComponent<Text>().text = photopath;
    }

}
