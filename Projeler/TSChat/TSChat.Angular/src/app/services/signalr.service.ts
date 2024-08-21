import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { mainUrl } from '../constants';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {  
  builder: signalR.HubConnection | undefined;

  connect(callBack?: () => void){
    this.builder = new signalR.HubConnectionBuilder().withUrl(`${mainUrl}/chat`).build();    

    this.builder.start()
    .then(()=> {
      console.log("Connection is successful");

      if(callBack){
        callBack();
      }
    })
    .catch((err)=> {
      console.log(err);
    });
  }
}
