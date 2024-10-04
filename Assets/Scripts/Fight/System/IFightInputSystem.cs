using QFramework;

namespace Fight.System
{
    /// <summary>
    /// 处理战斗输入事件
    /// </summary>
    public interface IFightInputSystem : ISystem
    {
        /// <summary>
        /// 点击鼠标左键
        /// </summary>
        void MouseButtonLeft();

        /// <summary>
        /// 点击鼠标中键
        /// </summary>
        void MouseButtonMiddle();

        /// <summary>
        /// 点击鼠标右键
        /// </summary>
        void MouseButtonRight();
    }
}