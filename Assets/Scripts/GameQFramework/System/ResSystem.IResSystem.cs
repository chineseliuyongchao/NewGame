using QFramework;
using UnityEngine;

namespace GameQFramework
{
    public interface IResSystem : ISystem
    {
        /// <summary>
        /// 加载图片方法
        /// </summary>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        public Texture2D LoadTexture(string imagePath);
    }
}