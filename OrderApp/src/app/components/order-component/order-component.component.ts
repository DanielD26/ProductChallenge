import { Component, OnInit } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { Customer } from 'src/app/models/customer';
import { Order } from 'src/app/models/order';
import { Product } from 'src/app/models/product';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-order-component',
  templateUrl: './order-component.component.html',
  styleUrls: ['./order-component.component.css']
})
export class OrderComponentComponent implements OnInit {
  orderList: Order[];
  customerList: Customer[];
  productList: Product[];
  newOrder: Order;
  selectedCustomer: Customer;
  selectedProduct: Product;
  selectedShipMode: string;
  qty: any;
  deleteOrder: Order;
  editSelectedOrder: Order;
  editOrder: Order;

  constructor(private _api: ApiService) { }

  ngOnInit(): void {
  
    this.customerList = [];
    this.productList = [];
    this.orderList = [];

    this._api.getProduct().subscribe((listProducts) => {
      for (let i=0; listProducts.length > i; i++) {
        this.productList.push(listProducts[i]);
      }
    })
    
    this._api.getOrders().subscribe((listOrders) => {
      for (let i=0; listOrders.length > i; i++) {
        this.orderList.push(listOrders[i]);
      }
    })

    this._api.getCustomers().subscribe((listCustomers) => {
      for (let i=0; listCustomers.length > i; i++) {
        this.customerList.push(listCustomers[i]);
      }
    })

    // sets the selected order to the first choice in select box
    this.editSelectedOrder = this.orderList[0];
    // inherits the selected order object to the editOrder
    this.editOrder = {...this.editSelectedOrder};
  }

  createOrder() {
    this.newOrder = {
      custID: this.selectedCustomer.custID,
      productID: this.selectedProduct.productID,
      orderDate: new Date().toString(),
      quantity: this.qty,
      shipDate: new Date().toString(),
      shipMode: this.selectedShipMode

    }
    this._api.postOrder(this.newOrder).subscribe((res: any) => {
      alert("Order Placed!")
    })
  }

  removeOrder() {
    this._api.deleteOrder(this.deleteOrder).subscribe((res: any) =>{
      alert("Order Deleted!")
    })
  }

  changeOrder() {
    this._api.putOrder(this.editOrder).subscribe((res: any) =>{
      alert("Edited Order!")
    })
  }

  // when theres a change in the select, inherit the selected order
  chosenOrder() {
    this.editOrder = {...this.editSelectedOrder};
  }

}
