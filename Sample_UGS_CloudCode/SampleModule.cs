using Unity.Services.CloudCode.Core;

namespace Sample_UGS_CloudCode;

public class SampleModule
{
    [CloudCodeFunction("SayHello")]
    public string Hello(string name)
    {
        return $"Hello, {name}!";
    }
}