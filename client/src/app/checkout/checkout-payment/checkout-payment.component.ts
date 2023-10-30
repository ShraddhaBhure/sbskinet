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
import { Stripe, StripeCardCvcElement, StripeCardExpiryElement, StripeCardNumberElement, loadStripe } from '@stripe/stripe-js';

@Component({
  selector: 'app-checkout-payment',
  templateUrl: './checkout-payment.component.html',
  styleUrls: ['./checkout-payment.component.scss']
})
export class CheckoutPaymentComponent implements OnInit {
@Input() checkoutForm?: FormGroup;
@ViewChild('cardNumber') cardNumberElement?: ElementRef;
@ViewChild('cardExpiry') cardExpiryElement?: ElementRef;
@ViewChild('cardCvc') cardCvcElement?: ElementRef;
stripe: Stripe|null = null;
cardNumber?: StripeCardNumberElement;
cardExpiry?: StripeCardExpiryElement;
cardCvc?: StripeCardCvcElement;

cardNumberComplete = false;
cardExpiryComplete = false;
cardCvcComplete = false;
cardErrors: any;
loading = false;

constructor(private basketService: BasketService,  private checkoutService: CheckoutService, 
  private toastr: ToastrService, private router:Router) {}
  ngOnInit(): void {
    //Publishable key for loadStripe
    loadStripe('pk_test_51O69f7SF0N0pTWURQ1JessEoF30SrS0gkvqVk9d4DlqJEOnTGZIFXp6jed4HnQYasdFVz7Cv39cy8XojflCckZor008960YvnQ').then(stripe => {
      this.stripe = stripe;
      const elements = stripe?.elements();
      if (elements) {
        this.cardNumber = elements.create('cardNumber');
        this.cardNumber.mount(this.cardNumberElement?.nativeElement);
        this.cardNumber.on('change', event => {
          this.cardNumberComplete = event.complete;
          if (event.error) this.cardErrors = event.error.message;
          else this.cardErrors = null;
        })

        this.cardExpiry = elements.create('cardExpiry');
        this.cardExpiry.mount(this.cardExpiryElement?.nativeElement);
        this.cardExpiry.on('change', event => {
          this.cardExpiryComplete = event.complete;
          if (event.error) this.cardErrors = event.error.message;
          else this.cardErrors = null;
        })

        this.cardCvc = elements.create('cardCvc');
        this.cardCvc.mount(this.cardCvcElement?.nativeElement);
        this.cardCvc.on('change', event => {
          this.cardCvcComplete = event.complete;
          if (event.error) this.cardErrors = event.error.message;
          else this.cardErrors = null;
        })
      }
    })
  }

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
