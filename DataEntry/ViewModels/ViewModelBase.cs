using ReactiveUI;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace DataEntry.ViewModels;

public abstract class ViewModelBase : ReactiveObject {
    public TRet RaiseAndSetIfChangedAndSave<TRet>(ref TRet backingField, TRet newValue,
        [CallerMemberName] string? propertyName = null)
    {
        bool mustSave = !EqualityComparer<TRet>.Default.Equals(backingField, newValue);
        this.RaiseAndSetIfChanged(ref backingField, newValue, propertyName);
        if (mustSave) {
            save();
        }
        return backingField;
    }

    protected abstract void save();
}
