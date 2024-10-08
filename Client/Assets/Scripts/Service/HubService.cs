using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using UnityEngine;
using UnityEngine.UI;

public class HubService
{
    public event Action<string> OnMessageReceived;
    public event Action<Dictionary<InsectType, int>> OnPlayerInsectViewReceived;
    public event Action<List<VertexDTO>> OnBoardReceived;
    public event Action<Trigger> OnTriggerReceived;

    private readonly ConfigLoader _configLoader;
    public HubService(ConfigLoader configLoader)
    {
        _configLoader = configLoader;
    }

    private HubConnection _hubConnection;

    private string _serverUrl
    {
        get
        {
            return _configLoader.GetConfigValue(ConfigLoaderConsts.MatchmakingHubUrlKey);
        }
    }


    public async Task InitializeMatchmakingServiceAsync(string token)
    {
        _hubConnection = new HubConnectionBuilder()
            .WithUrl(_serverUrl, options =>
            {
                options.AccessTokenProvider = () => Task.FromResult(token);
            })
            .Build();

        _hubConnection.On<string, string, Trigger?, PlayerViewDTO>("ReceiveMessage", (player, message, trigger, playerView) =>
        {
            Debug.Log($"Player: {player}. Message from server: {message}. Has trigger: {trigger.HasValue}");


            if (trigger.HasValue)
            {
                OnTriggerReceived?.Invoke(trigger.Value);
            }


            if (playerView != null && playerView.PlayerInsects != null)
            {
                Debug.Log($"HubService: Got player insects");
                OnPlayerInsectViewReceived?.Invoke(playerView.PlayerInsects);
            }

            if (playerView != null && playerView.Board != null)
            {
                Debug.Log($"HubService: Got board");
                OnBoardReceived?.Invoke(playerView.Board);
            }
        });

        await _hubConnection.StartAsync();
    }

    public async Task JoinQueueAsync()
    {
        await _hubConnection.InvokeAsync("JoinQueue");
    }

    public async Task LeaveQueueAsync()
    {
        await _hubConnection.InvokeAsync("LeaveQueue");
    }

    public async Task PutInsectAsync(InsectType insect, (int, int, int)? position)
    {
        await _hubConnection.InvokeAsync("PutInsect", insect, position);
    }

    public async Task PutFirstInsectAsync(InsectType insect)
    {
        await _hubConnection.InvokeAsync("PutFirstInsect", insect);
    }

    public async Task MoveInsectAsync()
    {
        throw new NotImplementedException();
        await _hubConnection.InvokeAsync("MoveInsectAsync");
    }
}