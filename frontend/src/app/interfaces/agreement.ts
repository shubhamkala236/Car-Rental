import { Car } from "./car";
import { User } from "./user";

export interface Agreement {
    agreementId:number;
    userId:number;
    carId:number;
    rentalDuration:number;
    totalCost:number;
    isAccepted:boolean;
    isReturnRequested:boolean;
    isReturned:boolean;
    fromDate:Date;
    toDate:Date;
    carDetails?:Car;
    userData?:User
}
