using System;
using System.Collections.Generic;

namespace CryptoMarketClient.Models;

public class SaveFileEventArgs : EventArgs
{
    public List<string> Files { get; set; } = new List<string>();
    public bool Cancel { get; set; }
}