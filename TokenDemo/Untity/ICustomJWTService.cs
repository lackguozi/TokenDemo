namespace TokenDemo.Untity
{
    public interface ICustomJWTService
    {
        string GetJWTToken(string name,string pwd);
    }
}
