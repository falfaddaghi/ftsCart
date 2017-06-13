// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.
open System
open Xunit
open FtsCart
open FtsCart.transition

[<Fact>]
let ``test`` () =
    let x={id=1;name="hello";price=10}
    Assert.Equal(x.id,1)
[<Fact>]
let ``adding to and empty cart give gives correct result``()=
    let cart={id=1;items=[]}
    let item={id=1;name="item";price=10}
    let newCart=addItem cart item
    Assert.Equal(newCart.items.Head,item)
    Assert.Equal(newCart.items.Length,1)
[<Fact>]
let ``add item to  non empty cart gives correct result``()=
    let item1={id=1;name="item1";price=10}
    let item2={id=2;name="item2";price=20}
    let cart={id=2;items=[item1]}
    let newCart=addItem cart item2
    Assert.Equal(newCart.items.Head,item2)
    Assert.Equal(newCart.items.Length,2)
[<Fact>]
let `` buy items in the non empty cart gives empty cart`` ()=
    let item1={id=1;name="item1";price=10}
    let item2={id=2;name="item2";price=20}
    let cart={id=3;items=[item1;item2]}
    let newCart=buyItems cart
    Assert.Equal(newCart.items.Length,0)
[<Fact>]
let `` buyitems from empty cart should'nt be allowed``()=
    let cart={id=4;items=[]}
    let newCart=buyItems cart
    Assert.NotEqual(cart.items.Length,0)

(*
requirements to be added.
- calculate total
- add quantities 
- remove item from a cart
- presist shopping cart (save cart and retrieve it by userID (current)  
    - rules 
        - (no two aactive shopping carts at the same time for the same user)        
    
- history (previous orders)
- shipping (add shipping address, cost(this is a fixed amount for now))

*)    
