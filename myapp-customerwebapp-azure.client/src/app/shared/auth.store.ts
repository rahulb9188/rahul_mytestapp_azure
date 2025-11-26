import { createAction, props, createReducer, on, createSelector, createFeatureSelector } from '@ngrx/store';

export interface User {
  //id: string;
  username: string;
  email: string;
  //accessToken: string;
  //token: string;
}

export interface UserState {
  user: User | null;
}

export const initialState: UserState = {
  user: null
};

//
// 3. Actions
//
export const loginSuccess = createAction(
  '[Auth API] Login Success',
  props<{ user: User }>()
);

export const logout = createAction('[Auth] Logout');

//
// 4. Reducer
//
export const userReducer = createReducer(
  initialState,
  on(loginSuccess, (state, { user }) => ({ ...state, user })),
  on(logout, () => initialState)
);

//
// 5. Selectors
//
export const selectUserState = createFeatureSelector<UserState>('user');

export const selectCurrentUser = createSelector(
  selectUserState,
  (state) => state.user
);

export const selectIsLoggedIn = createSelector(
  selectCurrentUser,
  (user) => !!user 
);


