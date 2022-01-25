import { createAsyncThunk, createSlice, PayloadAction } from '@reduxjs/toolkit';
import axios, { AxiosResponse } from 'axios';
import { RootState, AppThunk } from '../../app/store';
import { API_AUTH_URL } from '../../Utils';

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
  email: string,
  password: string,
}

export interface LoginResponse {
  token: string,
  role: string,
  success: boolean,
  error: null | string,
}

export const loginAsync = createAsyncThunk(
    'auth/login',
    async (loginRequest: ILoginRequest, thunkAPI) => {
      await axios
        .post<LoginResponse>(API_AUTH_URL + "login", loginRequest)
        .then((response) => {
          localStorage.setItem("token", response.data.token)
          console.log('worked');
          return response.data
        })
        .catch((error) => {
          return thunkAPI.rejectWithValue(error)
        })
    }
    
)

export const authSlice = createSlice({
  name: 'auth',
  initialState,
  reducers: {
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
  },
});


const { reducer } = authSlice;
export default reducer;
