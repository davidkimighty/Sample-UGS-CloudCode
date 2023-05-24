using Newtonsoft.Json;
using SharedLibrary;
using Unity.Services.CloudCode.Apis;
using Unity.Services.CloudCode.Core;
using Unity.Services.CloudCode.Shared;
using Unity.Services.RemoteConfig.Model;

namespace Sample_UGS_CloudCode;

public class MessageModule
{
    [CloudCodeFunction("SayHello")]
    public async Task<Message> SayHello(IExecutionContext context, IGameApiClient gameApiClient,string name)
    {
        Message messageData = new Message();
        try
        {
            ApiResponse<SettingsDeliveryResponse> response = await gameApiClient.RemoteConfigSettings.AssignSettingsGetAsync(context, context.AccessToken,
                context.ProjectId, context.EnvironmentId, null, new List<string>(){ RemoteConfigKeys.message_sender.ToString() });

            if (response.Data.Configs.Settings.TryGetValue(RemoteConfigKeys.message_sender.ToString(), out object config))
            {
                Sender sender = JsonConvert.DeserializeObject<Sender>(config.ToString());
                messageData.content = $"Hello, {name}!\n- {sender.name} -";
            }
        }
        catch (ApiException ex)
        {
            throw;
        }
        return messageData;
    }
}