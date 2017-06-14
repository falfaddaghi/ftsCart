namespace FtsCart
type Id =int
type Name=string
type Price=float
type Item=
    {
        id:Id
        name:Name
        price:Price
    }
type ActiveCartData=
    {
        items:Item list
        id:Id
    }
type BoughtCartData=
    {
        items:Item list
        id:Id
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
        cart.items|>List.sumBy(fun item->item.price)
    let addItemToEmptyCart (c:EmptyCartData) item=
        ActiveCart{id=c.id;items=[item]}
    let addItemToActiveCart (ac:ActiveCartData) item=
        ActiveCart{ac with items=item::ac.items}       
    let removItemFromActiveCart (ac:ActiveCartData) item=
        let newItems=ac.items|>List.filter(fun x->x=item)
        ActiveCart{ac with items=newItems}
    let BuyItemsOfActiveCart (ac:ActiveCartData) =
        let items=ac.items
        let totalPrice=calculateTotalPriceActiveCart ac
        let price=calculateTotalPriceActiveCart ac
        BoughtCart{id=ac.id;items=ac.items;totalPrice=price}
     
    