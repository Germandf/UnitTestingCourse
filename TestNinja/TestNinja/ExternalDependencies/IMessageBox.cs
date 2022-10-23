namespace TestNinja.ExternalDependencies;

public interface IMessageBox
{
    void Show(string s, string housekeeperStatements, MessageBoxButtons messageBoxButtons);
}

public class MessageBox
{
    public void Show(string s, string housekeeperStatements, MessageBoxButtons messageBoxButtons)
    {
    }
}

public enum MessageBoxButtons
{
    OK
}
