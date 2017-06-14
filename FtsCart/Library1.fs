namespace FtsCart
type Id =int
type Name=string
type Price=float
type ShippingInfo=
    {
        shippingAddress:string
        shippingCost:Price
    }
type Item=
    {
        id:Id
        name:Name
        price:Price
        quantity:int
    }
type ActiveCartData=
    {
        id:Id
        items:Item list
        shippingInfo:ShippingInfo option
    }
type BoughtCartData=
    {
        id:Id
        items:Item list
        totalPrice:float
    }
type EmptyCartData=
    {
        id:Id
    }
type UserCart=
|EmptyCart of EmptyCartData
|ActiveCart of ActiveCartData
|BoughtCart of BoughtCartData 
 module transition=
    let calculateTotalPriceActiveCart (cart:ActiveCartData) =
        cart.items|>List.sumBy(fun item->float item.price* float item.quantity)
    let addItemToEmptyCart (c:EmptyCartData) item=
        ActiveCart{id=c.id;items=[item]}
    let addItemToActiveCart (ac:ActiveCartData) item=
        ActiveCart{ac with items=item::ac.items}       
    let removItemFromActiveCart (ac:ActiveCartData) itemId=
        let newItems=ac.items|>List.filter(fun x->x.id=itemId)
        ActiveCart{ac with items=newItems}
    let BuyItemsOfActiveCart (ac:ActiveCartData) =
        let items=ac.items
        let totalPrice=calculateTotalPriceActiveCart ac
        let price=calculateTotalPriceActiveCart ac
        BoughtCart{id=ac.id;items=ac.items;totalPrice=price}
     
    