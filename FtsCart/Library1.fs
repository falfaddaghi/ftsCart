namespace FtsCart
type Id =int
type Name=string
type Price=int
type Item=
    {
        id:Id
        name:Name
        price:Price
    }
type Cart=
    {
        items:Item list
        id:Id
    }

 module transition=
    let addItem cart item =
        let items=item::cart.items
        {cart with items=items}
    let buyItems cart=
        {cart with items=[]}


