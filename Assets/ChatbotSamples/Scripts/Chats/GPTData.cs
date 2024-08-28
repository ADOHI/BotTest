using UnityEngine;

[CreateAssetMenu(fileName = "GPTData", menuName = "Scriptable Objects/GPTData")]
public class GPTData : ScriptableObject
{
    [HideInInspector]
    public string apiKey => GetAPIKey();
    [TextArea]
    public string prompt;
    public class APIKeyData
    {
        public string OpenAIKey;
    }

    private APIKeyData apiKeyData;
    
    private void OnEnable()
    {
        LoadAPIKey();
    }

    private void LoadAPIKey()
    {
        // Resources 폴더 내의 JSON 파일을 불러옵니다.
        TextAsset jsonFile = Resources.Load<TextAsset>("apikey");

        if (jsonFile != null)
        {
            // JSON 파일의 내용을 클래스 인스턴스로 변환합니다.
            apiKeyData = JsonUtility.FromJson<APIKeyData>(jsonFile.text);
            Debug.Log("API Key loaded successfully.");
        }
        else
        {
            Debug.LogError("API key JSON file not found!");
        }
    }

    public string GetAPIKey()
    {
        if (apiKeyData != null)
        {
            return apiKeyData.OpenAIKey;
        }
        else
        {
            Debug.LogError("API key is not loaded.");
            return null;
        }
    }

}
