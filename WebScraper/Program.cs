namespace WebScraper;

internal class Program
{
    private static async Task Main(string[] args)
    {
        IBrowser browser = new Browser();
        IAuthenticator authenticator = new Authenticator(browser);
        IScraper scraper = new Scraper(browser, authenticator);

        await scraper.LoginAndScrapeAsync("CSE 210-06", "Thayne, Timothy E.", "Fall Semester 2023");
    }
}