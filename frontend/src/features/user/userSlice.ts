import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import axios from "axios";
import { RootState } from "../../app/store";
import { API_USER_URL, GuardRole } from "../../Utils";

export interface IUserState {
    isLoading: boolean;
    isSuccess: boolean;
    isError: boolean;
    username: string | null;
    country: string | null;
    type?: GuardRole | null;
    firstName?: string | null;
    lastName?: string | null;
    gender?: string | null;    
}
  
const initialState: IUserState = {
    isLoading: false,
    isSuccess: false,
    isError: false,
    username: null,
    country: null,
    type: null,
    firstName: null,
    lastName: null,
    gender: null,
};


export interface UserProfileRequest {
    token: null | string;
    email: null | string;
}

export interface ApplicationUserProfileResponse {
    username: string;
    email: string;
    country: string;
    status: null | boolean;
}

export interface UserProfileResponse {
    applicationUserProfile: ApplicationUserProfileResponse;
    type?: GuardRole;
    firstName?: string;
    lastName?: string;
    gender?: string;
}

export const userProfileAsync = createAsyncThunk<
  UserProfileResponse,
  UserProfileRequest,
  {
    rejectValue: string
  }  
>(
    "user/profile",
    async (userProfileRequest: UserProfileRequest, thunkAPI) => await axios
        .post(
            API_USER_URL + "profile",
            {
                params: { email: userProfileRequest.email }
            },
            {
                headers: { Authorization: "Bearer " + userProfileRequest.token }
            }
        )
        .then((response) => {
            return response.data;
        })
        .catch((error: string) => {
            return thunkAPI.rejectWithValue(error);
        })
);

export const userSlice = createSlice({
    name: "user",
    initialState,
    reducers: {

    },
    extraReducers: (builder) => {
        builder
        .addCase(userProfileAsync.pending, (state) => {
            state.isLoading = true;
        })
        .addCase(userProfileAsync.fulfilled, (state, {payload}) => {
            state.isSuccess = true;
            state.isLoading = false;
            state.country = payload.applicationUserProfile.country;
            state.type = payload.type;
            state.firstName = payload.firstName;
            state.lastName = payload.lastName;
            state.gender = payload.gender;
        })
        .addCase(userProfileAsync.rejected, (state) => {
            state.isSuccess = false;
            state.isLoading = false;
            state.isError = true;
        })
    }
});

const { reducer } = userSlice;
const userState = (state: RootState) => state.user;

export {
    userState,
    reducer as userReducer
}