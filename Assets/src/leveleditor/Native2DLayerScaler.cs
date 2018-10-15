using UnityEngine;
using System.Collections;

/**
 * Author :     XiaoYing
 * Version:     1.0
 * Date：       2018-10-8
 * Description：游戏Sprite的根目录, 借助UGUI的Canvas的大小来适配
 * Change History: 
 */

[ExecuteInEditMode]
public class Native2DLayerScaler: MonoBehaviour {

	public UnityEngine.UI.CanvasScaler referenceCanvas;

	void Start(){
		if(referenceCanvas){
			transform.localScale = referenceCanvas.transform.localScale*100f;
		}
	}

	#if UNITY_EDITOR
	void LateUpdate(){
		if(referenceCanvas){
			transform.localScale = referenceCanvas.transform.localScale*100f;
		}
	}
	#endif
}
