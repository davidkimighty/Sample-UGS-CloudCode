using Newtonsoft.Json;
using SharedLibrary;
using Unity.Services.CloudCode.Apis;
using Unity.Services.CloudCode.Core;
using Unity.Services.CloudCode.Shared;
using Unity.Services.Economy.Model;
using Unity.Services.RemoteConfig.Model;

namespace Sample_UGS_CloudCode;

public class SlotMachineModule
{
    [CloudCodeFunction("SpinSlotMachine")]
    public async Task<SpinResult> SpinSlotMachine(IExecutionContext context, IGameApiClient gameApiClient, int numberOfReels)
    {
        SpinResult result = new SpinResult();
        try
        {
            await gameApiClient.CloudSaveData.SetItemAsync(context, context.AccessToken, context.ProjectId, context.PlayerId);

            CurrencyModifyBalanceRequest balanceIncreaseRequest = new CurrencyModifyBalanceRequest();
            balanceIncreaseRequest.Amount = 300;
            await gameApiClient.EconomyCurrencies.IncrementPlayerCurrencyBalanceAsync(context, context.AccessToken,
                context.ProjectId, context.PlayerId, CurrencyKeys.CREDIT.ToString(), balanceIncreaseRequest);
            
            //PlayerPurchaseVirtualRequest request = new PlayerPurchaseVirtualRequest();
            //request.Id = VirtualPurchaseKeys.PLAY_SLOTMACHINE.ToString();
            //await gameApiClient.EconomyPurchases.MakeVirtualPurchaseAsync(context, context.AccessToken,
                //context.ProjectId, context.PlayerId, request);
            
            ApiResponse<SettingsDeliveryResponse> response = await gameApiClient.RemoteConfigSettings.AssignSettingsGetAsync(context, context.AccessToken,
                context.ProjectId, context.EnvironmentId, null, new List<string>() { RemoteConfigKeys.slotmachine_reel.ToString() });

            if (response.Data.Configs.Settings.TryGetValue(RemoteConfigKeys.slotmachine_reel.ToString(), out object config))
            {
                SlotReel reel = JsonConvert.DeserializeObject<SlotReel>(config.ToString());
                List<string> spinResult = new List<string>();
                Random random = new Random();
                
                for (int i = 0; i < numberOfReels; i++)
                    spinResult.Add(reel.symbols[random.Next(0, reel.symbols.Length - 1)]);
                result.spins = spinResult.ToArray();
            }
        }
        catch (ApiException ex)
        {
            throw;
        }
        return result;
    }
}