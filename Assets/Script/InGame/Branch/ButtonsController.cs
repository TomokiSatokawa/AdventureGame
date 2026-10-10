using System.Data.Common;
using System.Net.NetworkInformation;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InGame.Branch
{
    public class ButtonsController : MonoBehaviour
    {
        [SerializeField] private Button _button1;
        [SerializeField] private TextMeshProUGUI _text1;  
        
        [SerializeField] private Button _button2;
        [SerializeField] private TextMeshProUGUI _text2;  

        [SerializeField] private Button _button3;
        [SerializeField] private TextMeshProUGUI _text3;

        private BranchState _currentState = BranchState.Idle;
        private int _selectNumber;

        private void Start()
        {
            _button1.onClick.AddListener(() => OnClick(0));
            _button2.onClick.AddListener(() => OnClick(1));
            _button3.onClick.AddListener(() => OnClick(2));

            SetButtonActive(false);
        }

        private void SetButtonActive(bool b)
        {
            _button1.gameObject.SetActive(b);
            _button2.gameObject.SetActive(b);
            _button3.gameObject.SetActive(b);
        }

        public async  UniTask<string> ShowButton(BranchData branchData)
        {
            _currentState = BranchState.Select;

            ButtonSetting(_button1, _text1, branchData.OptionName1, branchData.OptionDestination1);
            ButtonSetting(_button2, _text2, branchData.OptionName2, branchData.OptionDestination2);
            ButtonSetting(_button3, _text3, branchData.OptionName3, branchData.OptionDestination3);

            while (_currentState != BranchState.End) 
                await UniTask.Yield();

            SetButtonActive(false);
            _currentState = BranchState.Idle;

            switch (_selectNumber)
            {
                case 0:
                    return branchData.OptionDestination1;
                case 1:
                    return branchData.OptionDestination2;
                case 2:
                    return branchData.OptionDestination3;
                default:
                    Debug.LogError("aaa");
                    return "";
            }

        }

        private void ButtonSetting(Button button,TextMeshProUGUI text,string buttonName,string destination)
        {
            if (string.IsNullOrEmpty(destination))
            {
                button.gameObject.SetActive(false);
                return;
            }

            button.gameObject.SetActive(true);
            text.text = buttonName;
        }

        private void OnClick(int number)
        {
            _selectNumber = number;
            _currentState = BranchState.End;
        }

        private enum BranchState
        {
            Idle,Select,End
        }
    }
}
