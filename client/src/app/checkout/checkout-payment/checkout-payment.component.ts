import { Component, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { BasketService } from 'src/app/basket/basket.service';
import { CheckoutService } from '../checkout.service';
import { ToastrService } from 'ngx-toastr';
import { NavigationExtras, Router } from '@angular/router';
import { Basket } from 'src/app/shared/models/basket';
import { Address } from 'src/app/shared/models/user';

@Component({
  selector: 'app-checkout-payment',
  templateUrl: './checkout-payment.component.html',
  styleUrls: ['./checkout-payment.component.scss']
})
export class CheckoutPaymentComponent {
@Input() checkoutForm?: FormGroup;

constructor(private basketService: BasketService,  private checkoutService: CheckoutService, 
  private toastr: ToastrService, private router:Router) {}

  submitOrder(){
    const basket = this.basketService.getCurrentBasketValue();
    if(!basket) return;
    const orderToCreate = this.getOrderToCreate(basket);
    if(!orderToCreate) return;
    this.checkoutService.createOrder(orderToCreate).subscribe({
      next: order => {
        this.toastr.success('order created sucessfully');
        this.basketService.deleteLocalBasket();
        const navigationExtras:NavigationExtras = {state:order};
        this.router.navigate(['checkout/success'], navigationExtras);
      }
    })
  }
  private getOrderToCreate(basket: Basket) {
   const  deliveryMethodId = this.checkoutForm?.get('deliveryForm')?.get('deliveryMethod')?.value;
   const shipToAddress = this.checkoutForm?.get('addressForm')?.value as Address;
   if(!deliveryMethodId || !shipToAddress) return;
   return{
    basketId: basket.id,
    deliveryMethodId: deliveryMethodId,
    shipToAddress:shipToAddress
   }

  }
}
