using System;
using System.Collections.Generic;

public class Orders
{
    public List<Data> orders;
}

public class Data
{
    public int index;
    public string address;
    public DateTime date;
    public List<Item> items;
    public string person;
    public string phone;
    public string status;
}

public class Item
{
    public int amount;
    public string name;
}

public class OrderDetails
{
    public Data data;
    public string restaurantID;
}