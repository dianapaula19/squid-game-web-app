import { configureStore, ThunkAction, Action } from '@reduxjs/toolkit';
import reducer from '../features/auth/authSlice';
import counterReducer from '../features/counter/counterSlice';

export const store = configureStore({
  reducer: {
    auth: reducer,
    counter: counterReducer,
  },
  devTools: true
});

export type AppDispatch = typeof store.dispatch;
export type RootState = ReturnType<typeof store.getState>;
export type AppThunk<ReturnType = void> = ThunkAction<
  ReturnType,
  RootState,
  unknown,
  Action<string>
>;
