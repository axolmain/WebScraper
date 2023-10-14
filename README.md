![Web Scraper logo](DALL·E%20Spider%20Logo.png)

# Overview

In order to improve both my skills in web scraping/automation and C# project design, 
I built a web scraper using Selenium WebDriver and C# to interact with my school's 
registration portal, inject the class/professor whose course I want to sign up for, 
and navigate through the portal to automatically enroll me. This project not only serves 
as a practical application of my learning but also aims to build a foundation for more 
complex automation tasks in the future (and saves me time/effort when I have to register for 
classes every semester in university). 

[Software Demo Video](http://youtube.link.goes.here)

# Development Environment

The software was developed using:

- The C# programming language, an object-oriented programming language.
- Selenium WebDriver, a tool for controlling a web browser programmatically and automating browser actions.
  - FireFox Webdriver was the specific webdriver used with the Selenium package.
- Rider, a .NET IDE by JetBrains, for writing and debugging the code.

# Useful Websites

The following websites were instrumental in the development of this project:

- [Selenium WebDriver Documentation](https://www.selenium.dev/documentation/en/)
- [ChatGPT](https://chat.openai.com/) for troubleshooting and help deciphering error stacks.
- [Stack Overflow](https://stackoverflow.com/) 
- [Phind.com](phind.com) An AI powered, developer focused, search engine

# Future Work

Future goals for enhancing the functionality and efficiency of this web scraper are:

- Incorporating a more robust error handling and logging system.
- Adding support for navigating/signing you up for multiple classes at once.
- Adding error handling if your desired class is not found/full
  - i.e. add the user to the waitlist, then enroll for a class in the same time period with a professor with a similar "RateMyProfessor" score
- Adding capabilities to handle dynamic websites with AJAX-loaded content.
- Adding unit tests to ensure the reliability and correctness of the code as it evolves.