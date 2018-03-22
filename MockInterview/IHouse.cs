using System.Collections.Generic;

interface IHouse
{
    string Address { get; set; }
    string Bedrooms { get; set; }
    string BER { get; set; }
    string BriefFeatures { get; set; }
    string DateEntered { get; set; }
    string Description { get; set; }
    List<string> Features { get; set; }
    string Price { get; set; }
    List<string> PriceHistory { get; set; }
    List<string> SoldInArea { get; set; }
    string Type { get; set; }
    string MainPhoto { get; set; }

    string ToString();
}