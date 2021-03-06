import { Injectable } from '@angular/core';
import * as alertify from 'alertifyjs';

@Injectable({
  providedIn: 'root'
})
export class AlertifyService {

  constructor() { }

  confirm(message: string, okCallBack: () => any) {
    alertify.confirm(message, (e: any) => {
      if (e) {
        okCallBack();
      } else { }
    });
  }

  error(message: string) {
    alertify.error(message);
    console.log(message);
  }
  warning(message: string) {
    alertify.warning(message);
  }
  message(message: string) {
    alertify.message(message);
  }
  success(message: string) {
    alertify.success(message);
  }
}
