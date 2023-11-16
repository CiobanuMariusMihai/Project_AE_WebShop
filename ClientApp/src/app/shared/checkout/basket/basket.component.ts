import { Component, OnInit } from '@angular/core';
import { ShopService } from 'src/app/shop/shop.service';
import { Basket, Product } from '../../models/product';
import {Location} from '@angular/common';
import {loadStripe} from '@stripe/stripe-js';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment.development';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.css']
})

export class BasketComponent implements OnInit {

  basket: Basket | null = null;
  constructor(private shopService: ShopService, private location: Location, private http: HttpClient){}
  message: string = '';
  decoded: any;
  check: boolean = false;



  ngOnInit(): void {
    this.shopService.setBasket()

    this.basket = this.shopService.basket;

    this.shopService.getBasket().subscribe({
      next: (response: any) => {
        this.basket = response
      }
    })
  }

  increment(product: Product){
    this.shopService.addToBasket(product);
    this.shopService.getBasket().subscribe({
      next: (response: any) => {
        this.basket = response
      }
    })
      
  }

  decrement(product: Product){
    this.shopService.decrementFromBasket(product);
    
    this.shopService.updateBasket().subscribe({
      next: _=>{
        this.basket = this.shopService.basket;
      }
    })    
  }

  clearBasket(){
    this.shopService.emptyBasket().subscribe({
      next: _=>{
        window.location.reload();
      }
    })
  }

  onCheckout(): void {
    this.check = true;
    console.log(this.basket)
    this.http.post(`${environment.appUrl}/checkout/checkout?basketId=` +this.basket?.id+`&userId=`+this.basket?.userId+`&price=`+this.basket?.price,this.basket).subscribe(async (res:  any) => {
      console.log(res);
      let stripe = await loadStripe("pk_test_51OBbUcI3YKXUmoIVLrV14kHgC467aIstjJOHzw2R2qWSCmYImWIs9BipR2R5ukUomcXoNQOqaFxXOSvILBGfEnbC00VSARX1UH");
      stripe?.redirectToCheckout({
        sessionId: res.sessionId
      })
    })
  }


}
