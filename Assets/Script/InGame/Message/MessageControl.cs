using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
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

        [Header("パラメーター")]
        [SerializeField] private float _normalTextInterval;
        [SerializeField] private float _skipTextInterval;

        private MessageState _currentState = MessageState.Idle;
        


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

        public async UniTask ShowMessage(string character,string text)
        {
            _currentState = MessageState.Normal;

            _characterName.text = character;
            _messageText.text = "";

            bool isTag = false;
            foreach(var t in text)
            {
                //タグを検出
                if(t == '<')
                {
                    isTag = true;
                }
                _messageText.text += t;

                //タグ中は一括表示
                if (!isTag)
                {
                    await UniTask.WaitForSeconds(GetTextInterval());
                }


                if (isTag && t == '>')
                {
                    isTag = false;
                }
            }
            _currentState = MessageState.Wait;

            while (_currentState == MessageState.End)
                await UniTask.Yield();

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
            Idle,Normal,Skip,Wait,End
        }
    }
}