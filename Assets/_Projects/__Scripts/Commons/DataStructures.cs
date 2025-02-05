using System;

[Serializable]
public struct BoxProductData
{
    public ZohoProduct MainProduct;
    public ZohoProduct BaseProduct;
    public ZohoProduct[] OtherProductList; // 0th index - (0) correct Rotation, 1st index - (-90) Rotation, 2nd index - (-180) Rotation, 3rd index - (90) rotation

}