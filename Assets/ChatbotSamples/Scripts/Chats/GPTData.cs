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
        // Resources ���� ���� JSON ������ �ҷ��ɴϴ�.
        TextAsset jsonFile = Resources.Load<TextAsset>("apikey");

        if (jsonFile != null)
        {
            // JSON ������ ������ Ŭ���� �ν��Ͻ��� ��ȯ�մϴ�.
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
