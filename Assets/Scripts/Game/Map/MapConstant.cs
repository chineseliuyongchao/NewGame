namespace Game.Map
{
    /// <summary>
    /// 地图相关常量
    /// </summary>
    public abstract class MapConstant
    {
        /// <summary>
        /// 图片的多少像素算一个网格
        /// </summary>
        public const int GRID_SIZE = 10;

        /// <summary>
        /// 网格地图的宽度
        /// </summary>
        public const int MAP_MESH_WIDTH = 192;

        /// <summary>
        /// 网格地图的高度
        /// </summary>
        public const int MAP_MESH_HEIGHT = 108;

        /// <summary>
        /// 地图单位像素
        /// </summary>
        public const int MAP_PIXELS_PER_UNIT = 100;

        /// <summary>
        /// 地图资源路径
        /// </summary>
        public const string MAP_PATH = "Assets/Res/Image/map/";

        /// <summary>
        /// 地图图片路径
        /// </summary>
        public const string MAP_FILE_NAME = "map1.png";

        /// <summary>
        /// 地图网格图路径
        /// </summary>
        public const string GRID_MAP_FILE_NAME = "grid_image.jpg";
    }
}