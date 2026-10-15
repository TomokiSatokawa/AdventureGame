using UnityEngine;

public class ScenarioPlaybackData : SingletonPersistent<ScenarioPlaybackData>
{
    public string StartScenario {  get; private set; }  
    public int StartIndex { get; private set; }

    public void SetData( string start,int index)
    {
        StartScenario = start;
        StartIndex = index;
    }
}
