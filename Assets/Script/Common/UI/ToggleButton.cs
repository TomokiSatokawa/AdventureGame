using UnityEngine;
using UnityEngine.UI;

namespace Common.UI
{
    /// <summary>
    /// OnClickéûÇ…On/OffÇêÿÇËë÷Ç¶ÇÈ
    /// </summary>
    public class ToggleButton : MonoBehaviour
    {
        [SerializeField] private Button _buttons;
        [SerializeField] private Color _selectColor;
        [SerializeField] private Color _unselectColor;

        public bool IsOn { get;private set; }
        private void Start()
        {
            IsOn = false;
            _buttons.image.color = _unselectColor;

            _buttons.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            IsOn = !IsOn;
            _buttons.image.color = IsOn ? _selectColor : _unselectColor;
        }
    }
}