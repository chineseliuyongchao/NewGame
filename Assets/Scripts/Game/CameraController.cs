using GameQFramework;
using QFramework;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// 控制摄像机的移动
    /// </summary>
    public class CameraController : BaseGameController
    {
        public float moveSpeed = 5f;
        private float _minX;
        private float _maxX;
        private float _minY;
        private float _maxY;

        private void Start()
        {
            float proportion = 5 / 5.4f; //摄像机拍摄范围缩小比例（原理不清楚）
            _minX = -this.GetModel<IMapModel>().MapSize.x * (1 - proportion) / 2;
            _maxX = -_minX;
            _minY = -this.GetModel<IMapModel>().MapSize.y * (1 - proportion) / 2;
            _maxY = -_minY;
        }

        private void Update()
        {
            // 获取用户输入
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // 计算移动方向和距离
            Vector3 moveDirection = new Vector3(horizontalInput, verticalInput, 0);
            Vector3 moveAmount = moveDirection.normalized * (moveSpeed * Time.deltaTime);

            // 移动相机
            Transform transform1;
            (transform1 = transform).Translate(moveAmount);

            float clampedX = Mathf.Clamp(transform1.position.x, _minX, _maxX);
            var position = transform.position;
            float clampedY = Mathf.Clamp(position.y, _minY, _maxY);

            // 更新相机位置
            position = new Vector3(clampedX, clampedY, position.z);
            transform.position = position;
        }
    }
}