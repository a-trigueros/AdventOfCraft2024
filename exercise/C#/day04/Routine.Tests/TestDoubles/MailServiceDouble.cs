namespace Routine.Tests.TestDoubles;

public class MailServiceDouble : IEmailService
{
    public bool WasReadNewEmailsCalled { get; private set; } = false;
    public void ReadNewEmails()
    {
        WasReadNewEmailsCalled = true;
    }
}