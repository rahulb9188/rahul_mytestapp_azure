import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Store } from '@ngrx/store';
import { SharedMaterialModule } from '../../modules/shared-material.module';
import { selectIsLoggedIn } from '../../store/auth.selectors';
import { MaterialImports } from '../../../material.module';
import { AsyncPipe, CommonModule } from '@angular/common';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css'],
  imports: [
    [...MaterialImports,
      CommonModule]
  ],
  standalone: true
})
export class SidebarComponent implements OnInit {
  isLoggedIn$: Observable<boolean>;

  constructor(private store: Store) {
    this.isLoggedIn$ = this.store.select(selectIsLoggedIn);
  }

  ngOnInit(): void {
    // Component initialization logic can go here.
  }

  logout() { }
}
