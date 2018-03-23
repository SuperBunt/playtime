using System.Collections.Generic;

public class House
{
    public House()
    {
        SoldInArea = new List<string>();
        PriceHistory = new List<string>();
        Features = new List<string>();
        Photos = new List<string>();
    }
    public string Price { get; set; }
    public string Description { get; set; }
    public string Bedrooms { get; set; }
    public string Address { get; set; }
    public string BER { get; set; }
    public string Type { get; set; }
    public string DateEntered { get; set; }
    public List<string> SoldInArea { get; set; }
    public List<string> PriceHistory { get; set; }
    public List<string> Features { get; set; }
    public List<string> Photos { get; set; }
    public string BriefFeatures { get; set; }
    public string MainPhoto { get; set; }

    public override string ToString()
    {
        return string.Format("{0}\n{1}\n{2}", Address, Price, Description);
    }

}