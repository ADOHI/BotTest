using com.cyborgAssets.inspectorButtonPro;
using OpenAI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TTSModule : MonoBehaviour
{
    private AudioSource audioSource;

    private OpenAIApi openAIApi;

    public GPTData gptData;

    public UnityEvent onPlayingStart;
    public UnityEvent onPlayingFinished;
    private void Start()
    {
        openAIApi = new OpenAIApi(apiKey: gptData.apiKey);
        audioSource = GetComponent<AudioSource>();

        
    }

    public void SendRequest(string message)
    {
        SendRequestAsync(message);
    }

    [ProButton]
    public async void SendRequestAsync(string message)
    {
        var request = new CreateTextToSpeechRequest
        {
            Input = message,
            Model = "tts-1",
            Voice = "nova"
        };

        var response = await openAIApi.CreateTextToSpeech(request);

        if (response.AudioClip) audioSource.PlayOneShot(response.AudioClip);

        onPlayingStart.Invoke();

        Invoke("OnPlayingFinished", response.AudioClip.length);
    }

    public void OnPlayingFinished()
    {
        onPlayingFinished.Invoke();
    }
}
