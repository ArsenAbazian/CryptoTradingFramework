using System.Collections.ObjectModel;
using System.Collections.Specialized;

using CommunityToolkit.Mvvm.ComponentModel;

namespace CryptoMarketClient.ViewModels;
public partial class PopupMenuViewModel : ObservableObject
{
	[ObservableProperty] private Actions.ToolbarItemViewModel ownerItem;
	
	public PopupMenuViewModel()
	{
		Items = new ObservableCollection<Actions.ToolbarItemViewModel>();
		Items.CollectionChanged += ItemsOnCollectionChanged;
	}

	private void ItemsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
	{
		if(e.NewItems != null)
		{
			foreach(var item in e.NewItems)
			{
				((Actions.ToolbarItemViewModel)item).OwnerMenu = this;
			}
		}
	}

	public ObservableCollection<Actions.ToolbarItemViewModel> Items { get; }
	public object Tag { get; set; }
}