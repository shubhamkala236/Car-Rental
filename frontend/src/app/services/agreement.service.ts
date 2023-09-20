import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { Agreement } from '../interfaces/agreement';

@Injectable({
  providedIn: 'root'
})
export class AgreementService {

  constructor(private http:HttpClient) { }

  private baseServerUrl = "https://localhost:44350/api/";

  //create a agreement
  createAgreement(agreementData:FormData)
  {
    // const headers = this.auth.createHeader();
    return this.http.post(this.baseServerUrl+"Agreement/rentalAgreement",agreementData,{responseType:'text'})
  }

  // get my agreements
  getMyAgreements()
  {
    return this.http.get<Agreement[]>(this.baseServerUrl+"Agreement/myAgreement");
  }

  //edit agreement
  editAgreement(agreementId:number,rentalDuration:FormData)
  {
    return this.http.put(this.baseServerUrl+`Agreement/editAgreement/${agreementId}`,rentalDuration,{responseType:'text'})
  }

  //getAgreement by id
  getAgreementById(agreementId:number)
  { 
    // debugger
    return this.http.get<Agreement>(this.baseServerUrl+`Agreement/agreementById/${agreementId}`);
  }

  //getAgreement by id --- admin
  getAgreementByIdAdmin(agreementId:number)
  { 
    // debugger
    return this.http.get<Agreement>(this.baseServerUrl+`Agreement/agreementByIdAdmin/${agreementId}`);
  }

  //deleteAgreement by id
  deleteAgreementById(agreementId:number)
  {
    return this.http.delete(this.baseServerUrl+`Agreement/delete/${agreementId}`,{responseType:'text'})
  }

  //accept Agreement
  acceptAgreementById(agreementId:number)
  {
    return this.http.get(this.baseServerUrl+`Agreement/accept/${agreementId}`,{responseType:'text'})
  }

  //request Return agreement
  requestReturn(agreementId:number)
  {
    return this.http.get(this.baseServerUrl+`Agreement/returnRequest/${agreementId}`,{responseType:'text'});
  }

  //Admin --- get All agreements
  getAllAgreements()
  {
    return this.http.get<Agreement[]>(this.baseServerUrl+`Agreement/allAgreementsAdmin`)
  }

  //Admin Delete Any
  deleteAgreementAdmin(agreementId:number)
  {
    console.log("DELETE ADMIN API");
    
    return this.http.delete(this.baseServerUrl+`Agreement/deleteAgreementAdmin/${agreementId}`,{responseType:'text'})
  }

  //Admin Edit Any
  editAgreementAdmin(agreementId:number,rentalDuration:FormData)
  {
    return this.http.put(this.baseServerUrl+`Agreement/editAgreementAdmin/${agreementId}`,rentalDuration,{responseType:'text'})
  }

  //Approve Return Admin
  approveReturnAdmin(agreementId:number)
  {
    return this.http.get(this.baseServerUrl+`Agreement/requestApprove/${agreementId}`,{responseType:'text'});
  }

}
