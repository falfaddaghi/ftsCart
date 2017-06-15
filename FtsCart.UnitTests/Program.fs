// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.
open System
open Xunit
open FsUnit.Xunit
open FtsCart
open FtsCart.transition
open Expecto 
let tests =
  testList "A test group" [
      test "addItemToEmptyCart_emptycart_returnsActivCart" {
        let e={id=1} :EmptyCartData
        let item={id=1;name="item1";quantity=1;price=5.}
        let actual=addItemToEmptyCart e item
        let expected=ActiveCart{id=1;items=[item];shippingInfo=None}
        Expect.equal (actual) (expected) "hello"
      }
      test "addItemToEmptyCart_addTwoItemstoEmptyCart_retursActiveCart"{
        let item1={id=1;name="item1";quantity=1;price=5.}
        let item2={id=1;name="item2";quantity=1;price=5.}
        let active={id=1;items=[item1];shippingInfo=None}
        let actual=addItemToActiveCart active item2
        let expected=ActiveCart{id=1;items=[item2;item1];shippingInfo=None}
        Expect.equal actual expected "hello there"
      }
      test "remove item from active cart with items more than 1"{
        let item1={id=1;name="item1";quantity=1;price=5.}
        let item2={id=2;name="item2";quantity=1;price=5.}
        let active={id=1;items=[item2;item1];shippingInfo=None}
        let actual=removItemFromActiveCart active item1.id
        let expected=ActiveCart{id=1;items=[item2];shippingInfo=None}
        Expect.equal actual expected "another hi"
      }
      test "remov ite from active with 1 item "{
        let item1={id=1;name="item1";quantity=1;price=5.}
        let active={id=1;items=[item1];shippingInfo=None}
        let actual=removItemFromActiveCart active item1.id
        let expected=EmptyCart{id=1}
        Expect.equal actual expected "another hi"
      }
      test "buy items of the active cart that has no shipping info"{
        let item1={id=1;name="item1";quantity=1;price=5.}
        let active={id=1;items=[item1];shippingInfo=None}
        let actual=BuyItemsOfActiveCart active 
        let expected=ActiveCart{id=1;items=[item1];shippingInfo=None}
        Expect.equal actual expected "another hi"
      }
      test "buy items of active cart with shipping info"{
        let item1={id=1;name="item1";quantity=1;price=5.}
        let active={id=1;items=[item1];shippingInfo=Some {shippingAddress="Nablus";shippingCost=50.}}
        let actual=BuyItemsOfActiveCart active 
        let expected=BoughtCart{id=1;items=[item1];totalPrice=55.;shippingInfo={shippingAddress="Nablus";shippingCost=50.}}
        Expect.equal actual expected "another hi"
      }
      test "changeItemQuantity to less than 1 doesn't change quantity"{
        let item1={id=1;name="item1";quantity=1;price=5.}
        let actual=changeItemQuantity 0 item1
        let expected=item1
        Expect.equal actual expected "another hi"
      }
      test "changeItemQuantity to any value >=1  changes the quantity"{
        let item1={id=1;name="item1";quantity=1;price=5.}
        let actual=changeItemQuantity 10 item1
        let expected={id=1;name="item1";quantity=10;price=5.}
        Expect.equal actual expected "another hi"
      }
      test "inrementQuantity returns item with quantity increamneted by 1"{
        let item1={id=1;name="item1";quantity=1;price=5.}
        let actual=incrementQuantity item1
        let expected={id=1;name="item1";quantity=2;price=5.}
        Expect.equal actual expected "another hi"
      }
      test "decrementQuantity  less than 1 returns item with quantity = 1"{
        let item1={id=1;name="item1";quantity=1;price=5.}
        let actual=decrementQuantity item1
        let expected={id=1;name="item1";quantity=1;price=5.}
        Expect.equal actual expected "another hi"
      }
      test "decrementQuantity  with qunatity >1 returns item with quantity decremented by 1"{
        let item1={id=1;name="item1";quantity=50;price=5.}
        let actual=decrementQuantity item1
        let expected={id=1;name="item1";quantity=49;price=5.}
        Expect.equal actual expected "another hi"
      }
    ]
[<EntryPoint>]
let main args =
  runTestsWithArgs defaultConfig args tests
(*to be added.
- calculate total
- add quantities 
- remove item from a cart
- presist shopping cart (save cart and retrieve it by userID (current)  
    - rules 
        
requirements - (no two aactive shopping carts at the same time for the same user)        
    
- history (previous orders)
- shipping (add shipping address, cost(this is a fixed amount for now))

*)    
