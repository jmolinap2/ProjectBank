import { Component, Injector } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { finalize } from 'rxjs/operators';
import { AppComponentBase } from '@shared/app-component-base';
import { CreditRequestServiceProxy, CreditRequestDto } from '@shared/service-proxies/service-proxies';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-credit-request',
  templateUrl: './create-credit-request.component.html',
  styleUrls: ['./create-credit-request.component.css']
})
export class CreateCreditRequestComponent extends AppComponentBase {
  form: FormGroup;
  saving = false;

  constructor(
    injector: Injector,
    private fb: FormBuilder,
    private _creditRequestService: CreditRequestServiceProxy,
    private _router: Router
  ) {
    super(injector);

    this.form = this.fb.group({
      montoSolicitado: [null, Validators.required],
      plazoMeses: [null, Validators.required],
      ingresoMensual: [null, Validators.required],
      antiguedadLaboral: [null, Validators.required]
    });
  }

  submit(): void {
    if (this.form.invalid) {
      this.notify.warn('Formulario inválido');
      return;
    }

    const input = new CreditRequestDto();
    input.init(this.form.value); // Asume que los nombres coinciden exactamente

    this.saving = true;
    this._creditRequestService
      .create(input)
      .pipe(finalize(() => (this.saving = false)))
      .subscribe(() => {
        this.notify.success('Solicitud enviada exitosamente');
        this._router.navigate(['/app/credit-requests/mine']);
      });
  }
}
