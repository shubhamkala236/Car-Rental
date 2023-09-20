import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Car } from 'src/app/interfaces/car';
import { AgreementService } from 'src/app/services/agreement.service';
import { AuthService } from 'src/app/services/auth.service';
import { CarServiceService } from 'src/app/services/car-service.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-car-details',
  templateUrl: './car-details.component.html',
  styleUrls: ['./car-details.component.css']
})
export class CarDetailsComponent implements OnInit {
  agreementForm:any;
  carId:any;
  carDetail:Car|undefined;
  
  constructor(private fb:FormBuilder,private activatedRoute:ActivatedRoute,private toastr:ToastrService,private carService:CarServiceService,private router:Router,private authService:AuthService,private agreementService:AgreementService)
  {}
  
  ngOnInit(): void {
    this.carId = this.activatedRoute.snapshot.paramMap.get('id');
    this.getCarDetails(this.carId);
    this.setForm();
  }

  //getters
  get rentalDuration()
  {
    return this.agreementForm.get('rentalDuration');
  }


  //setForm
  setForm()
  {
    this.agreementForm = this.fb.group({
      rentalDuration:['',[Validators.required,Validators.pattern("^[0-9]*$")]],
      // CarId:['',[Validators.required,Validators.pattern("^[0-9]*$")]],
    });
  }

  //get car details based on id 
  getCarDetails(carId:number)
  {
    this.carService.getCarDetails(carId).subscribe((res)=>{
      console.log(res);
      // this.CarDetailsId = res.carId;
      this.carDetail = res;
      console.log(this.carDetail);
      
    },(error)=>{
      console.log(error);
      this.toastr.error("Failed to fetch car details","Error")
    });
  }


  //submit agreementForm
  submitForm(formData:FormGroup)
  {
    console.log(formData.value);
    if(this.agreementForm.invalid)
    {
      this.toastr.error("Invalid Form","Pease write again")
      return;
    }

    //send form data to api
    const agreementData:FormData = new FormData();
    agreementData.append('RentalDuration',formData.value.rentalDuration);
    agreementData.append('CarId',this.carId);
    // console.log(agreementData.get('CarId'));
    
    //send data to backend api -- > agreement Service
    this.agreementService.createAgreement(agreementData).subscribe((res)=>{
      console.log(res);
      this.toastr.success("Agreement Created","Success");
      this.router.navigate(['/home']);
    },(error)=>{
      console.log(error);
    });
    
  }

}
