using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace com.zonglv.minigame.draw
{
    /// <summary>
    /// 绘画类的抽象基类，由于要进行绘画，要先继承自MonoBehaiver
    /// </summary>
    public abstract class DrawAbst :MonoBehaviour
    {
        public abstract void DrawSprites();
    }
}
