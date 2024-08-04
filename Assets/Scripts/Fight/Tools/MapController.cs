using Fight.Game;
using QFramework;
using UnityAttribute;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Fight.Tools
{
    /**
     * 根据a*的网格生成相应的地图
     */
    public class MapController : MonoBehaviour, IController, IPointerEnterHandler, IPointerExitHandler,
        IBeginDragHandler, IEndDragHandler
    {
        private static readonly Color32 GrassColor = new(34, 139, 34, 255);
        private static readonly Color32 IceColor = new(173, 216, 230, 255);
        private static readonly Color32 MagmaColor = new(255, 69, 0, 255);
        private static readonly Color32 LavaGroundColor = new(139, 0, 0, 255);
        private static readonly Color32 StoneColor = new(112, 128, 144, 255);
        [Label("原始图块")] [SerializeField] private GameObject piece;

        private void Awake()
        {
            var aStarModel = this.GetModel<AStarModel>();
            var index = 0;
            foreach (var graphNode in aStarModel.FightGridNodeInfoList.Values)
            {
                var obj = Instantiate(piece, transform);
                obj.transform.localPosition = (Vector3)graphNode.position;
                obj.name = (++index).ToString();
                // SpriteRenderer sprite = obj.GetComponent<SpriteRenderer>();
                // sprite.color = graphNode.Tag switch
                // {
                //     Constants.GrassTag => GrassColor,
                //     Constants.IceTag => IceColor,
                //     Constants.MagmaTag => MagmaColor,
                //     Constants.LavaGroundTag => LavaGroundColor,
                //     Constants.StoneTag => StoneColor,
                //     _ => sprite.color
                // };
            }
        }

        private void Update()
        {
            #region debug

            if (_isPointerOver)
            {
                var mousePosition = Input.mousePosition;
                var worldPosition =
                    Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 0));
                var spriteRenderer =
                    transform.GetChild(this.GetModel<AStarModel>().GetGridNodeIndexMyRule(worldPosition))
                        .GetComponent<SpriteRenderer>();
                if (_spriteRenderer != spriteRenderer)
                {
                    spriteRenderer.color = Color.black;
                    if (_spriteRenderer) _spriteRenderer.color = Color.white;

                    _spriteRenderer = spriteRenderer;
                }
            }

            #endregion
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _isPointerOver = true;
        }


        public IArchitecture GetArchitecture()
        {
            return GameApp.Interface;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _isPointerOver = false;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _isPointerOver = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _isPointerOver = false;
        }

        #region debug

        private bool _isPointerOver;
        private SpriteRenderer _spriteRenderer;

        #endregion
    }
}