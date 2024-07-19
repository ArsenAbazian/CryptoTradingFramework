using System;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Markup.Xaml;

namespace CryptoMarketClient.Utils;

public class DataTemplateSource : IDataTemplate
{
    private readonly Uri baseUri;
    private DataTemplates templates;
    private bool isLoading;
	
    public DataTemplateSource(Uri baseUri)
    {
        this.baseUri = baseUri;
    }

    public DataTemplateSource(IServiceProvider serviceProvider)
    {
        baseUri = ((IUriContext)serviceProvider.GetService(typeof(IUriContext)))?.BaseUri;
    }

    public Uri Source { get; set; }

    public DataTemplates Templates
    {
        get
        {
            if (templates == null)
            {
                isLoading = true;
                templates = (DataTemplates)AvaloniaXamlLoader.Load(Source, baseUri);
                isLoading = false;
            }

            return templates;
        }
    }

    public bool Match(object data)
    {
        if (isLoading || Templates == null)
        {
            return false;
        }

        return Templates.Any(dt => dt.Match(data));
    }

    public Control Build(object data)
    {
        if (isLoading || Templates == null)
        {
            return null;
        }

        return Templates.FirstOrDefault(dt => dt.Match(data))?.Build(data);
    }
}