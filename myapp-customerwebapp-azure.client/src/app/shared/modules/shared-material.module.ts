import { NgModule } from '@angular/core';
import {
  MatSidenav,
  MatSidenavContainer,
  MatSidenavContent
} from '@angular/material/sidenav';
import { MatToolbar } from '@angular/material/toolbar';
import { MatNavList } from '@angular/material/list';
import { CommonModule } from '@angular/common';

@NgModule({
  imports: [CommonModule, MatSidenav, MatToolbar, MatNavList, MatSidenavContainer, MatSidenavContent],
  exports: [CommonModule, MatSidenav, MatToolbar, MatNavList, MatSidenavContainer, MatSidenavContent]
})
export class SharedMaterialModule { }
