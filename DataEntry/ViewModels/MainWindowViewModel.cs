using ReactiveUI;
using System;
using System.Collections.ObjectModel;

namespace DataEntry.ViewModels;

public partial class MainWindowViewModel : ViewModelBase {
    ObservableCollection<string> _currentInfoSource;
    TestJson _info;
    InfoDatabase<TestJson> _entityDB;

    // Pick the Entity

    public ObservableCollection<string> CurrentInfoSource {
        get => _currentInfoSource;
        set => RaiseAndSetIfChangedAndSave(ref _currentInfoSource, value);
    }

    public int CurrentInfoSelection {
        get => _currentInfoSelection;
        set {
            _currentInfoSelection = value;
            _info = _entityDB.GetInfo(value);
            refresh();
        }
    }

    public string CurrentInfo => _info.FileID;

    int _currentInfoSelection = 0;

    // Create new entity

    public string NewInfoName {
        get => _newInfoName;
        set => this.RaiseAndSetIfChanged(ref _newInfoName, value);
    }

    public void OnCreateNew() {
        if (_newInfoName.IsNil()) {
            Console.WriteLine("ERROR: Invalid new shop name");
            return;
        }

        _info = new TestJson() {
            Name = _newInfoName,
        };
        _entityDB.SaveNew(_info, _newInfoName);
        _currentInfoSource.Add(_info.FileID);
        _currentInfoSelection = _entityDB.NumInfos - 1;
        refresh();
    }

    string _newInfoName = string.Empty;

    // Name

    public string Name {
        get => _info.Name;
        set => RaiseAndSetIfChangedAndSave(ref _info.Name, value);
    }

    // Value

    public int Value {
        get => _info.Value;
        set => RaiseAndSetIfChangedAndSave(ref _info.Value, value);
    }

    // Constructors

    public MainWindowViewModel() {
        _entityDB = new InfoDatabase<TestJson>(Config.TestJsonFolder);
        _entityDB.CreateInitialEntryIfNoneExist("empty");
        _info = _entityDB.GetInfo(_currentInfoSelection);
        _currentInfoSource = new ObservableCollection<string>(Array.ConvertAll(_entityDB.Infos, i => i.FileID));
    }

    // Private and protected

    void refresh() {
        this.RaisePropertyChanged(nameof(Name));
        this.RaisePropertyChanged(nameof(Value));
        this.RaisePropertyChanged(nameof(CurrentInfo));
    }

    protected override void save() { }
}
