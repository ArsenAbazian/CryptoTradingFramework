using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using Eremex.AvaloniaUI.Controls.Docking;

namespace CryptoMarketClient.Utils;

public partial class DocumentManager : ObservableObject
{
    [ObservableProperty] private ObservableCollection<IViewDocument> documents;

    public DocumentManager(DockManager manager, IToolbarController toolbarController)
    {
        _manager = manager;
        _toolbarController = toolbarController;
        documents = new ObservableCollection<IViewDocument>();
        documents.CollectionChanged += DocumentsOnCollectionChanged;
        _manager.DockItemActivated += ManagerOnDockItemActivated;
    }

    private void ManagerOnDockItemActivated(object sender, DockItemActivatedEventArgs e)
    {
        var prev = GetDocument(e.OldItem);
        var curr = GetDocument(e.NewItem);
        if(prev != null)
        {
            _toolbarController.RemoveClient(prev.Toolbars);
        }   
        if(curr != null)
        {
            _toolbarController.AddClient(curr.Toolbars);
            DocumentActivated?.Invoke(curr);
        }
    }

    protected IViewDocument GetDocument(DockItemBase item)
    {
        if(item is DocumentPane pane && pane.Content is Control c && c.DataContext is IViewDocument doc)
            return doc;
        return null;
    }
    
    private void DocumentsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        if(e.OldItems != null)
        {
            foreach(var item in e.OldItems)
            {
                OnDocumentRemoved((IViewDocument)item);
                
            }
        }
        
        if(e.NewItems != null)
        {
            foreach(var item in e.NewItems)
            {
                OnDocumentAdded((IViewDocument)item);
            }
        }
    }

    private void OnDocumentRemoved(IViewDocument item)
    {
        item.OnDetached();
        DocumentRemoved?.Invoke(item);
    }

    private void OnDocumentAdded(IViewDocument item)
    {
        var container = CreateDocument(item);
        item.OnAttached(container);
        Activate(item);
        DocumentAdded?.Invoke(item);
    }

    private Control CreateDocument(IViewDocument item)
    {
        Control control = new ViewLocator().Build(item);
        control.DataContext = item;
        DocumentPane pane = new DocumentPane() { Header = item.Name };
        pane.Content = control;
        var group = _manager.GetItems().OfType<DocumentGroup>().FirstOrDefault(dg => dg.Name == "MainDocumentGroup");
        group?.Add(pane);
        return control;
    }

    public Action<IViewDocument> DocumentAdded;
    public Action<IViewDocument> DocumentRemoved;
    public Action<IViewDocument> DocumentActivated;
    
    private DockManager _manager;
    private readonly IToolbarController _toolbarController;

    public void Activate(IViewDocument document)
    {
        if(_manager.GetItems().OfType<DocumentPane>().FirstOrDefault(dp => ((Control)dp.Content)?.DataContext == document) is DocumentPane c)
            c.IsActive = true;
    }
}

public interface IViewDocument
{
    string Name { get; }
    void OnAttached(object view);
    void OnDetached();
    ToolbarManagerViewModel Toolbars { get; }
}
