
export class IProperty {
  constructor() {
    this.action = "contains";
    this.logical_operator = "and";
    this.property = "";
    this.value = "";
  }
 
  property: string;
  action: string;
  value: string;
  logical_operator: string;
}
