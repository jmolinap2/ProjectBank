import { Component, Injector, OnInit } from '@angular/core';
import { CreditRequestDto, CreditRequestServiceProxy } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/app-component-base';

@Component({
  selector: 'app-my-credit-requests',
  templateUrl: './my-credit-requests.component.html',
  styleUrls: ['./my-credit-requests.component.css']
})
export class MyCreditRequestsComponent extends AppComponentBase implements OnInit {
  creditRequests: CreditRequestDto[] = [];
  loading = false;

  constructor(
    injector: Injector,
    private _creditRequestService: CreditRequestServiceProxy
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.getCreditRequests();
  }

  getCreditRequests(): void {
    this.loading = true;
    this._creditRequestService
      .getAll(undefined, 0, 100)
      .subscribe(result => {
        this.creditRequests = result.items;
        this.loading = false;
      });
  }
}
