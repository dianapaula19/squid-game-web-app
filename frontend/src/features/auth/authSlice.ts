import { createAsyncThunk, createSlice } from '@reduxjs/toolkit';
import axios from 'axios';
import { RootState } from '../../app/store';
import { API_AUTH_URL, Role } from '../../Utils';

export interface IAuthState {
  token: null | string;
  role: Role;
  email: null | string;
  isLoading: boolean;
  isSuccess: boolean;
  isError: boolean; 
}

const initialState: IAuthState = {
  token: null,
  role: Role.undefined,
  email: null,
  isLoading: false,
  isSuccess: false,
  isError: false,
};

export interface ILoginRequest {
  Email: string;
  Password: string;
}

export interface IProfileInfo {
  FirstName: string;
  LastName: string;
  Gender: string;
}

export interface IRegistrationRequest {
  Username: string;
  Email: string;
  Password: string;
  Profile?: IProfileInfo;
  Role: Role;
}

export interface ILoginResponse {
  token: null | string;
  email: null | string;
  role: Role;
  success: boolean;
  error: null | string;
}

export interface IRegistrationResponse {
  token: null | string;
  email: null | string;
  role: Role;
  success: boolean;
  error: null | string;
}

export interface Error {
  errorMessage: string;
}

export const loginAsync = createAsyncThunk<
  ILoginResponse,
  ILoginRequest,
  {
    rejectValue: Error
  }  
>(
  'auth/login',
  async (loginRequest: ILoginRequest, thunkAPI) => await axios
    .post(API_AUTH_URL + "login", loginRequest)
    .then((response) => {
      localStorage.setItem("token", response.data.token);

      return response.data;
    })
    .catch((error: Error) => {
      return thunkAPI.rejectWithValue(error);
    })  
);

export const registerAsync = createAsyncThunk<
  IRegistrationResponse,
  IRegistrationRequest,
  {
    rejectValue: Error
  }  
>(
  'auth/register',
  async (registrationRequest: IRegistrationRequest, thunkAPI) => await axios
    .post(API_AUTH_URL + "register", registrationRequest)
    .then((response) => {
      localStorage.setItem("token", response.data.token);
      return response.data;
    })
    .catch((error: Error) => {
      return thunkAPI.rejectWithValue(error);
    })
);


export const authSlice = createSlice({
  name: 'auth',
  initialState,
  reducers: {
    logout: (state) => {
      localStorage.removeItem("token");
      state.token = null;
      state.email = null;
      state.role = Role.undefined;
      state.isError = false;
      state.isLoading = false;
      state.isSuccess = false;
    }
  },
  extraReducers: (builder) => {
    builder
    .addCase(loginAsync.pending, (state) => {
      state.isLoading = true;
    })
    .addCase(loginAsync.fulfilled, (state, {payload}) => {
      state.isSuccess = true;
      state.isLoading = false;
      state.token = payload.token;
      state.role = payload.role;
      state.email = payload.email;
    })
    .addCase(loginAsync.rejected, (state) => {
      state.isSuccess = false;
      state.isLoading = false;
      state.isError = true;
    })
    .addCase(registerAsync.pending, (state) => {
      state.isLoading = true;
    })
    .addCase(registerAsync.fulfilled, (state, {payload}) => {
      state.isSuccess = true;
      state.isLoading = false;
      state.token = payload.token;
      state.role = payload.role;
      state.email = payload.email;
    }) 
    .addCase(registerAsync.rejected, (state) => {
      state.isSuccess = false;
      state.isLoading = false;
      state.isError = false;
    })
  },
});


const { reducer } = authSlice;
const authState = (state: RootState) => state.auth;
export  
{
  authState,
  reducer as authReducer
};
