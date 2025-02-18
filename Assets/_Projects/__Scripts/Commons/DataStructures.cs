using System;
using System.Collections.Generic;

[Serializable]
public struct ProductData
{
    public ZohoProduct Product;
    public string TagLine;
    public List<string> CompetitorProductsList;
    

    public ProductData(ZohoProduct _product, List<string> _competitors, string _tagLine) : this()
    {
        this.Product = _product;
        this.CompetitorProductsList = _competitors;
        TagLine = _tagLine;
    }
    public string GetMainProductName()
    {
        return Product.ToStringValue();
    }
    

}

