using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace XeroApplication
{
    [TestClass]
    public class xeroTest1
    {
        string testNumber = string.Empty;
        string urlString = string.Empty;
        IWebDriver driver;

        [TestMethod]
        public void xeroTestMethod1()
        {

            testNumber = "XeroTest1";
            Boolean isNumber = false;
            int bankAccountInt = 0;
            string countryValue = string.Empty;


            const string cSallysOrganisation = "Sallys Organisation";
            const string cUrlTitle = "https://www.xero.com/nz/";
            const string cUrlMyXero = "https;//www.xero.com/nz/myxero";
            const string cUrlOrganisations = "urlOrganisations";
            const string cNZBankAccount = "123456789";
            const string cCountryNZ = "NZ";

            try
            {
                // Initialise - start browser and open Xero website
                driver = new ChromeDriver("C:/");
                driver.Navigate().GoToUrl(cUrlTitle);
                driver.Manage().Window.Maximize();

                // Click the list Icon.
                IWebElement listIcon = driver.FindElement(IdentifierAndFunctions.Homepage.listIcon());
                Assert.IsTrue(listIcon.Displayed, "No List Icon is displayed for the Xero website");
                listIcon.Click();

                // Select My Xero.
                IWebElement myXero = driver.FindElement(IdentifierAndFunctions.Homepage.myXero());
                driver.Navigate().GoToUrl(cUrlMyXero);
                Assert.IsTrue(driver.Url.Contains(myXero.ToString()));

                // Add Sally Organisation.
                IWebElement organisationTextBox = driver.FindElement(IdentifierAndFunctions.Homepage.GetTextBox());
                Assert.IsTrue(organisationTextBox.Displayed, "No organisation text box is displayed for the xero website");
                Assert.IsTrue(organisationTextBox.Equals(cSallysOrganisation), "The new organisation " + cSallysOrganisation + "has not been successfully added");

                // Navigate to Organisation page.
                driver.Navigate().GoToUrl(cUrlOrganisations);

                // For each Organisation add the NZ Bank Account.
                IList<IWebElement> organisationList = driver.FindElements(IdentifierAndFunctions.Homepage.Organisations());
                Assert.IsTrue(organisationList.Count > 0, "No organisations have been returned for the Xero websited");

                for (int i = 0; i < organisationList.Count; i++)
                {
                        IWebElement bankAccountCountryTextBox = driver.FindElement(IdentifierAndFunctions.Homepage.BankAccountTexBox());
                        Assert.IsTrue(bankAccountCountryTextBox.Displayed, "No Bank Account text box is displayed for the Xero website");
                        bankAccountCountryTextBox.SendKeys(cCountryNZ);
                        countryValue = bankAccountCountryTextBox.Text;
                        Assert.IsTrue(countryValue == cCountryNZ, "Country is not set to NZ for the Xero website");

                        IWebElement bankAccountTextBox = driver.FindElement(IdentifierAndFunctions.Homepage.BankAccountTexBox());
                        Assert.IsTrue(bankAccountTextBox.Displayed, "No Bank Account text box is displayed for the Xero website");

                        bankAccountTextBox.SendKeys(cNZBankAccount);
                        bankAccountInt = Convert.ToInt32(bankAccountTextBox.Text);

                        foreach (char value in cNZBankAccount)
                        {
                            isNumber = char.IsNumber(value);
                            if (isNumber)
                            {
                                break;
                            }
                        }
                        Assert.IsTrue(isNumber, "The Bank account is not a numeric value");
                    }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
            finally
            {
                //Cleanup
                driver.Dispose();
                driver.Close();
            }
        }
    }
}

namespace IdentifierAndFunctions
{
    class Homepage
    {
        internal static By BankAccountTexBox()
        {
            //return By.ClassName(BankAccountTexBox);
            throw new NotImplementedException();
        }

        internal static By GetTextBox()
        {
            //return By.Id(GetTextBox);
            throw new NotImplementedException();
        }

        internal static By listIcon()
        {
            return By.LinkText("somethinginthelist");
            throw new NotImplementedException();
        }

        internal static By myXero()
        {
            throw new NotImplementedException();
        }

        internal static By Organisations()
        {
            throw new NotImplementedException();
        }
    }
}
//}




