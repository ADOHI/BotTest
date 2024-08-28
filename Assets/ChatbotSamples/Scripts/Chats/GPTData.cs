using UnityEngine;

[CreateAssetMenu(fileName = "GPTData", menuName = "Scriptable Objects/GPTData")]
public class GPTData : ScriptableObject
{
    public string apiKey;
    [TextArea]
    public string prompt;
}
