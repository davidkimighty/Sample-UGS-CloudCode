using SharedLibrary;
using Unity.Services.CloudCode.Core;

namespace Sample_UGS_CloudCode;

public class MessageModule
{
    [CloudCodeFunction("SayHello")]
    public Message SayHello(string name)
    {
        return new Message()
        {
            from = "Cloud Code",
            content = $"Hello, {name}!!!"
        };
    }
}