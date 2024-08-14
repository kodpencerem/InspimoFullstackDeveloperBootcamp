import { HttpClient } from '@angular/common/http';
import { Component, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import * as signalR from '@microsoft/signalr';

@Component({
  selector: 'app-chats',
  standalone: true,
  imports: [FormsModule, RouterLink],
  templateUrl: './chats.component.html',
  styleUrl: './chats.component.css'
})
export class ChatsComponent {
  connection: signalR.HubConnection;
  chats = signal<{name: string, message: string}[]>([]);
  message = signal<string>("");
  name = signal<string>("Jhon Doe");

  constructor(private http: HttpClient){
    this.connection = new signalR
    .HubConnectionBuilder()
    .withUrl("http://localhost:5150/chat")    
    .build();

    this.startConnection();

    this.connection.onclose(() => {
      console.log("Connection closed. Reconnecting...");
      setTimeout(() => {
        this.startConnection();
      }, 2000);
    });

    this.connection.on("msg",(res:{name: string, message: string})=> {
      this.chats.update(prev=> [...prev, res]);      
    });
  }

  startConnection(){
    this.connection
    .start()
    .then(()=> {
      console.log("Connection is started");      
    })
    .catch(()=> {
      setTimeout(() => {
        console.log("Connection is lost. We try again...");
        
        this.startConnection();
      }, 2000);
    })
  }

  sendMessage(){
    this.http.get(`https://localhost:7138/send-message?name=${this.name()}&message=${this.message()}`).subscribe(()=> {      
      this.message.set("");
    });    
  }
}
