import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Store } from '@ngrx/store';
import { selectIsLoggedIn } from './shared/store/auth.selectors';
import { checkAuth } from './shared/store/auth.actions';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  isLoggedIn$: Observable<boolean>;
  constructor(private store: Store) {
    this.store.dispatch(checkAuth());
    this.isLoggedIn$ = this.store.select(selectIsLoggedIn);
}

  ngOnInit() {
    //this.getForecasts();
  }

  title = 'myapp-customerwebapp-azure.client';
}
