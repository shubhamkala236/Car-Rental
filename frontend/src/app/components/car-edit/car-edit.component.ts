import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CarServiceService } from 'src/app/services/car-service.service';

@Component({
  selector: 'app-car-edit',
  templateUrl: './car-edit.component.html',
  styleUrls: ['./car-edit.component.css']
})
export class CarEditComponent {

  carForm:FormGroup;
  carId:number;
  constructor(private fb:FormBuilder,private carService:CarServiceService,private toastr:ToastrService,private router:Router,private activatedRoute:ActivatedRoute)
  {
    this.carForm = this.fb.group({
      maker:['',[Validators.required]],
      model:['',[Validators.required]],
      rentPrice:['',[Validators.required,Validators.pattern("^[0-9]*$")]],
      availibilityStatus:['available',[Validators.required]],
    });

    this.carId = this.activatedRoute.snapshot.params['id'];
    this.getEditDetails();
  }

  //getters
  get maker()
  {
    return this.carForm.get('maker');
  }
  
  get model()
  {
    return this.carForm.get('model');
  }

  get rentPrice()
  {
    return this.carForm.get('rentPrice');
  }

  get availibilityStatus()
  {
    return this.carForm.get('availibilityStatus');
  }

  //get car details by id
  getEditDetails()
  {
    console.log("Car Id:",this.carId);
    this.carService.getCarDetails(this.carId).subscribe((res)=>{
      console.log("CarDetails",res);
      
      //set car details to form -- prefill
      this.carForm.patchValue({
        maker:res.maker,
        model:res.model,
        rentPrice:res.rentPrice,
        availibilityStatus:res.availibilityStatus
      })

    },(error)=>{
      console.error('Unable to edit car details:', error);
      this.toastr.error("Error while Editing Car","Car Error")
    })
    
  }

  //submit edit form
  submitForm(carData:FormGroup)
  {
    // console.log(carData.value);
    if(this.carForm.invalid)
    {
      this.toastr.error("Invalid Form","Please Enter Form Correctly")
      return;
    }
    
    //submit form data to backend using service
    const carInputs:FormData = new FormData();
    carInputs.append('Maker',carData.value.maker);
    carInputs.append('Model',carData.value.model);
    carInputs.append('RentPrice',carData.value.rentPrice);
    carInputs.append('AvailibilityStatus',carData.value.availibilityStatus);
    
    //send to api
    this.carService.editCarAdmin(this.carId,carInputs).subscribe((res)=>{
      console.log("Car response",res);
      this.toastr.success("Edited Car Successfully","Edit");
      this.router.navigate(['admin-carlist']);
    },(error)=>{
      console.log(error);
      this.toastr.error("Edit Failed",`Enable to edit`);
    });
  }

  
}
