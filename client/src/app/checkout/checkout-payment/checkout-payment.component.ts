import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { firstValueFrom } from 'rxjs';
import { BasketService } from 'src/app/basket/basket.service';
import { Basket } from 'src/app/shared/models/basket';
import { OrderToCreate } from 'src/app/shared/models/order';
import { Address } from 'src/app/shared/models/user';
import { CheckoutService } from '../checkout.service';

@Component({
  selector: 'app-checkout-payment',
  templateUrl: './checkout-payment.component.html',
  styleUrls: ['./checkout-payment.component.scss']
})
export class CheckoutPaymentComponent {
@Input() checkoutForm?: FormGroup;
loading = false;
constructor(private basketService: BasketService,  private checkoutService: CheckoutService, 
  private toastr: ToastrService, private router:Router) {}
  submittingOrder: boolean = false;
  submitOrder(){
    this.loading = true;
    const basket = this.basketService.getCurrentBasketValue();
    const createdOrder =  this.createOrder(basket);
    if(!basket) return;
    const orderToCreate = this.getOrderToCreate(basket);
    if(!orderToCreate) return;
    
    this.checkoutService.createOrder(orderToCreate).subscribe({
      next: (order) => {
        this.toastr.success('Order created successfully');
        this.basketService.deleteLocalBasket();
        const navigationExtras: NavigationExtras = { state: createdOrder };
        this.router.navigate(['checkout/success'], navigationExtras);
      },
      error: (error) => {
        // Handle any errors here
      },
      complete: () => {
        this.loading = false; // Set it back to false when order submission is complete.
      },
    });
  }
  private async createOrder(basket: Basket | null) {
    if (!basket) throw new Error('Basket is null');
    const orderToCreate = this.getOrderToCreate(basket);
    return firstValueFrom(this.checkoutService.createOrder(orderToCreate));
  }


  private getOrderToCreate(basket: Basket): OrderToCreate {
    //try {
      const deliveryMethodId = this.checkoutForm?.get('deliveryForm')?.get('deliveryMethod')?.value;
      const shipToAddress = this.checkoutForm?.get('addressForm')?.value as Address;
  
      if (!deliveryMethodId || !shipToAddress) {
        console.error('Missing or invalid form values in getOrderToCreate');
        throw new Error('Missing or invalid form values');
      }
  
      return {
        basketId: basket.id,
        deliveryMethodId: deliveryMethodId,
        shipToAddress: shipToAddress
      };
    } //catch (error) {
      //console.error('Error in getOrderToCreate:', error);
      // Handle the error as needed
     // return null;
    //}
  }
