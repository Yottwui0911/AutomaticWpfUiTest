using System.Collections.Generic;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;

namespace WpfApp6
{
  public class MainViewModel : BindableBase
  {
    public MainViewModel()
    {
      this._reason = new Dictionary<int, string>
      {
        {1,"テレビで見た" },
        {2,"メルマガが届いた" },
        {3,"SNSで見た" },
        {4,"新聞で見た" },
        {5,"雑誌で見た" },
        {6,"その他" },
      };

      this._sexDic = new Dictionary<Enums.Sex, string>
      {
        { Enums.Sex.Male , "男性"},
        { Enums.Sex.Female , "女性"},
        { Enums.Sex.Neither , "どちらでもない"},
        { Enums.Sex.Other , "その他"},
      };

      this.Sex = Enums.Sex.Male;
    }

    private IDictionary<Enums.Sex, string> _sexDic;
    private string _name;

    public string Name
    {
      get => this._name;
      set => this.SetProperty(ref this._name, value);
    }

    private Enums.Sex _sex;

    public Enums.Sex Sex
    {
      get => this._sex;
      set => this.SetProperty(ref this._sex, value);
    }

    private IDictionary<int, string> _reason;

    public IDictionary<int, string> Reason
    {
      get => this._reason;
    }

    private int _selectedReason;

    public int SelectedReason
    {
      get => this._selectedReason;
      set => this.SetProperty(ref this._selectedReason, value);
    }

    private ICommand _registerCommand;

    public ICommand RegiseterCommand
    {
      get
      {
        if (this._registerCommand == null)
        {
          this._registerCommand = new DelegateCommand(() => this.OnRegister());
        }

        return this._registerCommand;
      }
    }

    private void OnRegister()
    {
      // 例外処理は省略
      this.Output = $"登録者名： {this.Name}\n性別： {this._sexDic[this.Sex]}\n登録する経緯： {this.Reason[this.SelectedReason]}";
    }

    public string _output;

    public string Output
    {
      get => this._output;
      set => this.SetProperty(ref this._output, value);
    }
  }
}
