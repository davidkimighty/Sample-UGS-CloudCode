using Microsoft.Extensions.DependencyInjection;
using Unity.Services.CloudCode.Apis;
using Unity.Services.CloudCode.Core;

namespace Sample_UGS_CloudCode;

public class ModuleConfig : ICloudCodeSetup
{
    public void Setup(ICloudCodeConfig config)
    {
        config.Dependencies.AddSingleton(GameApiClient.Create());
    }
}

public enum RemoteConfigKeys
{
    message_sender,
    slotmachine_reel,
        
}

public enum CurrencyKeys
{
    CREDIT
}

public enum VirtualPurchaseKeys
{
    PLAY_SLOTMACHINE
}