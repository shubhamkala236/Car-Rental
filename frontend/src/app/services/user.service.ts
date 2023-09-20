import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../interfaces/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http:HttpClient) { }

  private baseServerUrl = "https://localhost:44350/api/";

  //login User
  loginUser(credentials:FormData)
  {
    return this.http.post(this.baseServerUrl+"User/login",credentials);
  }
  //get my data
  getMyData()
  {
    return this.http.get<User>(this.baseServerUrl+"User/myData");
  }

  //admin get user
  getUserDataAdmin(userId:number)
  {
    return this.http.get<User>(this.baseServerUrl+`User/getUserAdmin/${userId}`);
  }

}
