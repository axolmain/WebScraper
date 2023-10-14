using OpenQA.Selenium;

namespace WebScraper;

public interface IScraper
{
    Task LoginAndScrapeAsync(string course, string professor, string track = "Winter Semester 2024");
    
    Task<IWebElement> SearchForTable(By by);

    Task ClickCellInTable(string cellValue);
}