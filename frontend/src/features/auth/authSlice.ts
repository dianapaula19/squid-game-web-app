import { createAsyncThunk, createSlice } from '@reduxjs/toolkit';
import axios from 'axios';
import { API_AUTH_URL, Role } from '../../Utils';

export interface AuthState {
  token: string;
  role: string;
  isLoading: boolean;
  isSuccess: boolean;
  isError: boolean; 
}

const initialState: AuthState = {
  token: '',
  role: '',
  isLoading: false,
  isSuccess: false,
  isError: false,
};

export interface ILoginRequest {
  email: string;
  password: string;
}

export interface IProfileInfo {
  firstName: string;
  lastName: string;
  gender: 'female' | 'male' | 'unspecified';
}

export interface IRegistrationRequest {
  username: string;
  email: string;
  password: string;
  profileInfo?: IProfileInfo;
  role: Role;
}

export interface LoginResponse {
  token: string;
  role: string;
  success: boolean;
  error: null | string;
}

export interface RegistrationResponse {
  token: string;
  role: Role;
  success: boolean;
  error: null | string;
}

export const loginAsync = createAsyncThunk(
  'auth/login',
  async (loginRequest: ILoginRequest, thunkAPI) => {
    await axios
      .post<LoginResponse>(API_AUTH_URL + "login", loginRequest)
      .then((response) => {
        localStorage.setItem("token", response.data.token)
        return response.data
      })
      .catch((error) => {
        return thunkAPI.rejectWithValue(error)
      })
  }  
);

export const registerAsync = createAsyncThunk(
  'auth/register',
  async (registrationRequest: IRegistrationRequest, thunkAPI) => {
    await axios
      .post<RegistrationResponse>(API_AUTH_URL + "register", registrationRequest)
      .then((response) => {
        localStorage.setItem("token", response.data.token);
        return response.data;
      })
      .catch((error) => {
        return thunkAPI.rejectWithValue(error);
      })
  }
);


export const authSlice = createSlice({
  name: 'auth',
  initialState,
  reducers: {
    logout: (state) => {
      localStorage.removeItem("token");
      state = initialState;
    }
  },
  extraReducers: (builder) => {
    builder
    .addCase(loginAsync.pending, (state) => {
      state.isLoading = true;
    })
    .addCase(loginAsync.fulfilled, (state) => {
      state.isSuccess = true;
      state.isLoading = false;
      
    })
    .addCase(loginAsync.rejected, (state) => {
      state.isSuccess = false;
      state.isLoading = false;
      state.isError = true;
    })
    .addCase(registerAsync.pending, (state) => {
      state.isLoading = true;
    })
    .addCase(registerAsync.fulfilled, (state) => {
      state.isSuccess = true;
      state.isLoading = false;
    }) 
    .addCase(registerAsync.rejected, (state) => {
      state.isSuccess = false;
      state.isLoading = false;
      state.isError = false;
    })
  },
});


const { reducer } = authSlice;
export default reducer;
