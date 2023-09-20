import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Agreement } from 'src/app/interfaces/agreement';
import { User } from 'src/app/interfaces/user';
import { AgreementService } from 'src/app/services/agreement.service';
import { CarServiceService } from 'src/app/services/car-service.service';
import { UserService } from 'src/app/services/user.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-my-agreements',
  templateUrl: './my-agreements.component.html',
  styleUrls: ['./my-agreements.component.css']
})
export class MyAgreementsComponent implements OnInit {
  
  myAgreementsList:Agreement[]=[];
  
  constructor(private toastr:ToastrService,private agreementService:AgreementService,private carService:CarServiceService,private userService:UserService,private router:Router){
  }

  ngOnInit(): void {
    this.getAgreements();
  }

  //edit button clicked
  handleEdit(agreementId:number)
  {
    this.router.navigate([`editAgreement/${agreementId}`]);
  }

  //delete button clicked
  handleDelete(agreementId:number)
  {
    Swal.fire({
      title:'Are you sure?',
      text:'You will not be able to recover deleted Agreement!',
      icon:'warning',
      showCancelButton:true,
      confirmButtonText:'Yes, delete it!',
      cancelButtonText:'No, keep it'
    }).then((result)=>{
      if(result.value)
      {
        //apicall to delete
        this.agreementService.deleteAgreementById(agreementId).subscribe((res)=>{
          console.log(res);
          this.getAgreements();
          this.toastr.success("Deleted Success !!","Product Deleted")    
          Swal.fire(
            'Deleted',
            'Your record has been deleted',
            'success'
          )
        },(error)=>{
          console.log(error);
          this.toastr.error("Unable to delete record !!","Failed Delete")    
        });
      }
      else if(result.dismiss === Swal.DismissReason.cancel)
      {
        Swal.fire('Cancelled','Your record is safe :)','error')
      }
    })
  }

  //Accept Agreement
  handleAccept(agreementId:number)
  {
    Swal.fire({
      title:'Are you sure?',
      text:'You want to accept Agreement?',
      icon:'warning',
      showCancelButton:true,
      confirmButtonText:'Yes, Aceept it!',
      cancelButtonText:'No, do not Accept it'
    }).then((result)=>{
      if(result.value)
      {
        console.log(agreementId);
        //apicall to Accept
        this.agreementService.acceptAgreementById(agreementId).subscribe((res)=>{
          console.log(res);
          this.getAgreements();
          this.toastr.success("Accept Success !!","Agreement Accepted")    
          Swal.fire(
            'Aceepted',
            'Your Agreement has been Aceepted',
            'success'
          )
        },(error)=>{
          console.log(error);
          this.toastr.error("Unable to Accept Agreement !!","Failed Accept")    
        });
      }
      else if(result.dismiss === Swal.DismissReason.cancel)
      {
        Swal.fire('Cancelled','Your agreement is not accepted :)','error')
      }
    })
  }

  //handle Return Request
  handleReturnRequest(agreementId:number)
  {
    Swal.fire({
      title:'Are you sure?',
      text:'You want to Return Car?',
      icon:'warning',
      showCancelButton:true,
      confirmButtonText:'Yes, return it!',
      cancelButtonText:'No, keep it'
    }).then((result)=>{
      if(result.value)
      {
        console.log(agreementId,"HMMMMMMM");
        //apicall to Accept
        this.agreementService.requestReturn(agreementId).subscribe((res)=>{
          console.log(res);
          this.getAgreements();
          this.toastr.success("Accept Success !!","Return Request Processed")    
          Swal.fire(
            'Aceepted',
            'Your Return Request has been processed',
            'success'
          )
        },(error)=>{
          console.log(error);
          this.toastr.error("Unable to request return !!","Failed Return Request")    
        });
      }
      else if(result.dismiss === Swal.DismissReason.cancel)
      {
        Swal.fire('Cancelled','Your return is not processed :)','error')
      }
    })
    
  }

  getAgreements()
  {
    //get agreements
    this.agreementService.getMyAgreements().subscribe((res:Agreement[])=>{
      console.log(res);
      this.myAgreementsList = res;
      console.log("Saved",this.myAgreementsList);

      //fetch car details for each agreement
      for(const agreement of this.myAgreementsList)
      {
        this.carService.getCarDetails(agreement.carId).subscribe((res)=>{
          // console.log("CArData",res);
          
          agreement.carDetails = res;
        },(carError)=>{
          console.error('Error fetching car details:', carError);
          this.toastr.error("Error while fetching Car","Car Error")
        });
        
        //get User data
        this.userService.getMyData().subscribe((res:User)=>{
          // console.log("UserData",res);
          agreement.userData = res;
        },(error)=>{
          console.log(error);
          this.toastr.error("Error while fetching UserData","User Error")
        });
      }
        
      console.log("FINAL DATA",this.myAgreementsList);
    },(error)=>{
      console.log(error);
      this.toastr.error("Error while fetching agreements","Failed")
    });
  }
  

}
