import { Component } from '@angular/core';
import {FormBuilder,FormGroup,Validators} from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/services/auth.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent { 
  LoginForm:FormGroup;
  
  constructor(private fb:FormBuilder,private toastr:ToastrService,private auth:AuthService,private userService:UserService,private router:Router)
  {
    this.LoginForm = fb.group({
      email:['',[Validators.required,Validators.email,Validators.pattern("^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$"
      )]],
      password:['',[Validators.required]],
    });

    if(this.auth.isUser())
    {
      this.router.navigate(['/home']);
    }
    if(this.auth.isAdmin())
    {
      this.router.navigate(['/admin_home']);
    }
  }

  //getters
  get email()
  {
    return this.LoginForm.get('email');
  }
  
  get password()
  {
    return this.LoginForm.get('password');
  }

  //Login User
  loginUser(formdata:FormGroup)
  {
    console.warn(formdata);

    if(this.LoginForm.invalid)
    {
      this.toastr.error("Invalid Form","Please Enter Form Correctly")
      return;
    }
    
    //submit form data to backend using service
    const userCredentials:FormData = new FormData();
    userCredentials.append('Email',formdata.value.email);
    userCredentials.append('Password',formdata.value.password);


     

    this.userService.loginUser(userCredentials).subscribe((res:any)=>{
      console.log(res);
      this.toastr.success("Login Successfull","Welcome");
      this.auth.setToken(res.token);
      
      if(this.auth.isUser())
      {
        this.router.navigate(['/home']);
      }
      else{
        this.router.navigate(['/admin_home']);
      }

    },(error)=>{
      console.log(error);
      this.toastr.error("Login Failed","Check Credentials Again");
    });
    
  }


}
