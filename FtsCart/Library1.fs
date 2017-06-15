namespace FtsCart
type Id =int
type Name=string
type Price=float
type ShippingInfo=
    {
        shippingAddress:string
        shippingCost:Price
    }
type EmptyCartData=
    {
        id:Id
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
        shippingInfo:ShippingInfo 
    }
type UserCart=
|EmptyCart of EmptyCartData
|ActiveCart of ActiveCartData
|BoughtCart of BoughtCartData 
type User=
    {
        id:Id
        currentCart:UserCart
        history:BoughtCartData list

    }
 module transition=
    let calculateTotalPriceWithShipping  (shippingCost:float) (itemsCost:float) :float=
        itemsCost+shippingCost
    let calculatePriceOfItems (cart:ActiveCartData) =
        cart.items|>List.sumBy(fun item->float item.price* float item.quantity)

    let addItemToEmptyCart (c:EmptyCartData) item=
        ActiveCart{id=c.id;items=[item];shippingInfo=None}

    let addItemToActiveCart (ac:ActiveCartData) item=
        ActiveCart{ac with items=item::ac.items}       

    let removItemFromActiveCart (ac:ActiveCartData) itemId=
        let newItems=ac.items|>List.filter(fun x->x.id<>itemId)
        if newItems.Length =0 then
            EmptyCart{id=ac.id}
        else
        ActiveCart{ac with items=newItems}

    let BuyItemsOfActiveCart (ac:ActiveCartData) =
        match ac.shippingInfo with
        |None->ActiveCart ac
        |Some s->
            let items=ac.items
            let totalPrice=
                ac
                |>calculatePriceOfItems
                |>calculateTotalPriceWithShipping s.shippingCost
            BoughtCart{id=ac.id;items=ac.items;totalPrice=totalPrice;shippingInfo=s}

    let changeShippingInfoOfActiveCart (ac:ActiveCartData) (info:ShippingInfo)=
        {ac with shippingInfo=Some info}
    let changeItemQuantity q (item:Item )=
        if q>0 then
            {item with quantity=q} 
        else
            item
    let incrementQuantity (item:Item)=
        {item with quantity=item.quantity+1}
    let decrementQuantity (item:Item)=
        if item.quantity > 1 then
            {item with quantity=item.quantity-1}
        else 
            item
    let addActiveCartToUser (usr:User) (cart:ActiveCartData)=
        {usr with currentCart=ActiveCart cart}

    let addEmptyToUser (usr:User) (cart:EmptyCartData)=
        {usr with currentCart=EmptyCart cart}

        
        
     
    