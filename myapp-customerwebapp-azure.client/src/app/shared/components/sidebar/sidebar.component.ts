import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Store } from '@ngrx/store';
import { selectIsLoggedIn } from '../../auth.store';
import { SharedMaterialModule } from '../../modules/shared-material.module';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css'],
  imports: [
    SharedMaterialModule
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
