using OpenQA.Selenium;

namespace WebScraper;

public interface IBrowser
{
    Task NavigateAsync(string url);
    IWebElement FindElement(By by);

    Task ClickButtonAsync(IWebElement button);
}