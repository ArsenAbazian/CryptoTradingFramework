using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using CommunityToolkit.Mvvm.ComponentModel;
using CryptoMarketClient.ViewModels;
using DynamicData;

namespace CryptoMarketClient.Utils;

public partial class ToolbarController : ObservableObject, IToolbarController
{
    [ObservableProperty] private ObservableCollection<ToolbarViewModel> toolbars;
    [ObservableProperty] private ObservableCollection<ToolbarViewModel> topToolbars;
    [ObservableProperty] private ObservableCollection<ToolbarViewModel> bottomToolbars;

    [ObservableProperty] private ObservableCollection<ToolbarManagerViewModel> clients;

    public ToolbarController()
    {
        clients = new ObservableCollection<ToolbarManagerViewModel>();
        clients.CollectionChanged += ClientsOnCollectionChanged;

        toolbars = new ObservableCollection<ToolbarViewModel>();
        topToolbars = new ObservableCollection<ToolbarViewModel>();
        bottomToolbars = new ObservableCollection<ToolbarViewModel>();
    }

    private void ClientsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        RebuildMerge();
    }

    private void RebuildMerge()
    {
        Toolbars.Clear();
        TopToolbars.Clear();
        BottomToolbars.Clear();

        TopToolbars.AddRange(MergeToolbars(GetToolbars(c => c.TopToolbars)));
        BottomToolbars.AddRange(MergeToolbars(GetToolbars(c => c.BottomToolbars)));
        Toolbars.AddRange(MergeToolbars(GetToolbars(c => c.Toolbars)));
    }

    private IEnumerable<ToolbarViewModel> MergeToolbars(List<ToolbarViewModel> src)
    {
        // Right now there is no need to replace toolbars...
        // may be later
        return src;
    }

    List<ToolbarViewModel> GetToolbars(Func<ToolbarManagerViewModel, ObservableCollection<ToolbarViewModel>> m)
    {
        List<ToolbarViewModel> res = new List<ToolbarViewModel>();
        foreach(var c in Clients)
        {
            res.AddRange(m(c));
        }

        return res;
    }
    
    public void AddClient(ToolbarManagerViewModel client)
    {
        if(client == null)
            return;
        if(Clients.Contains(client))
            return;
        Clients.Add(client);
    }

    public void RemoveClient(ToolbarManagerViewModel client)
    {
        Clients.Remove(client);
    }
}

public interface IToolbarController
{
    ObservableCollection<ToolbarViewModel> Toolbars { get; }
    ObservableCollection<ToolbarViewModel> TopToolbars { get; }
    ObservableCollection<ToolbarViewModel> BottomToolbars { get; }

    void AddClient(ToolbarManagerViewModel client);
    void RemoveClient(ToolbarManagerViewModel client);
}