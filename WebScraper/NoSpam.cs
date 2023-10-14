using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace WebScraper;

public class NoSpam
{
    private static IWebDriver _driver;

    public static void SetDriver(IWebDriver driver)
    {
        _driver = driver;
    }

    public static IWebElement? WaitForElementToBeVisible(By by, double timeoutInSeconds)
    {
        TimeSpan timeout = TimeSpan.FromSeconds(timeoutInSeconds);
        WebDriverWait wait = new WebDriverWait(_driver, timeout);
        try
        {
            return wait.Until(ExpectedConditions.ElementIsVisible(by));
        }
        catch (WebDriverTimeoutException)
        {
            return null;
        }
    }

    public static bool WaitForUrlToBe(string url, double timeoutInSeconds)
    {
        TimeSpan timeout = TimeSpan.FromSeconds(timeoutInSeconds);
        WebDriverWait wait = new WebDriverWait(_driver, timeout);
        try
        {
            wait.Until(ExpectedConditions.UrlToBe(url));
            return true;
        }
        catch (WebDriverTimeoutException)
        {
            return false;
        }
    }
    
    public static async Task WaitAsync(double secondsDelay)
    {
        TimeSpan delay = TimeSpan.FromSeconds(secondsDelay);
        await Task.Delay(delay);
    }
}