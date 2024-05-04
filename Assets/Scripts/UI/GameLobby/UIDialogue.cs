using System;
using System.Collections.Generic;
using Game.Dialogue;
using Game.GameBase;
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

        /// <summary>
        /// 用于填充对话内可以变动的部分
        /// </summary>
        public List<string> dialogueValue;

        /// <summary>
        /// 对话结束后发生的事件
        /// </summary>
        public List<Action> dialogueAction;

        public UIDialogueData(string dialoguePath = null, List<string> dialogueValue = null,
            List<Action> dialogueAction = null)
        {
            this.dialoguePath = dialoguePath;
            this.dialogueValue = dialogueValue ?? new List<string>();
            this.dialogueAction = dialogueAction ?? new List<Action>();
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
        }

        protected override void OnListenButton()
        {
            for (int i = 0; i < chooseButtons.Count; i++)
            {
                var i1 = i;
                chooseButtons[i].onClick.AddListener(() =>
                {
                    if (mData.dialogueAction.Count > _node.dialogueActionIndex && _node.dialogueActionIndex >= 0)
                    {
                        mData.dialogueAction[_node.dialogueActionIndex]();
                    }

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

            List<string> value = new List<string>();
            for (int i = 0; i < _node.dialogValueIndex.Count; i++)
            {
                if (mData.dialogueValue.Count > _node.dialogValueIndex[i])
                {
                    value.Add(mData.dialogueValue[_node.dialogValueIndex[i]]);
                }
            }

            string dialogueText = this.GetSystem<IGameSystem>()
                .GetLocalizationText(_node.contentIndex, value, LocalizationType.DIALOGUE);

            if (_node.NodeType() == DialogueNodeType.NPC)
            {
                npcText.text = dialogueText;
                npcDialogBox.gameObject.SetActive(true);
                playerDialogBox.gameObject.SetActive(false);
            }
            else if (_node.NodeType() == DialogueNodeType.PLAYER)
            {
                playText.text = dialogueText;
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
                chooseButtonTexts[0].text = this.GetSystem<IGameSystem>()
                    .GetLocalizationText(4, null, LocalizationType.DIALOGUE_TIP);
            }
            else
            {
                for (int i = 0; i < _node.children.Count; i++)
                {
                    if (i == 0 && _node.children.Count == 1)
                    {
                        chooseButtons[0].gameObject.SetActive(true);
                        chooseButtonTexts[0].text = this.GetSystem<IGameSystem>()
                            .GetLocalizationText(4, null, LocalizationType.DIALOGUE_TIP);
                        break;
                    }

                    chooseButtons[i].gameObject.SetActive(true);
                    chooseButtonTexts[i].text = this.GetSystem<IGameSystem>()
                        .GetLocalizationText(_node.children[i].optionToThisIndex, null, LocalizationType.DIALOGUE_TIP);
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