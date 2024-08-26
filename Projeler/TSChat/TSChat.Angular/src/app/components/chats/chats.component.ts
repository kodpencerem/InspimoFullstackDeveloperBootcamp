import { Component, ElementRef, signal, ViewChild, ViewEncapsulation } from '@angular/core';
import { UserModel } from '../../models/user.model';
import { ActivatedRoute } from '@angular/router';
import { HttpService } from '../../services/http.service';
import { mainUrl } from '../../constants';
import { SharedService } from '../../services/shared.service';
import { ChatModel } from '../../models/chat.model';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { SignalRService } from '../../services/signalr.service';

@Component({
  selector: 'app-chat',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './chats.component.html',
  styleUrl: './chats.component.css',
  encapsulation: ViewEncapsulation.None
})
export default class ChatsComponent {
  user = signal<UserModel | null>(null);
  toUserId = signal<string>("");
  chats = signal<ChatModel[]>([]);
  message = signal<string>("");
  users = signal<UserModel[]>([]);
  mainUrl = mainUrl;
  
  @ViewChild("chatContainer") chatContainer: ElementRef<HTMLDivElement> | undefined;

  constructor(
    private acitivated: ActivatedRoute,
    public shared: SharedService,
    private http: HttpService,
    private signalR: SignalRService
  ){
    this.getChatUsers();

    this.acitivated.params.subscribe((res)=> {
      this.signalR.connect(()=> {
        this.signalR.builder!.invoke("Connection", this.shared.user.id);

        this.signalR.builder!.on("message",(res:ChatModel)=> {          
          if(res.userId == this.toUserId()){
            this.chats.update((prev)=> [...prev, res]);
            this.scrollToBottom();
          }
        });

        this.signalR.builder!.on("chatUser",(res: UserModel)=> {
          if(!this.users().find(p=> p.id === res.id)){
            this.users.update(prev => [...prev, res]);
          }
        })
      })
      if(res["id"]){
        this.toUserId.set(res["id"]);
        this.getUser();
      }
    });    
  }

  getChatUsers(){
    this.http.get<UserModel[]>("Chats/GetGetAllChatUsers",(res)=> this.users.set(res));
  }

  scrollToBottom(): void {
    setTimeout(() => {
      try {
        this.chatContainer!.nativeElement.scrollTop = this.chatContainer!.nativeElement.scrollHeight;
      } catch(err) { 
        console.error('Scroll to bottom failed', err);
      }
    }, 0);             
  }

  getUser(){
    this.http.get<UserModel>(`Users/GetById?id=${this.toUserId()}`,(res)=> {
      this.user.set(res);
      this.getChats();
    });
  }

  getChats(){
    this.http.get<ChatModel[]>(`Chats/GetAll?toUserId=${this.toUserId()}`,(res)=> {
      this.chats.set(res);
      this.scrollToBottom();
    });
  }

  sendMessage(){
    const data = {
      message: this.message(),
      toUserId: this.toUserId()
    };

    this.http.post<string>("Chats/SendMessage", data, (res)=> {
      this.message.set("");
      this.getChats();
    });
  }

  showChat(userId: string){
    this.toUserId.set(userId);
    this.getUser();
    const user = this.users().find(p=> p.id == userId);
    user!.unReadMessageCount = 0;
  }
}
