import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Car } from 'src/app/interfaces/car';
import { AuthService } from 'src/app/services/auth.service';
import { CarServiceService } from 'src/app/services/car-service.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-user-home',
  templateUrl: './user-home.component.html',
  styleUrls: ['./user-home.component.css']
})
export class UserHomeComponent implements OnInit {
  allCars:Car[]=[];
  filteredCars: Car[] = [];
  searchForm: FormGroup; 
  userRole:string|null;
  
  constructor(private toastr:ToastrService,private carService:CarServiceService,private router:Router,private authService:AuthService,private fb:FormBuilder)
  { 
    this.searchForm = this.fb.group({
      searchTerm: [''],
    });

    this.userRole = this.authService.getRole();
  }
  
  
  ngOnInit(): void {
    this.getCars();
  }

  getCars()
  {
    this.carService.getAllCars().subscribe((res:any)=>{
      console.log("Cars List",res);
      this.allCars = res;
      this.filteredCars = res;
    },(error)=>{
      console.log(error);
      this.toastr.error("Failed to fetch cars",`Error`);
    });
  }
  //search cars
  onSearch(): void {
    const searchTerm = this.searchForm.get('searchTerm')?.value.toLowerCase();

    this.filteredCars = this.allCars.filter((car) => {
      return (
        car.maker.toLowerCase().includes(searchTerm) ||
        car.model.toLowerCase().includes(searchTerm) ||
        (car.rentPrice && car.rentPrice.toString().includes(searchTerm))
      );
    });
  }

  //view car details agreement form
  viewCarDetails(carId:number)
  {
    this.router.navigate([`carDetail/${carId}`]);
  }
  
  //edit car
  editCarAdmin(carId:number)
  {
    console.log("EDIT CAR ADMIN");
    this.router.navigate([`admin-edit-car/${carId}`]);
    
  }

  //delete car
  deleteCarAdmin(carId:number)
  {
    Swal.fire({
      title:'Are you sure?',
      text:'You will not be able to recover deleted Car!',
      icon:'warning',
      showCancelButton:true,
      confirmButtonText:'Yes, delete it!',
      cancelButtonText:'No, keep it'
    }).then((result)=>{
      if(result.value)
      {
        //apicall to delete
        this.carService.deleteCar(carId).subscribe((res)=>{
          console.log(res);
          this.getCars();
          this.toastr.success("Deleted Success !!","Car Deleted")    
          Swal.fire(
            'Deleted',
            'Your Car has been deleted',
            'success'
          )
        },(error)=>{
          console.log(error);
          this.toastr.error("Unable to delete Car !!","Failed Delete")    
        });
      }
      else if(result.dismiss === Swal.DismissReason.cancel)
      {
        Swal.fire('Cancelled','Your record is safe :)','error')
      }
    })
  }

  

}
