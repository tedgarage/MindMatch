using UnityEngine;

public class ResourcesManager : Jambav.Utilities.Singleton<ResourcesManager>
{
    #region  PUBLIC METHODS
    public Texture2D GetProductTexture(string zohoProduct)
    {
        string path = string.Format(Constants.ZOHO_PRODUCT_LOGO_PATH, zohoProduct);
        return Resources.Load<Texture2D>(path);
    }
    public Sprite GetProductSprite(string zohoProduct)
    {
        string path = string.Format(Constants.ZOHO_PRODUCT_LOGO_PATH, zohoProduct);
        return Resources.Load<Sprite>(path);
    }

     public Texture2D GetCompetitorTexture(string _name)
    {
        string path = string.Format(Constants.COMPETITOR_PRODUCT_LOGO_PATH, _name);
        return Resources.Load<Texture2D>(path);
    }
    public Sprite GetCompetitorSprite(string _name)
    {
        string path = string.Format(Constants.COMPETITOR_PRODUCT_LOGO_PATH, _name);
        return Resources.Load<Sprite>(path);
    }

    // public Texture2D GetProductSpriteForSelect(ZohoProduct zohoProduct)
    // {
    //     string path = string.Format(Constants.PRODUCT_SPRITE_FOR_SELECT_PATH, zohoProduct.ToStringValue());
    //     return Resources.Load<Texture2D>(path);
    // }
    // public AudioClip GetProductIntegrationAudio(ZohoProduct mainProduct, ZohoProduct integrationProduct)
    // {
    //     string path = string.Format(Constants.INTEGRATION_AUDIO_PATH, mainProduct.ToStringValue(), integrationProduct.ToStringValue());
    //     print(path);
    //     return Resources.Load<AudioClip>(path);
    // }
    // public AudioClip GetInstructionProductAudio(ZohoProduct _product, bool _withZoho)
    // {
    //     string path = string.Format(_withZoho ? Constants.INSTRUCTION_PRODUCT_AUDIO_WITH_ZOHO_PATH : Constants.INSTRUCTION_PRODUCT_AUDIO_WITH_ZOHO_OUT_PATH, _product.ToStringValue());
    //     return Resources.Load<AudioClip>(path);
    // }
    #endregion
}


