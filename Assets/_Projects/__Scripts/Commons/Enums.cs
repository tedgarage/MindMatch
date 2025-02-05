using System;

// public enum UsingZohoProduct
// {
//     None,
//     Projects, =16
//     Mail, = 12
//     Books, = 21
//     Desk, =10
//     People, =20
//     CRM, = 1
// }



public enum ZohoProduct
{
    None,
    CRM,
    Campaigns,
    Sign,
    Social,
    SalesIQ,
    Backstage,
    Survey,
    Commerce,
    Forms,
    Desk,
    Assist,
    Mail,
    Cliq,
    Meeting,
    Workdrive,
    Projects,
    Calendar,
    Sheet,
    Recruit,
    People,
    Books,
    Inventory,
    Checkout,
    Expense,
    Invoice,
    Subscriptions,
    Vault,
    Creator,
    Flow,
    Apptics,
    Analytics,
    Payroll,
    Directory,
    One
}
public enum ZohoProductColor
{
    Red,
    Green,
    Blue,
    Yellow
}


[Serializable]
public enum GameState
{
    None,
    Home,
    Tutorial,
    GamePlay,
    EndGame
}
public enum ProductType
{
    Normal,
    HideLogo,
    HideName
}
public enum ProductPlacementState
{
    None,
    TimeUp,
    Dismissed,
    BeganToShow,
    Shown
}