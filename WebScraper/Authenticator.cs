using OpenQA.Selenium;

namespace WebScraper;

public class Authenticator : IAuthenticator
{
    private readonly IBrowser _browser;

    public Authenticator(IBrowser browser)
    {
        _browser = browser;
    }

    public async Task LoginAsync(string? username, string password)
    {
        await _browser.NavigateAsync("https://my.byui.edu/ICS/Academics/");

        // Check if user is logged in
        if (Scraper.IsElementPresent(By.XPath("/html/body/div[3]/form/div[4]/div/div/div/div[1]/h2/div/span"),
                _browser))
        {
            // Find and click login from dropdown banner thing
            IWebElement? signIn = _browser.FindElement(By.XPath("//*[@id=\"siteNavBar_ctl00_btnLogin\"]"));

            await _browser.ClickButtonAsync(signIn);

            await NoSpam.WaitAsync(2);

            // Log in
            EnterCredentials(username, password);

            await NoSpam.WaitAsync(1);

            IWebElement? loginFrThisTime = 
                _browser.FindElement(By.XPath("/html/body/main/div/div[1]/div[2]/div[2]/form/section[3]/input[5]"));

            await _browser.ClickButtonAsync(loginFrThisTime);
            
            

            // Wait to be back on main page
            NoSpam.WaitForUrlToBe("https://my.byui.edu/ICS/Academics/", 120);
        }
    }

    private void EnterCredentials(string? username, string password)
    {
        IWebElement? loginBox = _browser.FindElement(By.Id("login"));
        IWebElement? usernameInput = loginBox.FindElement(By.Name("username"));
        IWebElement? passwordInput = loginBox.FindElement(By.Name("password"));

        usernameInput.SendKeys(username);
        passwordInput.SendKeys(password);
    }
}