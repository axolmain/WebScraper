using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace WebScraper;

internal class color
{
    private static IWebDriver? Driver { get; set; }

    private static void Beeep()
    {
        const string url = "https://my.byui.edu/ICS/Academics/";

        const string Username = "sebastiancdunn";
        const string Password = "Volvic@12344";

        Driver = new FirefoxDriver();
        Driver.Navigate().GoToUrl(url);

        Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

        // Check if not logged in
        if (Driver.FindElement(By.XPath("/html/body/div[3]/form/div[4]/div/div/div/div[1]/h2/div/span")).Displayed)
        {
            // Click login from dropdown banner thing
            IWebElement? signIn = Driver.FindElement(By.XPath("//*[@id=\"siteNavBar_ctl00_btnLogin\"]"));
            Wait(1);

            signIn.Click();

            Wait(2);

            IWebElement? loginBox = Driver.FindElement(By.Id("login"));
            IWebElement? userName = loginBox.FindElement(By.Name("username"));
            IWebElement? password = loginBox.FindElement(By.Name("password"));

            userName.SendKeys(Username);
            Wait(0.3);
            password.SendKeys(Password);

            Wait(2);

            IWebElement? loginFrThisTime =
                Driver.FindElement(By.XPath("/html/body/main/div/div[1]/div[2]/div[2]/form/section[3]/input[5]"));

            loginFrThisTime.Click();

            WebDriverWait wait = new(Driver, TimeSpan.FromSeconds(120));
            wait.Until(ExpectedConditions.UrlToBe("https://my.byui.edu/ICS/Academics/"));
        }


        Driver.Navigate().GoToUrl(
                "https://my.byui.edu/ICS/Academics/Academic_Information.jnz?portlet=Registration&screen=Add+Drop+Courses+BYUI&screenType=next");

        Wait(2);

        TypeInBox(By.XPath("//*[@id=\"pg0_V_tabSearch_txtCourseRestrictor\"]"), "CSE 210", true);

        Driver.FindElement(By.Id("pg0_V_tabSearch_btnSearch")).Click();

        Wait(20);
    }

    private static void TypeInBox(By findingMethod, string whatToType, bool shouldWait = false)
    {
        Driver?.FindElement(findingMethod).SendKeys(whatToType);
        if (shouldWait)
            Wait(3);
    }

    private static void Wait(double seconds)
    {
        Thread.Sleep(TimeSpan.FromSeconds(seconds));
    }

    private static HtmlDocument GetPageHtml(string url)
    {
        HtmlWeb web = new();
        // downloading to the target page
        // and parsing its HTML content
        HtmlDocument? document = web.Load(url);

        return document;
    }

    private static HtmlNodeCollection SelectXpathNode(HtmlDocument doc, string xpath)
    {
        return doc.DocumentNode.SelectNodes(xpath);
    }
}