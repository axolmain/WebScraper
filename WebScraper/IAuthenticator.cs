namespace WebScraper;

public interface IAuthenticator
{
    Task LoginAsync(string? username, string password);
}