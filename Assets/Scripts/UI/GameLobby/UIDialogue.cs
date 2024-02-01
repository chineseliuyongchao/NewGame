using System.Collections.Generic;
using Game.Dialogue;
using GameQFramework;
using QFramework;
using TMPro;
using UnityEngine.UI;

namespace UI
{
    public class UIDialogueData : UIPanelData
    {
        /// <summary>
        /// 对话树的资源路径
        /// </summary>
        public string dialoguePath;

        public UIDialogueData(string dialoguePath = null)
        {
            this.dialoguePath = dialoguePath;
        }
    }

    public partial class UIDialogue : UIBase
    {
        public List<Button> chooseButtons;
        public List<TextMeshProUGUI> chooseButtonTexts;
        private DialogueTree _dialogueTree;
        private BaseDialogueNode _node;

        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as UIDialogueData ?? new UIDialogueData();
            // please add init code here
            base.OnInit(uiData);
            this.SendCommand(new HasShowDialogCommand(true));
            InitUI();
        }

        protected override void OnOpen(IUIData uiData = null)
        {
            mData = uiData as UIDialogueData ?? new UIDialogueData();
            // please add open code here
            base.OnOpen(uiData);
        }

        protected override void OnShow()
        {
            base.OnShow();
        }

        protected override void OnHide()
        {
            base.OnHide();
        }

        protected override void OnClose()
        {
            base.OnClose();
            this.SendCommand(new HasShowDialogCommand(false));
        }

        protected override void OnListenButton()
        {
            for (int i = 0; i < chooseButtons.Count; i++)
            {
                var i1 = i;
                chooseButtons[i].onClick.AddListener(() =>
                {
                    _node = _dialogueTree.UpdateDialogue(i1);
                    ShowDialogue();
                });
            }
        }

        protected override void OnListenEvent()
        {
        }

        private void InitUI()
        {
            _dialogueTree = mResLoader.LoadSync<DialogueTree>(mData.dialoguePath);
            _dialogueTree.StartDialogue();
            _node = _dialogueTree.UpdateDialogue(0);
            ShowDialogue();
        }

        /// <summary>
        /// 每次更新对话节点以后都要展示对话信息
        /// </summary>
        private void ShowDialogue()
        {
            if (_node == null)
            {
                DialogueEnd();
                return;
            }

            if (_node.NodeType() == DialogueNodeType.NPC)
            {
                npcText.text = _node.content;
                npcDialogBox.gameObject.SetActive(true);
                playerDialogBox.gameObject.SetActive(false);
            }
            else if (_node.NodeType() == DialogueNodeType.PLAYER)
            {
                playText.text = _node.content;
                npcDialogBox.gameObject.SetActive(false);
                playerDialogBox.gameObject.SetActive(true);
            }
            else
            {
                return;
            }

            for (int i = 0; i < chooseButtons.Count; i++)
            {
                chooseButtons[i].gameObject.SetActive(false);
            }

            if (_node.children.Count == 0)
            {
                chooseButtons[0].gameObject.SetActive(true);
                chooseButtonTexts[0].text = "继续";
            }
            else
            {
                for (int i = 0; i < _node.children.Count; i++)
                {
                    if (i == 0 && _node.children.Count == 1)
                    {
                        chooseButtons[0].gameObject.SetActive(true);
                        chooseButtonTexts[0].text = "继续";
                        break;
                    }

                    chooseButtons[i].gameObject.SetActive(true);
                    chooseButtonTexts[i].text = _node.children[i].optionToThis;
                }
            }
        }

        /// <summary>
        /// 对话结束
        /// </summary>
        private void DialogueEnd()
        {
            CloseSelf();
        }
    }
}