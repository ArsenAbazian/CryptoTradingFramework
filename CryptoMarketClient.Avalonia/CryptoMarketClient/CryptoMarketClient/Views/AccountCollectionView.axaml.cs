using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Platform.Storage;
using CryptoMarketClient.Models;
using CryptoMarketClient.Utils;
using CryptoMarketClient.ViewModels;
using Eremex.AvaloniaUI.Controls.Common;

namespace CryptoMarketClient.Views;

public partial class AccountCollectionView : MxWindow
{
    public AccountCollectionView()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }

    protected AccountCollectionViewModel viewModel;
    protected override void OnDataContextChanged(EventArgs e)
    {
        base.OnDataContextChanged(e);
        if(viewModel != null)
            UnsubscribeEvents(viewModel);
        viewModel = DataContext as AccountCollectionViewModel;
        if(viewModel != null)
            SubscribeEvents(viewModel);
    }

    private void SubscribeEvents(AccountCollectionViewModel vm)
    {
        vm.RequestSaveFile += VmOnRequestSaveFile;
        vm.RequestOpenFile += VmOnRequestOpenFile;
    }

    private void VmOnRequestOpenFile(object sender, SaveFileEventArgs e)
    {
        FilePickerOpenOptions opt = new FilePickerOpenOptions();
        opt.AllowMultiple = false;
        var xml = new FilePickerFileType("Xml Files");
        var all = new FilePickerFileType("All Files");
        xml.Patterns = new[] { "*.xml" };
        all.Patterns = new[] { "*.*" };
        opt.FileTypeFilter = new[] { xml, all };
        IReadOnlyList<IStorageFile> files = UIThreadAsyncHelper.WaitForUIThreadTask(StorageProvider.OpenFilePickerAsync(opt));
        if(files.Count > 0)
            e.Files.Add(files[0].Path.AbsolutePath);
        else
            e.Cancel = true;
    }

    private void VmOnRequestSaveFile(object sender, SaveFileEventArgs e)
    {
        FilePickerSaveOptions opt = new FilePickerSaveOptions();
        opt.DefaultExtension = "xml";
        opt.ShowOverwritePrompt = true;
        opt.SuggestedFileName = "Accounts.xml";
        var xml = new FilePickerFileType("Xml Files");
        var all = new FilePickerFileType("All Files");
        xml.Patterns = new[] { "*.xml" };
        all.Patterns = new[] { "*.*" };
        opt.FileTypeChoices = new[] { xml, all };
        IStorageFile file = UIThreadAsyncHelper.WaitForUIThreadTask(StorageProvider.SaveFilePickerAsync(opt));
        if(file != null)
            e.Files.Add(file.Path.AbsolutePath);
        else
            e.Cancel = true;
    }

    private void UnsubscribeEvents(AccountCollectionViewModel vm)
    {
        vm.RequestSaveFile -= VmOnRequestSaveFile;
        vm.RequestOpenFile -= VmOnRequestOpenFile;
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}