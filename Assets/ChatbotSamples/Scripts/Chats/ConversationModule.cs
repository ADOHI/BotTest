using com.cyborgAssets.inspectorButtonPro;
using OpenAI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class ConversationModule : MonoBehaviour
{
    private OpenAIApi openAIApi;
    private List<ChatMessage> messages = new List<ChatMessage>();

    public bool isConversationGeneratable = true;

    public GPTData gptData;

    public UnityEvent<string> onGeneratingMessageStart;
    public UnityEvent<string> onReceiveMessage;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        openAIApi = new OpenAIApi(apiKey: gptData.apiKey);

        onReceiveMessage.AddListener(OnReceiveMessage);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ProButton]
    private async void SendMessage()
    {
        if (!isConversationGeneratable)
        {
            return;
        }

        isConversationGeneratable = false;

        var newMessage = new ChatMessage()
        {
            Role = "user",
            Content = gptData.prompt
        };

        messages.Add(newMessage);

        var completionResponse = await openAIApi.CreateChatCompletion(new CreateChatCompletionRequest()
        {
            Model = "gpt-4o",
            Messages = messages,

        });

        if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
        {
            var message = completionResponse.Choices[0].Message;
            message.Content = message.Content.Trim();


            onReceiveMessage.Invoke(message.Content);
            
        }
        else
        {
            Debug.LogWarning("No text was generated from this prompt.");
        }

    }

    [ProButton]
    private async void SendMessage(string inputMessage)
    {
        if (!isConversationGeneratable)
        {
            return;
        }

        isConversationGeneratable = false;

        var newMessage = new ChatMessage()
        {
            Role = "user",
            Content = gptData.prompt+ "\n" + inputMessage
        };

        messages.Add(newMessage);

        var completionResponse = await openAIApi.CreateChatCompletion(new CreateChatCompletionRequest()
        {
            Model = "gpt-4o",
            Messages = messages,

        });

        if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
        {
            var message = completionResponse.Choices[0].Message;
            message.Content = message.Content.Trim();


            onReceiveMessage.Invoke(message.Content);

        }
        else
        {
            Debug.LogWarning("No text was generated from this prompt.");
        }

    }


    private void OnReceiveMessage(string message)
    {
        Debug.Log(message);
        foreach (var item in ScriptParser.ParseString(message))
        {
            Debug.Log(item);
        }
    }

    private void OnComplete()
    {

    } 

    public void ReleaseGeneratable()
    {
        isConversationGeneratable = true;
    }
}
