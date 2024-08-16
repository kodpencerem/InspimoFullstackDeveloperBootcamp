import { HttpClient } from '@angular/common/http';
import { Component, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import * as signalR from '@microsoft/signalr';

@Component({
  selector: 'app-groups',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './groups.component.html',
  styleUrl: './groups.component.css'
})
export class GroupsComponent {
  connection: signalR.HubConnection | undefined;
  chats = signal<{ name: string, message: string }[]>([]);
  message = signal<string>("");
  name = signal<string>("Jhon Doe");

  constructor(
    private http: HttpClient,
    private activated: ActivatedRoute
  ) {    
    this.activated.params.subscribe(res => {
      this.name.set(res["name"]);

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

      this.connection.on("grpmsg", (res: { name: string, message: string }) => {
        this.chats.update(prev => [...prev, res]);
      });     
    });
  }

  startConnection() {
    this.connection!
      .start()
      .then(() => {
        console.log("Connection is started");
        this.connection!.invoke("JoinGroup", (this.name())).then(() => {

        });
      })
      .catch(() => {
        setTimeout(() => {
          console.log("Connection is lost. We try again...");

          this.startConnection();
        }, 2000);
      })
  }

  sendMessage() {
    this.http.get(`https://localhost:7138/send-group-message?name=${this.name()}&message=${this.message()}`).subscribe(() => {
      this.message.set("");
    });
  }
}
