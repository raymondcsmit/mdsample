import { Directive, ElementRef, HostListener, Input } from '@angular/core';
import { NgControl } from "@angular/forms";

@Directive({
  selector: '[mvalidation]'
})

export class MValidationDirective {
  constructor(private el: ElementRef, private control: NgControl) {

  }
  @Input() entName: string;
  @Input() fieldName: string;  

  @HostListener('change') ngOnChanges() {
    this.validate(this.control.control.value);
  }
  private validate(value: string) {
    console.log('Validating:' + value + ' EntityName:' + this.entName + ' FieldName:' + this.fieldName);
    let valD = this.valJson.find(c => c.entName === this.entName && c.fieldName === this.fieldName);
    console.log(valD);

  }
  private valJson: any[] = [
    {
      id: 1, entName: "Basic", fieldName: "Forenames", validations: [
        {
          atype: "regex",
          expression: "/^[A-Za-z]+$/"
        },
        {
          atype: "required",
          expression: ""
        },
      ]
    },
    {
      id: 2, entName: "Person", fieldName: "lname", validations: [
        {
          atype: "regex",
          expression: "/^[A-Za-z]+$/"
        },
        {
          atype: "required",
          expression: ""
        },
      ]
    },
    {
      id: 3, entName: "Person", fieldName: "email", validations: [
        {
          atype: "regex",
          expression: "/^[A-Za-z]+$/"
        },
        {
          atype: "required",
          expression: ""
        },
      ]
    },
    {
      id: 3, entName: "Person", fieldName: "password", validations: [
        {
          atype: "regex",
          expression: "/^[A-Za-z]+$/"
        },
        {
          atype: "required",
          expression: ""
        },
      ]
    },
  ];
 

}