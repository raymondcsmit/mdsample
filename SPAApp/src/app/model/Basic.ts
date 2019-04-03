
export class IBasic {
  constructor() {
    this.BasicID = 0;
    this.FkPatientID = 0;
  }
  BasicID: number;
  PasNumber: string;
  Forenames: string;
  Surname: string;
  DateOfBirth: Date;
  SexCode: string;
  HomeTelephoneNumber: string;
  FkPatientID: number;
}
