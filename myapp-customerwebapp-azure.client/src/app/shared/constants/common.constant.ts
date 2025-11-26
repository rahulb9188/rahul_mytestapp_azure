import { environment } from '../../../environments/environment';

export const Common = {
  BaseUrl: environment.apiBaseUrl  // or your actual API URL
};
export interface Role {
  key: number;
  value: string;
}

export const Roles: Role[] = [
  { key: 1, value: 'Customer' },
  { key: 2, value: 'Staff' }
];
