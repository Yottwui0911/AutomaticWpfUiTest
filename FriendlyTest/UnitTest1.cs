using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows;
using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RM.Friendly.WPFStandardControls;

namespace FriendlyTest
{
  [TestClass]
  public class UnitTest1
  {
    private static WindowsAppFriend _app;

    // 実行したいWPFアプリケーションのPath
    private const string WpfAppId = @"C:xxxxxxxxxxxxxxxxxxx\\WpfApp6\WpfApp6\bin\Debug\netcoreapp3.1\WpfApp6.exe";

    [ClassInitialize]
    public static void Setup(TestContext context)
    {
      //attach to target process!
      var path = Path.GetFullPath(WpfAppId);
      _app = new WindowsAppFriend(Process.Start(path));
    }

    [ClassCleanup]
    public static void ClassCleanup()
    {
      Process process = Process.GetProcessById(_app.ProcessId);
      _app.Dispose();
      process.CloseMainWindow();
    }

    [TestInitialize]
    public void Clear()
    {
      dynamic window = _app.Type<Application>().Current.MainWindow;
      var nameTextBox = new WPFTextBox(window.Name);
      nameTextBox.EmulateChangeText(string.Empty);

      var sexRadio = new WPFButtonBase(window.Male);
      sexRadio.EmulateClick();

      var reasonComboBox = new WPFComboBox(window.Reason);
      reasonComboBox.EmulateChangeSelectedIndex(-1);
    }

    [TestMethod]
    public void RegisterMaleTv()
    {
      Thread.Sleep(1000);
      dynamic window = _app.Type<Application>().Current.MainWindow;
      var nameTextBox = new WPFTextBox(window.Name);
      nameTextBox.EmulateChangeText("Yottwui");
      Thread.Sleep(1000);

      var sexRadio = new WPFButtonBase(window.Male);
      sexRadio.EmulateClick();

      Thread.Sleep(1000);

      var reasonComboBox = new WPFComboBox(window.Reason);
      reasonComboBox.EmulateChangeSelectedIndex(0);

      Thread.Sleep(1000);

      var registerBtn = new WPFButtonBase(window.RegisterBtn);
      registerBtn.EmulateClick();

      var outputTxtBlock = new WPFTextBlock(window.Output);
      Assert.AreEqual(outputTxtBlock.Text.Replace("\n", ""), $"登録者名： Yottwui\n性別： 男性\n登録する経緯： テレビで見た".Replace("\n", ""));

      Thread.Sleep(1000);
    }

    [TestMethod]
    public void RegisterFemaleSns()
    {
      Thread.Sleep(1000);
      dynamic window = _app.Type<Application>().Current.MainWindow;
      var nameTextBox = new WPFTextBox(window.Name);
      nameTextBox.EmulateChangeText("Peach");
      Thread.Sleep(1000);

      var sexRadio = new WPFButtonBase(window.Female);
      sexRadio.EmulateClick();

      Thread.Sleep(1000);

      var reasonComboBox = new WPFComboBox(window.Reason);
      reasonComboBox.EmulateChangeSelectedIndex(2);

      Thread.Sleep(1000);

      var registerBtn = new WPFButtonBase(window.RegisterBtn);
      registerBtn.EmulateClick();

      var outputTxtBlock = new WPFTextBlock(window.Output);
      Assert.AreEqual(outputTxtBlock.Text.Replace("\n", ""), $"登録者名： Peach\n性別： 女性\n登録する経緯： SNSで見た".Replace("\n", ""));

      Thread.Sleep(1000);
    }
  }
}
