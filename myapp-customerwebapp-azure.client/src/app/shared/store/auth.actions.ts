import { createAction, props } from '@ngrx/store';
import { User } from '../models/user.model';

// Check auth on startup (token check)
export const checkAuth = createAction('[Auth] Check Auth');

// Load user profile from API
export const loadUserProfile = createAction('[Auth API] Load User Profile');

export const loadUserProfileSuccess = createAction(
  '[Auth API] Load Profile Success',
  props<{ user: User }>()
);

export const loadUserProfileFailure = createAction(
  '[Auth API] Load Profile Failure'
);

// Login success (manual login)
export const loginSuccess = createAction(
  '[Auth API] Login Success',
  props<{ user: User; token: string }>()
);

export const logout = createAction('[Auth] Logout');

