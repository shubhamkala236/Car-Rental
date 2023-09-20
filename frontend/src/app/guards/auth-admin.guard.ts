import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { AuthService } from '../services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthAdminGuard implements CanActivate {
  constructor(private authService:AuthService,private router:Router,private toastr:ToastrService){}

  canActivate():boolean {
    const role = this.authService.getRole();
    if(role=='admin')
    {
      return true;
    }
    this.toastr.error("Please Login as Admin","Authentication Failed");
    this.router.navigate(['login']);
    return false;
  }
  
}
