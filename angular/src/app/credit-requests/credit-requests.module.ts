import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ServiceProxyModule } from '@shared/service-proxies/service-proxy.module';

import { CreditRequestRoutingModule } from './credit-request-routing.module';
import { CreateCreditRequestComponent } from './create-credit-request/create-credit-request.component';
import { MyCreditRequestsComponent } from './my-credit-requests/my-credit-requests.component';
import { AnalystCreditRequestsComponent } from './analyst-credit-requests/analyst-credit-requests.component';

@NgModule({
  declarations: [
    CreateCreditRequestComponent,
    MyCreditRequestsComponent,
    AnalystCreditRequestsComponent      
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    CreditRequestRoutingModule,
    ServiceProxyModule 
  ]
})
export class CreditRequestsModule {}
