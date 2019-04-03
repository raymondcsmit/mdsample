
import { IBasic } from "../model/basic";

import { IGpDetail } from "../model/gpdetail";

import { INextOfKin } from "../model/nextofkin";

export class IPatient {
  PatientID: number;
  FkBasic: IBasic;
  GpDetailList: IGpDetail[];
  FkNextOfKin: INextOfKin;
  constructor() {
    this.PatientID = 0;
    this.FkBasic = new IBasic();
    this.FkNextOfKin = new INextOfKin();
    this.GpDetailList = [];
  }

}
