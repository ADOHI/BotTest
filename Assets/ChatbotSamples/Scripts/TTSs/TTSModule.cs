using com.cyborgAssets.inspectorButtonPro;
using OpenAI;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.UI;

public class TTSModule : MonoBehaviour
{

    private OpenAIApi openAIApi;

    public GPTData gptData;
    public AudioSource audioSource;

    public UnityEvent onPlayingStart;
    public UnityEvent onPlayingFinished;
    private void Start()
    {
        openAIApi = new OpenAIApi(apiKey: gptData.apiKey);
        //audioSource = GetComponent<AudioSource>();

        
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

        if (response.AudioClip)
        {
            audioSource.clip = response.AudioClip;
            audioSource.Play();
        }

        onPlayingStart.Invoke();

        Invoke("OnPlayingFinished", response.AudioClip.length);
    }

    public void OnPlayingFinished()
    {
        onPlayingFinished.Invoke();
    }
}
