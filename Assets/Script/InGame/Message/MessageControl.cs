using Common.UI;
using Cysharp.Threading.Tasks;
using R3;
using TMPro;
using UnityEngine;

namespace InGame.Message
{
    /// <summary>
    /// メッセージウィンドウを動かす
    /// </summary>
    public class MessageControl : MonoBehaviour
    {
        [Header("テキスト")]
        [SerializeField] private TextMeshProUGUI _characterName;
        [SerializeField] private TextMeshProUGUI _messageText;

        [Header("ボタン")]
        [SerializeField] private ToggleButton _autoButton;

        [Header("パラメーター")]
        [SerializeField] private float _normalTextInterval;
        [SerializeField] private float _skipTextInterval;
        [SerializeField] private float _autoWaitTime;


        private MessageState _currentState = MessageState.Idle;

        private void Start()
        {
            InputManager.Next.Subscribe(_ => OnNextButton());
        }

        private void OnNextButton()
        {
            switch (_currentState)
            {
                case MessageState.Normal:
                    _currentState = MessageState.Skip;
                    break;

                case MessageState.Wait:
                    _currentState = MessageState.End;
                    break;
            }
        }

        public async UniTask ShowMessage(string character, string text)
        {
            _currentState = MessageState.Normal;

            _characterName.text = character;
            _messageText.text = "";

            bool isTag = false;
            foreach (var t in text)
            {
                //タグを検出
                if (t == '<')
                {
                    isTag = true;
                }
                _messageText.text += t;

                //タグ中は一括表示
                if (!isTag)
                {
                    var interval = GetTextInterval();

                    if (interval > 0f)
                    {
                        await UniTask.WaitForSeconds(interval);
                    }
                }


                if (isTag && t == '>')
                {
                    isTag = false;
                }
            }
            _currentState = MessageState.Wait;

            if (_autoButton.IsOn)
            {
                await UniTask.WaitForSeconds(_autoWaitTime);
                _currentState = MessageState.End;
            }

            while (true)
            {
                if (_currentState == MessageState.End)
                    break;

                await UniTask.Yield();
            }

            _currentState = MessageState.Idle;
        }

        private float GetTextInterval()
        {
            switch (_currentState)
            {
                case MessageState.Normal:
                    return _normalTextInterval;
                case MessageState.Skip:
                    return _skipTextInterval;
                default:
                    return 0;
            }
        }

        private enum MessageState
        {
            Idle, Normal, Skip, Wait, End
        }
    }
}