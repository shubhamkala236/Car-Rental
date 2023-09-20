import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class CarServiceService {

  constructor(private http:HttpClient) { }
  private baseServerUrl = "https://localhost:44350/api/";

  //getAll cars
  getAllCars()
  {
    return this.http.get(this.baseServerUrl+"Car/allCars");
  }

  //get car details
  getCarDetails(carId:number)
  {
    return this.http.get<any>(this.baseServerUrl+`Car/carDetails/${carId}`);
  }

  //admin -- Edit Car
  createCarAdmin(carDetails:FormData)
  {
    return this.http.post(this.baseServerUrl+`Car/addCar`,carDetails,{responseType:'text'}); 
  }

  //admin -- Edit Car
  editCarAdmin(carId:number,editedDetails:FormData)
  {
    return this.http.put<any>(this.baseServerUrl+`Car/edit/${carId}`,editedDetails); 
  }

  //admin -- Delete Car
  deleteCar(carId:number)
  {
    return this.http.delete(this.baseServerUrl+`Car/delete/${carId}`,{responseType:'text'});
  }
}
