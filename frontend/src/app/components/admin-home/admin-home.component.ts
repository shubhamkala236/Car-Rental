import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Agreement } from 'src/app/interfaces/agreement';
import { User } from 'src/app/interfaces/user';
import { AgreementService } from 'src/app/services/agreement.service';
import { CarServiceService } from 'src/app/services/car-service.service';
import { UserService } from 'src/app/services/user.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-admin-home',
  templateUrl: './admin-home.component.html',
  styleUrls: ['./admin-home.component.css']
})
export class AdminHomeComponent implements OnInit {

  myAgreementsList:Agreement[]=[];
  filteredAgreements:Agreement[]=[];
  filterField:'all'|'pending'|'approved'|'returnRequested' = 'all';

  constructor(private toastr:ToastrService,private agreementService:AgreementService,private carService:CarServiceService,private userService:UserService,private router:Router){
  }

  ngOnInit(): void {
    this.getAgreements();
  }

    //edit button clicked
    handleEdit(agreementId:number)
    {
      // same route for admin and user
      this.router.navigate([`editAgreement/${agreementId}`]);
    }
  
    //delete button clicked
    handleDelete(agreementId:number)
    {
      console.log("DELETE ADMIN");
      
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
          this.agreementService.deleteAgreementAdmin(agreementId).subscribe((res)=>{
            console.log(res);
            this.getAgreements();
            this.toastr.success("Deleted Success !!","Agreement Deleted")    
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

    //Aproval
    handleAprroval(agreementId:number)
    {
      console.log("Aproval By ADMIN");
      
      Swal.fire({
        title:'Are you sure?',
        text:'You want to Approve Return Request!',
        icon:'warning',
        showCancelButton:true,
        confirmButtonText:'Yes, Aprrove it!',
        cancelButtonText:'No'
      }).then((result)=>{
        if(result.value)
        {
          //apicall to delete
          this.agreementService.approveReturnAdmin(agreementId).subscribe((res)=>{
            console.log(res);
            this.getAgreements();
            this.toastr.success("Approved Success !!","Aprroval Success")    
            Swal.fire(
              'Aprroved',
              'Your Return has been Approved',
              'success'
            )
          },(error)=>{
            console.log(error);
            this.toastr.error("Unable to delete record !!","Failed Aprroval")    
          });
        }
        else if(result.dismiss === Swal.DismissReason.cancel)
        {
          Swal.fire('Cancelled','You have chosen not to approve :)','error')
        }
      })
    }
  

  //getAllAgreements
  getAgreements()
  {
    //get agreements
    this.agreementService.getAllAgreements().subscribe((res:Agreement[])=>{
      console.log(res);
      this.myAgreementsList = res;
      this.filteredAgreements = res;
      console.log("Saved Agreements Admin",this.myAgreementsList);

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
        this.userService.getUserDataAdmin(agreement.userId).subscribe((res:User)=>{
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

  //filter agreements
  filter(status:'all'|'pending'|'approved'|'returnRequested')
  {
    this.filterField = status;
    if(status==='all')
    {
      this.filteredAgreements = this.myAgreementsList;
    }
    else
    {
      this.filteredAgreements = this.myAgreementsList.filter((agreement)=>{
        if(status==='pending')
        {
          return !agreement.isAccepted;
        }
        else if(status==='approved')
        {
          return agreement.isReturned;
        }
        else if(status==='returnRequested')
        {
          return agreement.isReturnRequested && agreement.isAccepted && !agreement.isReturned;
        }
        return agreement;
      });
    }
  }
}
