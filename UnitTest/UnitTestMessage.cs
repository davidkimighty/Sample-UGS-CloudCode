using Sample_UGS_CloudCode;
using SharedLibrary;

namespace UnitTest;

public class Tests
{
    private MockedMessage _messageTest;
    
    [SetUp]
    public void Setup()
    {
        _messageTest = new MockedMessage("Bob");
    }

    [Test]
    public void TestMessage()
    {
        MessageModule messageModule = new MessageModule();
        Message result = messageModule.SayHello("Bob");

        Assert.AreEqual(_messageTest.Message.content, result.content, "Values are not the same");
    }
}

public class MockedMessage
{
    public Message Message;
    
    public MockedMessage(string name)
    {
        Message = new Message()
        {
            from = "Cloud Code",
            content = $"Hello, {name}!!!"
        };
    }
}