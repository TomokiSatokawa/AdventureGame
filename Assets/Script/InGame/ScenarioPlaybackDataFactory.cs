using UnityEngine;

public class ScenarioPlaybackDataFactory : MonoBehaviour
{
    [SerializeField] private string _startScenario;
    [SerializeField] private int _startIndex;

    private void Start()
    {
        //Šù‚É‘¶İ‚µ‚Ä‚¢‚ê‚Îíœ
        ScenarioPlaybackData.Depose();

        var obj = new GameObject("ScenarioPlayData");
        var scenarioPlaybackData =  obj.AddComponent<ScenarioPlaybackData>();
        scenarioPlaybackData.SetData(_startScenario, _startIndex); 
    }
}
