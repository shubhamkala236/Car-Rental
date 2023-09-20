import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CarServiceService } from 'src/app/services/car-service.service';

@Component({
  selector: 'app-car-create',
  templateUrl: './car-create.component.html',
  styleUrls: ['./car-create.component.css']
})
export class CarCreateComponent {
  carForm:FormGroup;
  constructor(private fb:FormBuilder,private carService:CarServiceService,private toastr:ToastrService,private router:Router,private activatedRoute:ActivatedRoute)
  {
    this.carForm = this.fb.group({
      maker:['',[Validators.required]],
      model:['',[Validators.required]],
      rentPrice:['',[Validators.required,Validators.pattern("^[0-9]*$")]],
      availibilityStatus:['available',[Validators.required]],
    });
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

//submit form
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
    this.carService.createCarAdmin(carInputs).subscribe((res)=>{
      console.log("Car response",res);
      this.toastr.success("Created Car Successfully","Create");
      this.router.navigate(['admin-carlist']);
    },(error)=>{
      console.log(error);
      this.toastr.error("Create Failed",`Car may be already present`);
    });
  
}

}
