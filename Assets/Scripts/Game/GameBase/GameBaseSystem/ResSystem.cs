using QFramework;
using UnityEngine;

namespace Game.GameBase
{
    public class ResSystem : AbstractSystem, IResSystem
    {
        protected override void OnInit()
        {
        }

        public Texture2D LoadTexture(string imagePath)
        {
            Texture2D texture = new Texture2D(2, 2);
            byte[] fileData = System.IO.File.ReadAllBytes(imagePath);
            texture.LoadImage(fileData);
            return texture;
        }
    }
}