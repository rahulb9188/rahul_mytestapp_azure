import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedRoutingModule } from './shared-routing.module';
import { BooleanConverterPipe } from './pipes/boolean-converter.pipe';

@NgModule({
  declarations: [
    BooleanConverterPipe,
  ],
  imports: [
    CommonModule,
    SharedRoutingModule
  ],
  exports: [
    BooleanConverterPipe
  ]
})
export class SharedModule { }
