import { Component, Injector, OnInit } from '@angular/core';
import { CreditRequestDto, CreditRequestServiceProxy } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/app-component-base';
import { finalize } from 'rxjs/operators';
import { ChangeDetectorRef } from '@angular/core';
import { AppSessionService } from '@shared/session/app-session.service';
import { PermissionCheckerService } from 'abp-ng2-module';



@Component({
  selector: 'app-my-credit-requests',
  templateUrl: './my-credit-requests.component.html',
  styleUrls: ['./my-credit-requests.component.css']
})
export class MyCreditRequestsComponent extends AppComponentBase implements OnInit {
  creditRequests: CreditRequestDto[] = [];
  isAnalyst = false;
  constructor(
    injector: Injector,
    private _creditRequestService: CreditRequestServiceProxy,
    private cdr: ChangeDetectorRef,
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.isAnalyst = this.permission.isGranted('Pages.Analyst');
    this.getCreditRequests();
  }

  getCreditRequests(): void {
    abp.ui.setBusy();
    this._creditRequestService
      .getAll(undefined, 0, 100)
      .pipe(finalize(() => abp.ui.clearBusy()))
      .subscribe(result => {
        this.creditRequests = result.items || [];
        this.cdr.detectChanges();
      });
  }
}
