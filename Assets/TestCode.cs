using InGame.Message;
using UnityEngine;

public class TestCode : MonoBehaviour
{
    [SerializeField] private MessageControl _messageControl;
    [SerializeField] private string _a;
    [SerializeField, TextArea(3, 10)] private string _b;

    private async void OnEnable()
    {
        while (true)
        {
            await _messageControl.ShowMessage(_a, _b);
        }
    }
}
