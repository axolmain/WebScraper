using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace WebScraper;

public class Scraper : IScraper
{
    private readonly IAuthenticator _authenticator;
    private readonly IBrowser _browser;

    public Scraper(IBrowser browser, IAuthenticator authenticator)
    {
        _browser = browser;
        _authenticator = authenticator;
    }

    public async Task LoginAndScrapeAsync(string course, string professor, string track = "Winter Semester 2024")
    {
        await _authenticator.LoginAsync(Environment.GetEnvironmentVariable("USERNAME_SCRAPER"), 
            Environment.GetEnvironmentVariable("PASSWORD_SCRAPER"));
        
        // Go to course search page
        await _browser.NavigateAsync(
            "https://my.byui.edu/ICS/Academics/Academic_Information.jnz?portlet=Registration&screen=Advanced+Course+" +
            "Search+BYUI&screenType=next");

        await NoSpam.WaitAsync(2);
        if (NoSpam.WaitForElementToBeVisible(By.XPath(
                "/html/body/div[3]/form/div[5]/div/div/div/div[3]/div/div/div/div[3]/div/div[2]/" +
                "select[1]"), 10) != null)
        {
            SelectTrack(By.XPath(
                "/html/body/div[3]/form/div[5]/div/div/div/div[3]/div/div/div/div[3]/div/div[2]/" +
                "select[1]"), track);
        }
        await NoSpam.WaitAsync(2);
        
        SelectTrack(By.XPath("/html/body/div[3]/form/div[5]/div/div/div/div[3]/div/div/div/div[3]/div/" +
                             "div[2]/select[6]"), professor);
        
        // Type the class you want
        await TypeInBox(By.XPath("/html/body/div[3]/form/div[5]/div/div/div/div[3]/div/div/div/div[3]/div/div[2]/input[2]"), course.Split('-')[0], true);
        
        IWebElement searchCourseBtn = _browser.FindElement(By.Name("pg0$V$btnSearch"));

        
        await _browser.ClickButtonAsync(searchCourseBtn);

        await NoSpam.WaitAsync(4);
        await ClickCellInTable(course);
        Console.WriteLine("done");
    }

    public Task<IWebElement> SearchForTable(By by)
    {
        return Task.FromResult(_browser.FindElement(by));
        
    }
    
    /// TODO: Change to Enum if published
    public void SelectTrack(By by, string trackName)
    {
        IWebElement trackSelection = _browser.FindElement(by);
        SelectElement selectElement = new SelectElement(trackSelection);
        selectElement.SelectByText(trackName);
    }

    public async Task ClickCellInTable(string cellValue)
    {
        IWebElement table = await SearchForTable(By.Id("tableCourses"));
        ReadOnlyCollection<IWebElement> rows = table.FindElements(By.TagName("tr"));

        try
        {
            foreach (IWebElement row in rows)
            {
                ReadOnlyCollection<IWebElement> cells = row.FindElements(By.TagName("td"));

                foreach (IWebElement cell in cells)
                {
                    if (cell.Text.Equals(cellValue, StringComparison.OrdinalIgnoreCase))
                    {
                        IWebElement link = cell.FindElement(By.TagName("a"));
                        link.Click();
                        return;
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        
        
    }

    public static bool IsElementPresent(By by, IBrowser browser)
    {
        try
        {
            browser.FindElement(by);
            return true;
        }
        catch (NoSuchElementException)
        {
            return false;
        }
    }
    
    private async Task TypeInBox(By findingMethod, string whatToType, bool shouldWait = false)
    {
        _browser.FindElement(findingMethod).SendKeys(whatToType);
        if (shouldWait)
            await NoSpam.WaitAsync(2);
    }
}