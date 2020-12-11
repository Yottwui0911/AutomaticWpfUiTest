using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

namespace WinAppDriverTest
{
  [TestClass]
  public class WpfSession
  {
    private const string WadUrl = "http://127.0.0.1:4723";

    // 実行したいWPFアプリケーションのPath
    private const string WpfAppId = @"C:xxxxxxxxxxxxxxxxxxxxx\WpfApp6\WpfApp6\bin\Debug\netcoreapp3.1\WpfApp6.exe";

    protected static WindowsDriver<WindowsElement> session;

    [ClassInitialize]
    public static void Setup(TestContext context)
    {
      if (session == null)
      {
        var appiumOptions = new AppiumOptions();
        appiumOptions.AddAdditionalCapability("app", WpfAppId);
        appiumOptions.AddAdditionalCapability("deviceName", "WindowsPC");
        session = new WindowsDriver<WindowsElement>(new Uri(WadUrl), appiumOptions);
      }
    }

    [TestInitialize]
    public void Clear()
    {
      var nameTxtBox = session.FindElementByAccessibilityId("Name");
      nameTxtBox.Clear();
    }

    [TestMethod]
    public void RegisterMaleTv()
    {
      Thread.Sleep(1000);
      var nameTxtBox = session.FindElementByAccessibilityId("Name");
      nameTxtBox.SendKeys("Yottwui");
      Thread.Sleep(1000);

      var sexRadio = session.FindElementByAccessibilityId("Male");
      sexRadio.Click();

      var reasonComboBox = session.FindElementByAccessibilityId("Reason");
      reasonComboBox.Click();
      reasonComboBox.FindElementByName("[1, テレビで見た]").Click();
      Thread.Sleep(1000);

      var registerBtn = session.FindElementByAccessibilityId("RegisterBtn");
      registerBtn.Click();

      var outputTxtBlock = session.FindElementByAccessibilityId("Output");
      Assert.AreEqual(outputTxtBlock.Text.Replace("\r\n", ""), $"登録者名： Yottwui\n性別： 男性\n登録する経緯： テレビで見た".Replace("\n", ""));

      Thread.Sleep(1000);
    }

    [TestMethod]
    public void RegisterFemaleSns()
    {
      Thread.Sleep(1000);
      var nameTxtBox = session.FindElementByAccessibilityId("Name");
      nameTxtBox.SendKeys("Peach");
      Thread.Sleep(1000);

      var sexRadio = session.FindElementByAccessibilityId("Female");
      sexRadio.Click();

      var reasonComboBox = session.FindElementByAccessibilityId("Reason");
      reasonComboBox.Click();
      reasonComboBox.FindElementByName("[3, SNSで見た]").Click();
      Thread.Sleep(1000);

      var registerBtn = session.FindElementByAccessibilityId("RegisterBtn");
      registerBtn.Click();

      var outputTxtBlock = session.FindElementByAccessibilityId("Output");
      Assert.AreEqual(outputTxtBlock.Text.Replace("\r\n", ""), $"登録者名： Peach\n性別： 女性\n登録する経緯： SNSで見た".Replace("\n", ""));

      Thread.Sleep(1000);
    }

    [ClassCleanup]
    public static void ClassCleanup()
    {
      session.Close();
    }
  }
}
