import { Injectable } from '@angular/core';
import { UserModel } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class SharedService {

  user: UserModel = new UserModel();
  constructor() { }
}
