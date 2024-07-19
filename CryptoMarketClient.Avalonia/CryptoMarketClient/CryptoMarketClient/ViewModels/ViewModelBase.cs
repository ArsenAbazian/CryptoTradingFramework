using CommunityToolkit.Mvvm.ComponentModel;
using CryptoMarketClient.Utils;

namespace CryptoMarketClient.ViewModels;

public abstract partial class ViewModelBase : ObservableObject
{
    [ObservableProperty] private ToolbarManagerViewModel toolbars;
    [ObservableProperty] private IToolbarController toolbarController;

    private DocumentManager _documentManager;
    public ViewModelBase(DocumentManager documentManager, IToolbarController toolbarController)
    {
        _documentManager = documentManager;
        this.toolbarController = toolbarController;
    }

    protected DocumentManager DocumentManager => _documentManager;
    protected abstract ToolbarManagerViewModel CreateToolbars();

    public virtual void OnAttached(object view)
    {
        UpdateToolbars();
    }

    public virtual void OnDetached()
    {

    }

    protected void UpdateToolbars()
    {
        if(Toolbars == null)
            Toolbars = CreateToolbars();
    }
}