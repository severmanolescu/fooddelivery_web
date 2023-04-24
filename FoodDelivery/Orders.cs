using System;
using System.Collections.Generic;

public class Orders
{
    public List<Data> orders;
}

public class Data
{
    public DateTime Date { get; set; }
    public List<string> Items { get; set; }
    public string Status { get; set; }
}