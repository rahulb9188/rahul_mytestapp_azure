import { createReducer, on } from "@ngrx/store";
import { loadUserProfileSuccess, loginSuccess, logout } from "./auth.actions";
import { User } from "../models/user.model";
import * as AuthActions from './auth.actions';
export interface AuthState {
  user: User | null;
}

export const initialState: AuthState = {
  user: null,
};

export const authReducer = createReducer(
  initialState,
  on(AuthActions.loginSuccess, (state, { user }) => ({
    ...state,
    user
  })),
  on(AuthActions.loadUserProfileSuccess, (state, { user }) => ({
    ...state,
    user
  })),
  on(AuthActions.logout, () => initialState)
);
