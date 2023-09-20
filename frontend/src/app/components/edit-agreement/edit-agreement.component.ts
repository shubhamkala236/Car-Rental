import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Agreement } from 'src/app/interfaces/agreement';
import { Car } from 'src/app/interfaces/car';
import { User } from 'src/app/interfaces/user';
import { AgreementService } from 'src/app/services/agreement.service';
import { AuthService } from 'src/app/services/auth.service';
import { CarServiceService } from 'src/app/services/car-service.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-edit-agreement',
  templateUrl: './edit-agreement.component.html',
  styleUrls: ['./edit-agreement.component.css']
})
export class EditAgreementComponent {

  EditDurationForm:FormGroup;
  agreementId:number;
  agreementDetail:Agreement|null=null;
  userRole:string|null;

  constructor(private fb:FormBuilder,private userService:UserService,private auth:AuthService,private carService:CarServiceService,private agreementService:AgreementService,private toastr:ToastrService,private router:Router,private activatedRoute:ActivatedRoute)
  {
    this.EditDurationForm = this.fb.group({
      rentalDuration:['',[Validators.required,Validators.maxLength(3),Validators.minLength(1),Validators.pattern('^[1-9]*$')]],    
    });

    this.agreementId = this.activatedRoute.snapshot.params['id'];

    //get user role
    this.userRole = this.auth.getRole();

    if(this.userRole === 'admin')
    {
      console.log("I AM ADMIN");
      this.getAgreementDetailsAdmin();  
    }
    else if(this.userRole==='user')
    {
      console.log("I AM USER");
      this.getAgreementDetails();
    }
    else{
      console.log("I AM NOT LOGGED IN");
      this.toastr.error("Login to continue","Not Logged In")
    }
    
  }

  //getters
  get rentalDuration()
  {
    return this.EditDurationForm.get('rentalDuration');
  }

  
  //get details -- preFilled Form -- USER LOGGED IN
  getAgreementDetails()
  {
    console.log("GET DETAILS START");
        
    this.agreementService.getAgreementById(this.agreementId).subscribe((res:Agreement)=>{
      //store in variable
      this.agreementDetail = res;
      console.log(this.agreementDetail);
      
      //get car data and userData
      this.carService.getCarDetails(this.agreementDetail.carId).subscribe((res:Car)=>{
        // console.log("CArData",res);
        if(this.agreementDetail)
        {
          this.agreementDetail.carDetails = res;
        }
        else
        {
          console.log("Agreement Detail is null");
          
        }
        
      },(carError)=>{
        console.error('Error fetching car details:', carError);
        this.toastr.error("Error while fetching Car","Car Error")
      });
      
      //get User data
      this.userService.getMyData().subscribe((res:User)=>{
        console.log("UserData",res);
        
        if(this.agreementDetail)
        {
          this.agreementDetail.userData = res;
        }
        else
        {
          console.log("Agreement Detail is null");
          
        }
        // agreement.userData = res;
      },(error)=>{
        console.log(error);
        this.toastr.error("Error while fetching UserData","User Error")
      });

      //Prefill data
      this.EditDurationForm.patchValue({
        rentalDuration:res.rentalDuration,
      })
      // console.log("FormData",this.EditDurationForm.value);

    },(error)=>{
      console.log(error);
      this.toastr.error("Error while fetching Agreement","Agreement Error")
    });

  }

  //Get Details ---- ADMIN Logged In
  getAgreementDetailsAdmin()
  {
    console.log("GET DETAILS ADMIN START");
    console.log(this.agreementId);
    
    // Get any agreement
    this.agreementService.getAgreementByIdAdmin(this.agreementId).subscribe((res:Agreement)=>{
      //store in variable
      this.agreementDetail = res;
      console.log(this.agreementDetail);
      
      //get car data and userData
      this.carService.getCarDetails(this.agreementDetail.carId).subscribe((res:Car)=>{
        // console.log("CArData",res);
        if(this.agreementDetail)
        {
          this.agreementDetail.carDetails = res;
        }
        else
        {
          console.log("Agreement Detail is null");
          
        }
        
      },(carError)=>{
        console.error('Error fetching car details:', carError);
        this.toastr.error("Error while fetching Car","Car Error")
      });
      
      //get any User data from agreement userId -------- admin
      this.userService.getUserDataAdmin(this.agreementDetail.userId).subscribe((res:User)=>{
        console.log("UserData",res);
        
        if(this.agreementDetail)
        {
          this.agreementDetail.userData = res;
        }
        else
        {
          console.log("Agreement Detail is null");
          
        }
        // agreement.userData = res;
      },(error)=>{
        console.log(error);
        this.toastr.error("Error while fetching UserData","User Error")
      });

      //Prefill data
      this.EditDurationForm.patchValue({
        rentalDuration:res.rentalDuration,
      })
      // console.log("FormData",this.EditDurationForm.value);

    },(error)=>{
      console.log(error);
      this.toastr.error("Error while fetching Agreement","Agreement Error")
    });

  }


  //submit Edit form---- based on role
  submitForm(formData:FormGroup)
  {
    if(this.EditDurationForm.invalid)
    {
      this.toastr.error("Please fill form correctly","Invalid Inputs")
      return;
    }

    //send to edit api
    const data:FormData=new FormData();
    data.append('updatedRentDuration',formData.value.rentalDuration);
    // data.append('agreementId',this.agreementId.toString())

    this.agreementService.editAgreement(this.agreementId,data).subscribe((res)=>{
      console.log(res);
      this.toastr.success("Edited Success","Agreement")
      this.router.navigate(['myAgreements'])
    },(error)=>{
      console.log(error);
      this.toastr.error("Unable to Edit","Failed")
    })
  }

  //Submit form admin 
  submitFormAdmin(formData:FormGroup)
  {
    console.log("Admin FORM SUBMIT",formData.value);
    console.log("USER ROLE RECEIVED",this.userRole);

    if(this.EditDurationForm.invalid)
    {
      this.toastr.error("Please fill form correctly","Invalid Inputs")
      return;
    }

    //send to edit api
    const data:FormData=new FormData();
    data.append('updatedRentDuration',formData.value.rentalDuration);

    this.agreementService.editAgreementAdmin(this.agreementId,data).subscribe((res)=>{
      console.log(res);
      this.toastr.success("Edited Success","Agreement")
      this.router.navigate(['admin_home'])
    },(error)=>{
      console.log(error);
      this.toastr.error("Unable to Edit","Failed")
    })

  }
   
  
  //On submit check role
  onSubmitForm(formData:FormGroup)
  {
    if (this.userRole === 'admin') {
      this.submitFormAdmin(formData);
    } else if (this.userRole === 'user') {
      this.submitForm(formData);
    }
  }


 }
