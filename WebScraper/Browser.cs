using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace WebScraper;

public class Browser : IBrowser, IDisposable
{
    private readonly IWebDriver _driver;

    public Browser()
    {
        _driver = new FirefoxDriver();
        NoSpam.SetDriver(_driver);
    }

    public async Task NavigateAsync(string url)
    {
        await Task.Run(() => _driver.Navigate().GoToUrl(url));
    }

    public IWebElement FindElement(By by)
    {
        return _driver.FindElement(by);
    }

    public async Task ClickButtonAsync(IWebElement button)
    {
        try
        {
            await Task.Run(button.Click);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }

    public void Dispose()
    {
        _driver.Dispose();
    }
}